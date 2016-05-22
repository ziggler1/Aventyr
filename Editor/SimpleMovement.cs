﻿using Game;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class SimpleMovement
    {
        public float Acceleration { get; set; }
        /// <summary>Friction coefficient.</summary>
        public float Friction { get; set; }
        public InputExt Input { get; set; }

        public SimpleMovement(InputExt input, float acceleration, float friction)
        {
            Acceleration = acceleration;
            Friction = friction;
            Input = input;
        }

        public void Move(IPortalable moveable)
        {
            Transform2 v = moveable.GetVelocity();
            Transform2 t = moveable.GetTransform();
            if (Input.KeyDown(Key.Left))
            {
                v.Position += t.GetRight() * -Acceleration * Math.Abs(Transform2.GetSize(moveable));
            }
            if (Input.KeyDown(Key.Right))
            {
                v.Position += t.GetRight() * Acceleration * Math.Abs(Transform2.GetSize(moveable));
            }
            if (Input.KeyDown(Key.Up))
            {
                v.Position += t.GetUp() * Acceleration * Math.Abs(Transform2.GetSize(moveable));
            }
            if (Input.KeyDown(Key.Down))
            {
                v.Position += t.GetUp() * -Acceleration * Math.Abs(Transform2.GetSize(moveable));
            }
            v.Position *= 1 - Friction;
            moveable.SetVelocity(v);
        }
    }
}