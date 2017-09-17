﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Common;
using OpenTK;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Game.Rendering
{
    /// <summary>
    /// A camera intended for rendering things that need to align to pixels such as text and sprites.
    /// </summary>
    [DataContract]
    public class HudCamera2 : ICamera2
    {
        public float Aspect => (float)CanvasSize.XRatio;

        public Vector2 ViewOffset => Vector2.One;

        public double Fov => Math.PI / 4;
        public bool IsOrtho => true;

        public Vector2i CanvasSize => CanvasSizeFunc();
        public Func<Vector2i> CanvasSizeFunc { get; }

        public Transform2 WorldTransform => new Transform2(new Vector2(0, CanvasSize.Y), 0, -CanvasSize.Y, true);
        public Transform2 WorldVelocity => Transform2.CreateVelocity();

        public HudCamera2(Func<Vector2i> canvasSize)
        {
            CanvasSizeFunc = canvasSize;
        }
    }
}