﻿// This is generated from Transform3d.cs.
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Common
{
    public partial class Transform3
    {
        Matrix4 _matrix;
        bool _dirty = true;
        Vector3 _position;
        Quaternion _rotation = new Quaternion(0, 0, 1, 0);
        Vector3 _scale = new Vector3(1, 1, 1);

        public Quaternion Rotation {
            get => _rotation;
            set { _rotation = value; _dirty = true; }
        }
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                _dirty = true;
                _scale = value;
            }
        }
        public Vector3 Position {
            get => _position;
            set { _position = value; _dirty = true; }
        }

        public Transform3()
        {
        }

        public Transform3(Vector3 position)
        {
            Position = position;
        }

        public Transform3(Vector3 position, Vector3 scale)
        {
            Position = position;
            Scale = scale;
        }

        public Transform3(Vector3 position, Vector3 scale, Quaternion rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public Transform3 ShallowClone()
        {
            return new Transform3
            {
                Rotation = new Quaternion(Rotation.X, Rotation.Y, Rotation.Z, Rotation.W),
                Position = new Vector3(Position),
                Scale = new Vector3(Scale),
            };
        }

        public Matrix4 GetMatrix()
        {
            if (_dirty)
            {
#pragma warning disable
                _matrix = Matrix4.Scale(Scale) * Matrix4.CreateFromAxisAngle(new Vector3(Rotation.X, Rotation.Y, Rotation.Z), Rotation.W) * Matrix4.CreateTranslation(Position);
#pragma warning restore
                _dirty = false;
            }
            return _matrix;
        }

        public static Transform3 Lerp(Transform3 a, Transform3 b, float t)
        {
            return new Transform3
            {
                Position = Vector3.Lerp(a.Position, b.Position, t),
                Scale = Vector3.Lerp(a.Scale, b.Scale, t),
                Rotation = Quaternion.Slerp(a.Rotation, b.Rotation, t)
            };
        }
    }
}