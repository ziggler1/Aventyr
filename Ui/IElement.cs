﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Game;
using Game.Common;
using Game.Models;
using OpenTK;

namespace Ui
{
    public interface IElement : IEnumerable<IElement>
    {
        ElementArgs ElementArgs { get; set; }

        float GetX();
        float GetY();
        /// <summary>
        /// Element and child elements are excluded from processing.
        /// </summary>
        bool GetHidden();
        float GetWidth();
        float GetHeight();

        bool IsInside(Vector2 localPoint);
        List<Model> GetModels();
    }

    public static class ElementEx
    {
        public static ElementFunc<float> AlignX(float t) => 
            args => (args.Parent.GetWidth() - args.Self.GetWidth()) * t;
        public static ElementFunc<float> AlignY(float t) => 
            args => (args.Parent.GetHeight() - args.Self.GetHeight()) * t;
        public static ElementFunc<float> ChildWidth() => 
            args => args.Self.MaxOrNull(child => child.GetX() + child.GetWidth()) ?? 0;
        public static ElementFunc<float> ChildHeight() => 
            args => args.Self.MaxOrNull(child => child.GetY() + child.GetHeight()) ?? 0;

        public static Vector2 GetPosition(this IElement element) => new Vector2(element.GetX(), element.GetY());
        public static Vector2 GetSize(this IElement element) => new Vector2(element.GetWidth(), element.GetHeight());
        public static Transform2 GetTransform(this IElement element) => new Transform2(element.GetPosition());
    }

    public delegate T ElementFunc<T>(ElementArgs args);
}
