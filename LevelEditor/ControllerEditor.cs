﻿using Game;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor
{
    public class ControllerEditor : Controller
    {
        Scene Level;
        bool _isPaused;
        ControllerCamera camControl;
        public delegate void EntityAddedHandler(ControllerEditor controller, Entity entity);
        public event EntityAddedHandler EntityAdded;
        public delegate void SceneEventHandler(ControllerEditor controller, Scene scene);
        public event SceneEventHandler ScenePaused;
        public event SceneEventHandler ScenePlayed;
        public event SceneEventHandler SceneStopped;

        public ControllerEditor(Window window)
            : base(window)
        {
        }

        public ControllerEditor(Size canvasSize, InputExt input)
            : base(canvasSize, input)
        {
        }

        public override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Level = new Scene();
            renderer.AddScene(Level);

            Model background = Model.CreatePlane();
            background.TextureId = Renderer.Textures["grid.png"];
            background.Transform.Position = new Vector3(0, 0, -10f);
            float size = 100;
            background.Transform.Scale = new Vector3(size, size, size);
            background.TransformUv.Scale = new Vector2(size, size);
            Entity back = new Entity(Level, new Vector2(0f, 0f));
            back.Models.Add(background);

            Camera cam = Camera.CameraOrtho(new Vector3(0, 0, 10f), 10, CanvasSize.Width / (float)CanvasSize.Height);

            Level.ActiveCamera = cam;

            camControl = new ControllerCamera(cam, InputExt);
        }

        public override void OnRenderFrame(OpenTK.FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        public override void OnUpdateFrame(OpenTK.FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            camControl.Update();
            if (InputExt.MouseInside)
            {
                if (InputExt.MousePress(MouseButton.Right))
                {
                    Camera cam = Level.ActiveCamera;
                    Entity entity = new Entity(Level);
                    entity.Transform.Position = cam.ScreenToWorld(InputExt.MousePos);
                    entity.Models.Add(Model.CreateCube());
                    if (EntityAdded != null)
                    {
                        EntityAdded(this, entity);
                    }
                }
            }
            if (!_isPaused)
            {
                Level.Step();
            }
        }

        public void ScenePlay()
        {
            _isPaused = false;
            if (ScenePlayed != null)
                ScenePlayed(this, Level);
        }

        public void ScenePause()
        {
            _isPaused = true;
            if (ScenePaused != null)
                ScenePaused(this, Level);
        }

        public void SceneStop()
        {
            _isPaused = true;
            if (SceneStopped != null)
                SceneStopped(this, Level);
        }

        public override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        public override void OnResize(EventArgs e, System.Drawing.Size canvasSize)
        {
            base.OnResize(e, canvasSize);
            Level.ActiveCamera.Aspect = CanvasSize.Width / (float)CanvasSize.Height;
        }
    }
}
