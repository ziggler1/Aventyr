﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Models;
using Game.Rendering;
using OpenTK.Graphics;
using System.Collections.Immutable;
using Ui.Args;

namespace Ui.Elements
{
    public class Radio<T> : Button, IRadio
    {
        internal ElementFunc<T> _getValue;
        internal Action<T> _setValue { get; }
        public T Target { get; }
        public T Value => _getValue(ElementArgs);

        public bool Selected => Target.Equals(Value);

        internal ElementFunc<Color4> _selectedColor;
        public Color4 SelectedColor => GetValue(_selectedColor);

        public Radio(
            ElementFunc<float> x = null,
            ElementFunc<float> y = null,
            ElementFunc<float> width = null,
            ElementFunc<float> height = null,
            ElementAction<ClickArgs> onClick = null,
            ElementAction<HoverArgs> onHover = null,
            T target = default(T),
            ElementFunc<T> getValue = null,
            Action<T> setValue = null,
            ElementFunc<bool> enabled = null, 
            ElementFunc<bool> hidden = null,
            ElementFunc<Color4> color = null,
            ElementFunc<Color4> disabledColor = null,
            ElementFunc<Color4> hoverColor = null,
            ElementFunc<Color4> borderColor = null,
            ElementFunc<Color4> selectedColor = null,
            Style style = null)
            : base(x, y, width, height, onClick, onHover, enabled, hidden, color, disabledColor, hoverColor, borderColor, style)
        {
            var internalValue = default(T);
            _getValue = getValue ?? (_ => internalValue);
            _setValue = setValue ?? (value => internalValue = value);
            Target = target;

            _selectedColor = selectedColor;
        }

        public void SetValue() => _setValue(Target);

        public override Style RootStyle()
        {
            var defaultStyle = base.RootStyle();

            var type = typeof(Radio<>);
            return defaultStyle.With(new Style
            {
                new StyleElement(type, nameof(SelectedColor), arg => ((Button)arg.Self).HoverColor.AddRgb(0.1f, 0.1f, 0.1f))
            });
        }

        public override List<Model> GetModels(ModelArgs args)
        {
            var color = !Enabled ?
                DisabledColor :
                Selected ?
                    SelectedColor :
                    args.Controller.Hovered == this ?
                        HoverColor :
                        Color;

            return new[]
            {
                ModelFactory.CreatePlane(this.GetSize(), color),
            }.ToList();
        }
    }

    public interface IRadio : IHoverable
    {
        bool Selected { get; }

        void SetValue();
    }
}
