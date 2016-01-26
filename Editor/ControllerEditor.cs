﻿using Game;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public class ControllerEditor : Controller
    {
        public Scene Hud, Back;
        public EditorScene Level;
        bool _isPaused;
        public ControllerCamera CamControl { get; private set; }
        public delegate void EditorObjectHandler(ControllerEditor controller, EditorObject entity);
        public event EditorObjectHandler EntityAdded;
        public delegate void SceneEventHandler(ControllerEditor controller, Scene scene);
        public event SceneEventHandler ScenePaused;
        public event SceneEventHandler ScenePlayed;
        public event SceneEventHandler SceneStopped;
        /// <summary>Called when an EditorObject's public state has been modified.</summary>
        public event SceneEventHandler SceneModified;
        public delegate void ToolEventHandler(ControllerEditor controller, Tool tool);
        public event ToolEventHandler ToolChanged;
        bool _editorObjectModified;
        Tool _activeTool;
        Tool _toolDefault;
        Tool _nextTool;
        Queue<Action> Actions = new Queue<Action>();
        public Selection selection { get; private set; }
        public StateList StateList { get; private set; }
        object _lockAction = new object();

        public ControllerEditor(Size canvasSize, InputExt input)
            : base(canvasSize, input)
        {
        }

        public override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Back = new Scene();
            renderer.AddScene(Back);
            NewLevel();
            
            Hud = new Scene();
            Hud.SetActiveCamera(new Camera2(Hud, new Vector2(CanvasSize.Width / 2, CanvasSize.Height / 2), CanvasSize.Width, CanvasSize.Width / (float)CanvasSize.Height));
            renderer.AddScene(Hud);
            /*debugText = new Entity(Hud);
            debugText.SetTransform(new Transform2D(new Vector2(0, CanvasSize.Height - 40)));
            */
            InitTools();
            ScenePause();
        }

        public void NewLevel()
        {
            /*if (Level != null)
            {
                renderer.RemoveScene(Level.Scene);
            }*/
            Level = new EditorScene(Back);
            //renderer.AddScene(Level.Scene);

            #region create background
            Model background = ModelFactory.CreatePlane();
            background.Texture = Renderer.Textures["grid.png"];
            background.SetColor(new Vector3(1, 1, 0.5f));
            background.Transform.Position = new Vector3(0, 0, -5f);
            float size = 50;
            background.Transform.Scale = new Vector3(size, size, size);
            background.TransformUv.Scale = new Vector2(size, size);
            Entity back = new Entity(Back, new Vector2(0f, 0f));
            back.AddModel(background);
            #endregion

            selection = new Selection(Level);
            StateList = new StateList();

            Back.SetActiveCamera(new Camera2(Back, new Vector2(), 10, CanvasSize.Width / (float)CanvasSize.Height));

            Entity viewCenter = new Entity(Back);
            viewCenter.AddModel(ModelFactory.CreateCircle(new Vector3(), 0.1f, 10));
            viewCenter.DrawOverPortals = true;
            viewCenter.ModelList[0].SetColor(new Vector3(1, 0.9f, 0.2f));
            viewCenter.SetParent(Back.ActiveCamera);

            CamControl = new ControllerCamera(this, Back.ActiveCamera, InputExt);
        }

        public override void OnRenderFrame(OpenTK.FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            /*debugText.Models.Clear();
            debugText.Models.Add(FontRenderer.GetModel((Time.ElapsedMilliseconds / RenderCount).ToString()));*/
        }

        public Vector2 GetMouseWorldPosition()
        {
            return Back.ActiveCamera.ScreenToWorld(InputExt.MousePos);
        }

        /*public void SetMouseWorldPosition(Vector2 mousePosition)
        {
            Level.ActiveCamera.WorldToScreen(mousePosition);
            //System.Windows.Forms.Cursor.Position = mousePosition;
        }*/

        public EditorEntity CreateLevelEntity()
        {
            EditorEntity entity = new EditorEntity(this.Level);

            if (EntityAdded != null)
            {
                EntityAdded(this, entity);
            }
            return entity;
        }

        public void Remove(EditorObject editorObject)
        {
            editorObject.Remove();
            selection.Remove(editorObject);
        }

        public void RemoveRange(List<EditorObject> editorObjects)
        {
            foreach (EditorObject e in editorObjects)
            {
                e.Remove();
                selection.Remove(e);
            }
        }

        public void SetEditorObjectModified()
        {
            _editorObjectModified = true;
        }

        public override void OnUpdateFrame(OpenTK.FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            lock (_lockAction)
            {
                foreach (Action item in Actions)
                {
                    item();
                }
            }
            Actions.Clear();
            CamControl.Update();
            _setTool(_nextTool);
            _activeTool.Update();
            if (InputExt.KeyDown(InputExt.KeyBoth.Control) && !_activeTool.Active)
            {
                if (InputExt.KeyPress(Key.Y) || (InputExt.KeyDown(InputExt.KeyBoth.Shift) && InputExt.KeyPress(Key.Z)))
                {
                    StateList.Redo();
                }
                else if (InputExt.KeyPress(Key.Z))
                {
                    StateList.Undo();
                }
            }
            if (_editorObjectModified && SceneModified != null)
            {
                //SceneModified(this, Level);
                _editorObjectModified = false;
            }
            if (!_isPaused)
            {
                Back.Step();
            }
        }

        private void _setTool(Tool tool)
        {
            Debug.Assert(tool != null, "Tool cannot be null.");
            if (_activeTool == _nextTool)
            {
                return;
            }
            _activeTool.Disable();
            _activeTool = tool;
            _activeTool.Enable();
            if (ToolChanged != null)
            {
                ToolChanged(this, tool);
            }
        }

        private void InitTools()
        {
            _toolDefault = new ToolDefault(this);
            _activeTool = _toolDefault;
            _nextTool = _activeTool;
            _activeTool.Enable();
        }

        public void SetTool(Tool tool)
        {
            if (tool == null)
            {
                _nextTool = _toolDefault;
            }
            else
            {
                _nextTool = tool;
            }
        }

        public EditorObject GetNearestObject(Vector2 point)
        {
            return GetNearestObject(point, item => true);
        }

        public EditorObject GetNearestObject(Vector2 point, Func<EditorObject, bool> validObject)
        {
            List<EditorObject> tempList = new List<EditorObject>();
            tempList.AddRange(Level.FindByType<EditorObject>());
            var sorted = tempList.OrderBy(item => (point - item.GetWorldTransform().Position).Length).ToList();
            for (int i = 0; i < sorted.Count; i++)
            {
                if (validObject.Invoke(sorted[i]))
                {
                    return sorted[i];
                }
            }
            return null;
        }

        public void ScenePlay()
        {
            _isPaused = false;
            if (ScenePlayed != null)
                ScenePlayed(this, Back);
        }

        public void ScenePause()
        {
            _isPaused = true;
            if (ScenePaused != null)
                ScenePaused(this, Back);
        }

        public void SceneStop()
        {
            _isPaused = true;
            if (SceneStopped != null)
                SceneStopped(this, Back);
        }

        public void AddAction(Action action)
        {
            lock (_lockAction)
            {
                Actions.Enqueue(action);
            }
        }

        public override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        public override void OnResize(EventArgs e, System.Drawing.Size canvasSize)
        {
            base.OnResize(e, canvasSize);
            Back.ActiveCamera.Aspect = CanvasSize.Width / (float)CanvasSize.Height;
            Hud.ActiveCamera.Aspect = CanvasSize.Width / (float)CanvasSize.Height;
            Hud.ActiveCamera.Scale = canvasSize.Height;
        }
    }
}
