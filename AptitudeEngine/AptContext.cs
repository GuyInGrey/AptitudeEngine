﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using AptitudeEngine.Assets;
using AptitudeEngine.Components;
using AptitudeEngine.Enums;
using AptitudeEngine.Logging;
using AptitudeEngine.Logging.Formatters;
using AptitudeEngine.Logging.Handlers;
using AptitudeEngine.Logging.Modules;

using LogLevel = AptitudeEngine.Logger.Level;

namespace AptitudeEngine
{
    public class AptContext : IDisposable
    {
        internal Dictionary<string, AptObject> objectTable;
        private List<AptObject> hierarchy;
        private List<AptObject> toBeInitialized;

        private readonly GameWindow gameWindow;
        private int frameCount;
        private float timeSinceLastFrameLog;

        public AptInput Input { get; }

        public Camera MainCamera { get; internal set; }

        public Color ClearColor
        {
            get
            {
                var bkColor = new float[3];
                GL.GetFloat(GetPName.ColorClearValue, bkColor);
                var red = int.Parse((255f * bkColor[0]).ToString("0"));
                var green = int.Parse((255f * bkColor[1]).ToString("0"));
                var blue = int.Parse((255f * bkColor[2]).ToString("0"));
                return Color.FromArgb(red, green, blue);
            }
            set => GL.ClearColor(value);
        }

        public bool Disposed { get; private set; }
        public float DeltaTime { get; private set; }

        public event EventHandler<EventArgs> Load
        {
            add => gameWindow.Load += value;
            remove => gameWindow.Load -= value;
        }

        public event EventHandler<FrameEventArgs> PreRenderFrame;

        public event EventHandler<FrameEventArgs> RenderFrame
        {
            add => gameWindow.RenderFrame += value;
            remove => gameWindow.RenderFrame -= value;
        }

        public event EventHandler<FrameEventArgs> PostRenderFrame;

        public event EventHandler<FrameEventArgs> PreUpdateFrame;

        public event EventHandler<FrameEventArgs> UpdateFrame
        {
            add => gameWindow.UpdateFrame += value;
            remove => gameWindow.UpdateFrame -= value;
        }

        public event EventHandler<FrameEventArgs> PostUpdateFrame;

        public event EventHandler<EventArgs> Unload
        {
            add => gameWindow.Unload += value;
            remove => gameWindow.Unload -= value;
        }

        public event EventHandler<MouseWheelEventArgs> MouseWheel
        {
            add => gameWindow.MouseWheel += value;
            remove => gameWindow.MouseWheel -= value;
        }

        public event EventHandler<EventArgs> Closed
        {
            add => gameWindow.Closed += value;
            remove => gameWindow.Closed -= value;
        }

        public event EventHandler<CancelEventArgs> Closing
        {
            add => gameWindow.Closing += value;
            remove => gameWindow.Closing -= value;
        }

        public event EventHandler<EventArgs> OnDisposed
        {
            add => gameWindow.Disposed += value;
            remove => gameWindow.Disposed -= value;
        }

        public event EventHandler<EventArgs> FocusedChanged
        {
            add => gameWindow.FocusedChanged += value;
            remove => gameWindow.FocusedChanged -= value;
        }

        public event EventHandler<EventArgs> IconChanged
        {
            add => gameWindow.IconChanged += value;
            remove => gameWindow.IconChanged -= value;
        }

        public event EventHandler<KeyboardKeyEventArgs> KeyDown
        {
            add => gameWindow.KeyDown += value;
            remove => gameWindow.KeyDown -= value;
        }

        public event EventHandler<KeyPressEventArgs> KeyPress
        {
            add => gameWindow.KeyPress += value;
            remove => gameWindow.KeyPress -= value;
        }

        public event EventHandler<KeyboardKeyEventArgs> KeyUp
        {
            add => gameWindow.KeyUp += value;
            remove => gameWindow.KeyUp -= value;
        }

        public event EventHandler<MouseMoveEventArgs> MouseMove
        {
            add => gameWindow.MouseMove += value;
            remove => gameWindow.MouseMove -= value;
        }

        public event EventHandler<EventArgs> MouseEnter
        {
            add => gameWindow.MouseEnter += value;
            remove => gameWindow.MouseEnter -= value;
        }

        public event EventHandler<EventArgs> MouseLeave
        {
            add => gameWindow.MouseLeave += value;
            remove => gameWindow.MouseLeave -= value;
        }

        public event EventHandler<EventArgs> Resize
        {
            add => gameWindow.Resize += value;
            remove => gameWindow.Resize -= value;
        }

        public event EventHandler<EventArgs> TitleChanged
        {
            add => gameWindow.TitleChanged += value;
            remove => gameWindow.TitleChanged -= value;
        }

        public event EventHandler<EventArgs> VisibleChanged
        {
            add => gameWindow.VisibleChanged += value;
            remove => gameWindow.VisibleChanged -= value;
        }

        public event EventHandler<EventArgs> WindowBorderChanged
        {
            add => gameWindow.WindowBorderChanged += value;
            remove => gameWindow.WindowBorderChanged -= value;
        }

        public event EventHandler<EventArgs> WindowStateChanged
        {
            add => gameWindow.WindowStateChanged += value;
            remove => gameWindow.WindowStateChanged -= value;
        }

        public event EventHandler<MouseButtonEventArgs> MouseDown
        {
            add => gameWindow.MouseDown += value;
            remove => gameWindow.MouseDown -= value;
        }

        public event EventHandler<MouseButtonEventArgs> MouseUp
        {
            add => gameWindow.MouseUp += value;
            remove => gameWindow.MouseUp -= value;
        }

