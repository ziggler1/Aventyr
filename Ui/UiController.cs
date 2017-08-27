﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Game;
using Game.Common;
using Game.Models;
using Game.Rendering;
using OpenTK;
using OpenTK.Input;
using System.Text;
using OpenTK.Graphics;
using Ui.Elements;

namespace Ui
{
    public class UiController : IUiController
    {
        public Frame Root { get; }
        readonly IVirtualWindow _window;
        ICamera2 _camera;
        List<UiWorldTransform> _flattenedUi = new List<UiWorldTransform>();
        public Element Hover { get; private set; }
        public TextBox Selected { get; private set; }
        public (Element Element, DateTime Time) LastClick = (null, new DateTime());
        public TimeSpan DoubleClickSpeed { get; set; } = TimeSpan.FromSeconds(0.6);

        public FontAssets Fonts => _window.Fonts;

        public UiController(IVirtualWindow window, Frame root)
        {
            _window = window;

            Root = root;
            Root.Style = DefaultStyle();
            Root._x = _ => 0;
            Root._y = _ => 0;
            Root._width = _ => _window.CanvasSize.X;
            Root._height = _ => _window.CanvasSize.Y;
        }

        public ImmutableDictionary<(Type, string), ElementFunc<object>> DefaultStyle()
        {
            return Element.DefaultStyle(this)
                .Concat(Button.DefaultStyle(this))
                .Concat(Element.DefaultStyle(this))
                .Concat(Radio<object>.DefaultStyle(this))
                .Concat(Rectangle.DefaultStyle(this))
                .Concat(StackFrame.DefaultStyle(this))
                .Concat(TextBlock.DefaultStyle(this))
                .Concat(TextBox.DefaultStyle(this))
                .ToImmutable();
        }

        public void Update(float uiScale)
        {
            var time = DateTime.UtcNow;

            _camera = new HudCamera2(_window.CanvasSize);
            var mousePos = _window.MouseWorldPos(_camera);

            UpdateElementArgs(Root);
            _flattenedUi = AllChildren();

            DebugEx.Assert(
                _flattenedUi.Distinct().Count() == _flattenedUi.Count, 
                "Elements should not be used in multiple places.");

            var hover = _flattenedUi
                .FirstOrDefault(item =>
                    item.Element.IsInside(
                        Vector2Ex.Transform(
                            mousePos,
                            item.WorldTransform.GetMatrix().Inverted())));
            Hover = hover?.Element;
            if (_window.ButtonPress(MouseButton.Left))
            {
                var isDoubleClick = 
                    (time - LastClick.Time < DoubleClickSpeed) && 
                    LastClick.Element == Hover;

                if (Hover != null)
                {
                    switch (Hover)
                    {
                        case Button button:
                            if (button.Enabled)
                            {
                                if (button is IRadio radio)
                                {
                                    radio.SetValue();
                                }
                                var args = new ClickArgs(
                                    isDoubleClick, 
                                    button.ElementArgs.Parent, 
                                    button.ElementArgs.Self);
                                button.OnClick(args);
                            }
                            break;
                        case TextBox textBox:
                            SetSelected(textBox);
                            break;
                    }
                }
                else
                {
                    SetSelected(null);
                }

                LastClick = (Hover, time);
            }

            if (_window.ButtonPress(Key.Enter))
            {
                SetSelected(null);
            }

            if (Selected != null)
            {
                var text = Selected.Text;
                DebugEx.Assert(Selected.CursorIndex != null);
                var newCursorText = TextInput.Update(_window, new CursorText(text, Selected.CursorIndex));
                Selected.SetText(newCursorText.Text);
                Selected.CursorIndex = newCursorText.CursorIndex;
            }
        }

        public void SetSelected(TextBox selected)
        {
            if (Selected != null)
            {
                Selected.CursorIndex = null;
            }
            Selected = selected;
            if (Selected != null)
            {
                Selected.CursorIndex = 0;
            }
        }

        void UpdateElementArgs(Element root)
        {
            root.ElementArgs = new ElementArgs(null, root);
            _updateElementArgs(root);
        }

        void _updateElementArgs(Element element)
        {
            foreach (var child in element)
            {
                child.ElementArgs = new ElementArgs(element, child);
                _updateElementArgs(child);
            }
        }

        List<UiWorldTransform> AllChildren()
        {
            var list = new List<UiWorldTransform>();
            _allChildren(Root, new Transform2(), list);
            return list;
        }

        void _allChildren(Element element, Transform2 worldTransform, List<UiWorldTransform> list)
        {
            if (element.Hidden)
            {
                return;
            }
            var transform = worldTransform.Transform(element.GetTransform());
            foreach (var child in element)
            {
                _allChildren(child, transform, list);
            }
            
            list.Add(new UiWorldTransform(element, transform));
        }

        public Layer Render()
        {
            return new Layer
            {
                DepthTest = false,
                Camera = _camera,
                Renderables = _flattenedUi
                    .Select(item =>
                    {
                        var element = item.Element;
                        var elementArgs = item.Element.ElementArgs;
                        return (IRenderable)new Renderable(
                            item.WorldTransform,
                            element.GetModels(new ModelArgs(element == Selected, elementArgs.Parent, elementArgs.Self)))
                        {
                            PixelAlign = element is TextBlock || element is TextBox
                        };
                    })
                    .Reverse()
                    .ToList()
            };
        }

        class UiWorldTransform
        {
            public Element Element { get; }
            public Transform2 WorldTransform { get; }

            public UiWorldTransform(Element element, Transform2 worldTransform)
            {
                DebugEx.Assert(element != null);
                DebugEx.Assert(worldTransform != null);
                Element = element;
                WorldTransform = worldTransform;
            }
        }
    }
}
