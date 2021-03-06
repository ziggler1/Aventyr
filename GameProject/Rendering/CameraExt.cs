﻿using System;
using System.Collections.Generic;
using Game.Common;
using OpenTK;

namespace Game.Rendering
{
    public static class CameraExt
    {
        /// <summary>
        /// Create a view matrix for this Camera
        /// </summary>
        public static Matrix4 GetViewMatrix(ICamera2 camera, bool isOrtho = true)
        {
            Transform2 transform = camera.GetWorldTransform();
            var m = Matrix4.CreateRotationZ(transform.Rotation);
            Vector3 lookat = new Vector3(transform.Position) + Vector3.Transform(new Vector3(0, 0, -1), m);
            Vector3 eye;
            Matrix4 perspective;

            if (isOrtho)
            {
                float x = camera.ViewOffset.X / 2;
                float y = camera.ViewOffset.Y / 2;

                float width = transform.Scale.X * camera.Aspect;
                float height = transform.Scale.Y;
                x *= transform.Scale.X * camera.Aspect;
                y *= transform.Scale.Y;
                perspective = Matrix4.CreateOrthographicOffCenter(x - width / 2, x + width / 2, y - height / 2, y + height / 2, camera.ZNear, camera.ZFar);
                eye = new Vector3(transform.Position) + new Vector3(0, 0, 50);
                return Matrix4.LookAt(eye, lookat, new Vector3(GetUp(camera))) * perspective;
            }
            
            perspective = Matrix4.CreatePerspectiveFieldOfView((float)camera.Fov, camera.Aspect, 0.01f, 10000f);
            perspective = Matrix4.CreateScale(transform.Scale.X, transform.Scale.Y, Math.Abs(transform.Size)) * perspective;
            eye = new Vector3(transform.Position) + new Vector3(0, 0, (float)GetWorldZ(camera));
            return Matrix4.LookAt(eye, lookat, new Vector3(GetUp(camera))) * perspective * Matrix4.CreateTranslation(new Vector3(-camera.ViewOffset.X, -camera.ViewOffset.Y, 0));
        }

        public static float UnitZToWorld(ICamera2 camera, float z)
        {
            return (1 - z) * (float)GetWorldZ(camera);
        }

        static double GetWorldZ(ICamera2 camera)
        {
            return Math.Abs(camera.GetWorldTransform().Size / (2 * Math.Tan(camera.Fov / 2)));
        }

        //get xy world offset needed to make v appear to overlap target in screen space.
        public static Vector2 GetOverlapOffset(ICamera2 camera, Vector3 v, Vector3 target)
        {
            Vector3 cameraPos = new Vector3(camera.GetWorldTransform().Position);
            cameraPos.Z = (float)GetWorldZ(camera);
            float x = (v.X - cameraPos.X) / (v.Z - cameraPos.Z) - (target.X - cameraPos.X) / (target.Z - cameraPos.Z);
            float y = (v.Y - cameraPos.Y) / (v.Z - cameraPos.Z) - (target.Y - cameraPos.Y) / (target.Z - cameraPos.Z);
            Vector2 offset = -new Vector2(x, y) * (v.Z - cameraPos.Z);

            return offset;
        }

        public static Vector2[] GetVerts()
        {
            return new Vector2[4] {
                new Vector2(-1f, -1f),
                new Vector2(1f, -1f),
                new Vector2(1f, 1f),
                new Vector2(-1f, 1f),
            };
        }

        public static Vector2[] GetWorldVerts(ICamera2 camera)
        {
            return Vector2Ext.Transform(GetVerts(), WorldToClipMatrix(camera).Inverted());
        }

        static Matrix4 WorldToScreenMatrix(ICamera2 camera, Vector2 canvasSize)
        {
            Matrix4 scale = Matrix4.CreateScale(canvasSize.X / 2, -canvasSize.Y / 2, 1);
            Matrix4 translation = Matrix4.CreateTranslation(new Vector3(1f, -1f, 0f));
            return GetViewMatrix(camera) * translation * scale;
        }

        static Matrix4 WorldToClipMatrix(ICamera2 camera)
        {
            Matrix4 scale = Matrix4.CreateScale(1, -1, 1);
            Matrix4 translation = Matrix4.CreateTranslation(new Vector3(0f, 0f, 0f));
            return GetViewMatrix(camera) * translation * scale;
        }

        public static Vector2 WorldToScreen(ICamera2 camera, Vector2 worldCoord, Vector2 canvasSize)
        {
            return Vector2Ext.Transform(worldCoord, WorldToScreenMatrix(camera, canvasSize));
        }

        public static Vector2[] WorldToScreen(ICamera2 camera, IList<Vector2> worldCoord, Vector2 canvasSize)
        {
            return Vector2Ext.Transform(worldCoord, WorldToScreenMatrix(camera, canvasSize)).ToArray();
        }

        public static Vector2 ScreenToWorld(ICamera2 camera, Vector2 screenCoord, Vector2 canvasSize)
        {
            return Vector2Ext.Transform(screenCoord, WorldToScreenMatrix(camera, canvasSize).Inverted());
        }

        public static Vector2[] ScreenToWorld(ICamera2 camera, IList<Vector2> screenCoord, Vector2 canvasSize)
        {
            return Vector2Ext.Transform(screenCoord, WorldToScreenMatrix(camera, canvasSize).Inverted()).ToArray();
        }

        public static Vector2 ScreenToClip(ICamera2 camera, Vector2 screenCoord, Vector2 canvasSize)
        {
            return Vector2Ext.Transform(screenCoord, WorldToScreenMatrix(camera, canvasSize).Inverted() * GetViewMatrix(camera));
        }

        public static Vector2[] ScreenToClip(ICamera2 camera, IList<Vector2> screenCoord, Vector2 canvasSize)
        {
            return Vector2Ext.Transform(screenCoord, WorldToScreenMatrix(camera, canvasSize).Inverted() * GetViewMatrix(camera)).ToArray();
        }

        public static Vector2 ClipToWorld(ICamera2 camera, Vector2 screenCoord)
        {
            return Vector2Ext.Transform(screenCoord, GetViewMatrix(camera).Inverted());
        }

        public static Vector2[] ClipToWorld(ICamera2 camera, IList<Vector2> screenCoord)
        {
            return Vector2Ext.Transform(screenCoord, GetViewMatrix(camera).Inverted()).ToArray();
        }

        static Vector2 GetUp(ICamera2 camera)
        {
            Matrix4 m = Matrix4.CreateRotationZ(camera.GetWorldTransform().Rotation);
            return Vector2Ext.Transform(new Vector2(0, 1), m);
        }

        public static Transform2 GetWorldViewpoint(ICamera2 camera)
        {
            return new Transform2(camera.ViewOffset).Transform(camera.GetWorldTransform());
        }
    }
}