        public event EventHandler<EventArgs> Move
        {
            add => gameWindow.Move += value;
            remove => gameWindow.Move -= value;
        }

        public AptContext(string title, int width, int height, double maxUpdates, double maxFrames, GraphicsMode graphicsMode, VSyncMode bufferMode, GameWindowFlags flags, DisplayDevice device)
        {
            Logger.LoggerHandlerManager
                .AddHandler(new ConsoleLoggerHandler(new DefaultLoggerFormatter()));

            Logger.Log<AptContext>("Setting up OpenTK Context and GameWindow.");
            gameWindow = new GameWindow(width, height, graphicsMode, title, flags, device)
            {
                VSync = bufferMode,
                TargetRenderFrequency = maxFrames,
                TargetUpdateFrequency = maxUpdates,
            };

            Load += GameContext_Load;
            UpdateFrame += GameContext_UpdateFrame;
            RenderFrame += GameContext_RenderFrame;
            Unload += GameContext_Unload;

            // Initialize anything not context sensitive.
            hierarchy = new List<AptObject>();
            toBeInitialized = new List<AptObject>();
            objectTable = new Dictionary<string, AptObject>();

            Logger.Log<AptContext>("Setting up input handling.");
            Input = new AptInput(this);
        }

        public void Begin()
            => gameWindow.Run();

        private void GameContext_Load(object sender, EventArgs e) =>
            // Initialize anything context sensitive
            RecurseGameObjects(go => go.InternalStart());

        private void GameContext_UpdateFrame(object sender, FrameEventArgs e)
        {
            PreUpdateFrame?.Invoke(this, e);

            // hierarchy is a shallow representation (1-level deep) of the actual object hierarchy.
            // so we know that if an object has a parent, it cant be at the root 
            // and thus should be removed from the hierarchy's root representation.
            // we also check for disposed objects since we dont want to reference
            // those anymore.
            for (var i = 0; i < hierarchy.Count; i++)
            {
                var obj = hierarchy[i];
                if (obj.Parent != null || obj.Disposed)
                {
                    hierarchy.RemoveAt(i);
                    i--;
                }
            }

            // we also dont want any disposed objects being referenced in our table, so lets clean that right up.
            var removalQueue = from val in objectTable.Values
                               where val.Disposed
                               select val.Guid;
            foreach (var item in removalQueue)
            {
                objectTable.Remove(item);
            }

            RecurseGameObjects(toBeInitialized.ToArray(), go => go.InternalStart());
            toBeInitialized.Clear();

            RecurseGameObjects(go => go.InternalPreUpdate());
            RecurseGameObjects(go => go.InternalUpdate());
            RecurseGameObjects(go => go.InternalPostUpdate());

            PostUpdateFrame?.Invoke(this, e);
        }

        private void GameContext_RenderFrame(object sender, FrameEventArgs e)
        {
            PreRenderFrame?.Invoke(this, e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            DeltaTime = (float)e.Time;
            timeSinceLastFrameLog += DeltaTime;

            if (timeSinceLastFrameLog > 1)
            {
                Console.Title = "Framerate: " + frameCount + " FPS";
                frameCount = 0;
                timeSinceLastFrameLog = 0;
            }

            RecurseGameObjects(go => go.InternalPreRender(e));
            RecurseGameObjects(go => go.InternalRender(e));

            GL.Flush();
            gameWindow.SwapBuffers();

            RecurseGameObjects(go => go.InternalPostRender(e));
            frameCount++;

            PostRenderFrame?.Invoke(this, e);
        }
        private void GameContext_Unload(object sender, EventArgs e) =>
            Dispose();

        private void RecurseGameObjects(Action<AptObject> action)
            => RecurseGameObjects(hierarchy.ToArray(), action);

        private void RecurseGameObjects(AptObject[] objects, Action<AptObject> action)
        {
            for (var i = 0; i < objects.Length; i++)
            {
                var go = objects[i];
                action(go);
                RecurseGameObjects(go.Children, action);
            }
        }

        public AptObject Instantiate()
            => Instantiate(null);

        public AptObject Instantiate(AptObject parent)
        {
            var go = new AptObject(this);

            if (parent != null)
            {
                go.SetParent(parent);
            }
            else
            {
                hierarchy.Add(go);
            }

            go.Transform.Size = new Vector2(1, 1);

            go.InternalAwake();

            toBeInitialized.Add(go);
            return go;
        }

        public AptObject FindObjectByGuid(string guid)
            => objectTable.TryGetValue(guid, out var go) ? go : null;

        public AptObject FindObjectWithName(string name)
            => objectTable.Values.FirstOrDefault(go => name.Equals(go.Name));

        public AptObject[] FindObjectsWithName(string name)
            => objectTable.Values.Where(go => name.Equals(go.Name)).ToArray();

        public void AddToHierarchyRoot(AptObject obj)
        {
            if (obj == null)
            {
                return;
            }

            if (obj.Parent != null)
            {
                obj.SetParent(null);
            }

            hierarchy.Add(obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                // dispose managed resources
                gameWindow.Dispose();
                Input.Dispose();

                if (hierarchy?.Count > 0)
                {
                    foreach (var go in hierarchy)
                    {
                        go.Dispose();
                    }
                    hierarchy.Clear();
                }

                objectTable?.Clear();
                toBeInitialized?.Clear();
            }

            // dispose unmanaged resources

            // set references to null
            MainCamera = null;
            objectTable = null;
            hierarchy = null;
            toBeInitialized = null;

            Disposed = true;
        }

        ~AptContext()
            => Dispose(false);
    }
}