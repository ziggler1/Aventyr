﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Common;
using Game.Models;
using OpenTK;
using Game.Rendering;
using OpenTK.Graphics;

namespace Ui.Elements
{
    public class TextBox : Element, ISelectable
    {
        public enum Input { Text, Numbers }

        internal ElementFunc<Font> _font;
        internal ElementFunc<Color4> _backgroundColor;
        internal ElementFunc<string> _text;
        public Action<string> SetText { get; }
        public int? CursorIndex { get; set; }

        public string Text => InvokeFunc(_text);
        public Font Font => InvokeFunc(_font);
        public Color4 BackgroundColor => InvokeFunc(_backgroundColor);

        public TextBox(
            ElementFunc<float> x = null,
            ElementFunc<float> y = null,
            ElementFunc<float> width = null,
            ElementFunc<float> height = null,
            ElementFunc<string> getText = null,
            Action<string> setText = null,
            ElementFunc<Font> font = null,
            ElementFunc<Color4> backgroundColor = null,
            ElementFunc<bool> hidden = null)
            : base(x, y, width, height, hidden)
        {
            _font = font;

            var defaultText = "";
            _text = getText ?? (_ => defaultText);
            SetText = setText ?? (newText => defaultText = newText);

            _backgroundColor = backgroundColor;
        }

        public TextBox(
            out TextBox id,
            ElementFunc<float> x = null,
            ElementFunc<float> y = null,
            ElementFunc<float> width = null,
            ElementFunc<float> height = null,
            ElementFunc<string> getText = null,
            Action<string> setText = null,
            ElementFunc<Font> font = null,
            ElementFunc<Color4> backgroundColor = null,
            ElementFunc<bool> hidden = null)
            : this(x, y, width, height, getText, setText, font, backgroundColor, hidden)
        {
            id = this;
        }

        public override List<Model> GetModels(ModelArgs args)
        {
            var models = new List<Model>();
            var margin = new Vector2i(2, 2);
            var size = this.GetSize();
            if (size != new Vector2())
            {
                models.AddRange(Draw.Rectangle(new Vector2(), size, Color4.Brown).GetModels());
                models.AddRange(Draw.Rectangle((Vector2)margin, size - (Vector2)margin, BackgroundColor).GetModels());

                var settings = new Font.Settings();

                var font = Font;
                var text = Text;

                var textPos = new Vector2i(
                    margin.X + 5,
                    ((int)size.Y - font.GetSize(text, settings).Y) / 2);

                if (CursorIndex != null)
                {
                    var cursorPos = textPos + font.BaselinePosition(text, (int)CursorIndex, settings);

                    var cursor = Draw.Rectangle(
                        (Vector2)(cursorPos + new Vector2i(-1, -font.FontData.Common.Base)),
                        (Vector2)(cursorPos + new Vector2i(1, -font.FontData.Common.Base + font.FontData.Info.Size)), Color4.Black).GetModels();
                    models.AddRange(cursor);
                }

                var textModel = font.GetModel(text, Color4.Black);
                textModel.Transform.Position += new Vector3(
                    textPos.X,
                    textPos.Y, 
                    0); 
                models.Add(textModel);
            }
            return models;
        }

        public static new Style DefaultStyle(IUiController controller)
        {
            var type = typeof(TextBox);
            return new Style
            {
                new StyleElement(type, nameof(BackgroundColor), _ => Color4.White),
                new StyleElement(type, nameof(Font), _ => controller.Fonts.Inconsolata),
            };
        }

        public override bool IsInside(Vector2 localPoint)
        {
            return MathEx.PointInRectangle(new Vector2(), this.GetSize(), localPoint);
        }
    }
}
