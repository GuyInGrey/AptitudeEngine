using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.ComponentModel;
using OpenTK.Graphics.OpenGL;
using AptitudeEngine.Enums;
using AptitudeEngine.Events;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Logger;

namespace AptitudeEngine
{
    public class AptContext : IDisposable
    {
        /// <summary>
        /// All the <see cref="AptObject"/>s in 1 table with a Guid each.
        /// </summary>
        internal Dictionary<string, AptObject> objectTable;

        /// <summary>
        /// The hierarchy of <see cref="AptObject"/>
        /// </summary>
        private List<AptObject> hierarchy;

        /// <summary>
        /// All the <see cref="AptObject"/> that have been created but not initialized yet.
        /// </summary>
        private List<AptObject> toBeInitialized;

        /// <summary>
        /// The <see cref="OpenTK.GameWindow"/> that is being used for this context.
        /// </summary>
        private readonly OpenTK.GameWindow gameWindow;

        /// <summary>
        /// The frames that have been drawn since the last time the framerate had been updated to the console.
        /// </summary>
        private int frameCount;

        /// <summary>
        /// The time since the last time the framerate had been updated to the console.
        /// </summary>
        private float timeSinceLastFrameLog;

        /// <summary>
        /// The <see cref="AptInput"/> for the context.
        /// </summary>
        public AptInput Input { get; }

        /// <summary>
        /// The currently active <see cref="Camera"/> in the context.
        /// </summary>
        public Camera MainCamera { get; internal set; }

        /// <summary>
        /// Whether the context has been disposed or not.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// The time since the last frame rendered.
        /// </summary>
        public float DeltaTime { get; private set; }
        
        /// <summary>
        /// Whether the standard Windows cursor is being drawn.
        /// </summary>
        public bool ShowCursor
        {
            set => gameWindow.CursorVisible = value;
            get => gameWindow.CursorVisible;
        }

        /// <summary>
        /// Exit the engine context safely.
        /// </summary>
        public void Exit() => gameWindow.Close();

        /// <summary>
        /// The size of <see cref="gameWindow"/> in pixels.
        /// </summary>
        public Vector2 WindowPixelSize { get; private set; }

        /// <summary>
        /// The color used for the clearing the background of each new frame.
        /// </summary>
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

        private bool _CustomCursor = false;
        public bool CustomCursor
        {
            get => _CustomCursor;
            set
            {
                _CustomCursor = value;

                if (value)
                {
                    Bitmap b;

                    try
                    {
                        b = new Bitmap(CustomCursorPath);
                    }
                    catch
                    {
                        _CustomCursor = false;
                        return;
                    }

                    var size = b.Size;
                    var bytes = new byte[size.Width * size.Height * 4];

                    var index = 0;
                    for (var y = 0; y < size.Height; y++)
                    {
                        for (var x = 0; x < size.Width; x++)
                        {
                            var c = b.GetPixel(x,y);
                            bytes[index] = (byte)c.B;
                            index++;
                            bytes[index] = (byte)c.G;
                            index++;
                            bytes[index] = (byte)c.R;
                            index++;
                            bytes[index] = (byte)c.A;
                            index++;
                        }
                    }

                    gameWindow.Cursor = new OpenTK.MouseCursor(0, 0, size.Width, size.Height, bytes);
                }
                else
                {
                    gameWindow.Cursor = OpenTK.MouseCursor.Default;
                }
            }
        }

        public string CustomCursorPath { get; set; }

        #region Constructors

        /// <summary>
        /// Initialize a new Aptitude Context, capturing a OpenTK context internally, using <see cref="GraphicsMode.Default"/>.
        /// </summary>
        /// <param name="title">The title of the window that is associated with this context.</param>
        /// <param name="width">The width of the window in pixels.</param>
        /// <param name="height">The height of the window in pixels.</param>
        /// <param name="maxUpdates">The maximum number of times per second that a logic update should occur.</param>
        /// <param name="maxFrames">The maximum number of times per second that a frame render should occur.</param>
        /// <param name="bufferMode">The vsync mode of the window buffer. See <see cref="VSyncMode"/>,</param>
        /// <param name="flags">Flags determining if the window should be in a windowed or fullscreen state. See <see cref="GameWindowFlags"/>,</param>
        /// <param name="display">The display that the game window should put focus on rendering to. See <see cref="DisplayIndex"/>,</param>
        public AptContext(
            string title = "",
            int width = 800,
            int height = 600,
            double maxUpdates = 60,
            double maxFrames = 60,
            VSyncMode bufferMode = VSyncMode.Off,
            GameWindowFlags flags = GameWindowFlags.FixedWindow,
            DisplayIndex display = DisplayIndex.Primary
        ) : this(
            GraphicsMode.Default,
            title,
            width,
            height,
            maxUpdates,
            maxFrames,
            bufferMode,
            flags,
            display) { }

        /// <summary>
        /// Initialize a new Aptitude Context, capturing a OpenTK context internally.
        /// </summary>
        /// <param name="graphicsMode"></param>
        /// <param name="title">The title of the window that is associated with this context.</param>
        /// <param name="width">The width of the window in pixels.</param>
        /// <param name="height">The height of the window in pixels.</param>
        /// <param name="maxUpdates">The number of times per second that an update should occur.</param>
        /// <param name="maxFrames">The number of times per second that a render should occur.</param>
        /// <param name="bufferMode">The vsync mode of the window buffer. See <see cref="VSyncMode"/>,</param>
        /// <param name="flags">Flags determining if the window should be in a windowed or fullscreen state. See <see cref="GameWindowFlags"/>,</param>
        /// <param name="display">The display that the game window should put focus on rendering to. See <see cref="DisplayIndex"/>,</param>
        public AptContext(
            GraphicsMode graphicsMode,
            string title = "",
            int width = 800,
            int height = 600,
            double maxUpdates = 60,
            double maxFrames = 60,
            VSyncMode bufferMode = VSyncMode.Off,
            GameWindowFlags flags = GameWindowFlags.FixedWindow,
            DisplayIndex display = DisplayIndex.Primary
        )
        {
            //Begin Logging
            LoggingHandler.Boot();
            //Setting WindowPixelSize to the gameWindow width and height.
            WindowPixelSize = new Vector2(width, height);

            //Starting the gameWindow with the parameters.
            gameWindow = new OpenTK.GameWindow(
                width,
                height,
                graphicsMode,
                title,
                (OpenTK.GameWindowFlags) flags,
                OpenTK.DisplayDevice.GetDisplay((OpenTK.DisplayIndex) display))
            {
                VSync = (OpenTK.VSyncMode) bufferMode,
                TargetRenderFrequency = maxFrames,
                TargetUpdateFrequency = maxUpdates,
            };

            //Events
            Load += GameContext_Load;
            UpdateFrame += GameContext_UpdateFrame;
            RenderFrame += GameContext_RenderFrame;
            Unload += GameContext_Unload;

            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.MouseWheel += GameWindow_MouseWheel;
            gameWindow.KeyDown += GameWindow_KeyDown;
            gameWindow.KeyPress += GameWindow_KeyPress;
            gameWindow.KeyUp += GameWindow_KeyUp;
            gameWindow.MouseMove += GameWindow_MouseMove;
            gameWindow.MouseDown += GameWindow_MouseDown;
            gameWindow.MouseUp += GameWindow_MouseUp;

            // Initialize lists and dictionaries
            hierarchy = new List<AptObject>();
            toBeInitialized = new List<AptObject>();
            objectTable = new Dictionary<string, AptObject>();
            
            //Begin Input management
            Input = new AptInput(this);
        }

        #endregion

        /// <summary>
        /// When a mouse button goes up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_MouseUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
            => MouseUp?.Invoke(sender, e);

        /// <summary>
        /// When a mouse button goes down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_MouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
            => MouseDown?.Invoke(sender, e);

        /// <summary>
        /// When the mouse moves over the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_MouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
            => MouseMove?.Invoke(sender, e);

        /// <summary>
        /// When a key is depressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
            => KeyUp?.Invoke(sender, e);

        /// <summary>
        /// When a key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_KeyPress(object sender, OpenTK.KeyPressEventArgs e)
            => KeyPress?.Invoke(sender, e);

        /// <summary>
        /// When a key is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
            => KeyDown?.Invoke(sender, e);

        /// <summary>
        /// When the mouse wheel gets scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_MouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
            => MouseWheel?.Invoke(sender, e);

        /// <summary>
        /// When an UpdateFrame occurs in <see cref="gameWindow"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_UpdateFrame(object sender, OpenTK.FrameEventArgs e)
            => UpdateFrame?.Invoke(sender, e);

        /// <summary>
        /// When an RenderFrame occurs in <see cref="gameWindow"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_RenderFrame(object sender, OpenTK.FrameEventArgs e)
            => RenderFrame?.Invoke(sender, e);

        /// <summary>
        /// When the context is loaded, start all game objects.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameContext_Load(object sender, EventArgs e) =>
            // Initialize anything context sensitive
            RecurseGameObjects(go => go.InternalStart());

        /// <summary>
        /// When an UpdateFrame occurs in <see cref="gameWindow"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// When an RenderFrame occurs in <see cref="gameWindow"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameContext_RenderFrame(object sender, FrameEventArgs e)
        {
            PreRenderFrame?.Invoke(this, e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            DeltaTime = (float) e.Delta;
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

        /// <summary>
        /// When the context is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameContext_Unload(object sender, EventArgs e) =>
            Dispose();

        /// <summary>
        /// Loop through all gameobjects in heirarchy.
        /// </summary>
        /// <param name="action"></param>
        internal void RecurseGameObjects(Action<AptObject> action)
            => RecurseGameObjects(hierarchy.ToArray(), action);

        /// <summary>
        /// Loop through all gameobjects in heirarchy.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="action"></param>
        private void RecurseGameObjects(AptObject[] objects, Action<AptObject> action)
        {
            for (var i = 0; i < objects.Length; i++)
            {
                var go = objects[i];
                action(go);
                RecurseGameObjects(go.Children, action);
            }
        }

        /// <summary>
        /// Start rendering the window.
        /// </summary>
        public void Begin()
            => gameWindow.Run();

        /// <summary>
        /// Create a new <see cref="AptObject"/>.
        /// </summary>
        /// <returns></returns>
        public AptObject Instantiate()
            => Instantiate(null);

        /// <summary>
        /// Create a new <see cref="AptObject"/>.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find the <see cref="AptObject"/> by it's Guid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public AptObject FindObjectByGuid(string guid)
            => objectTable.TryGetValue(guid, out var go) ? go : null;

        /// <summary>
        /// Find the <see cref="AptObject"/> by it's name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AptObject FindObjectWithName(string name)
            => objectTable.Values.FirstOrDefault(go => name.Equals(go.Name));

        /// <summary>
        /// Find the <see cref="AptObject"/>s by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AptObject[] FindObjectsWithName(string name)
            => objectTable.Values.Where(go => name.Equals(go.Name)).ToArray();

        /// <summary>
        /// Add an AptObject to the base of the hierarchy.
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Dispose the context.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the context.
        /// </summary>
        /// <param name="disposing"></param>
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
        
        //Event Handlers

        public event EventHandler<EventArgs> Load
        {
            add => gameWindow.Load += value;
            remove => gameWindow.Load -= value;
        }
        
        public event EventHandler<FrameEventArgs> PreRenderFrame;

        public event EventHandler<FrameEventArgs> RenderFrame;

        public event EventHandler<FrameEventArgs> PostRenderFrame;

        public event EventHandler<FrameEventArgs> PreUpdateFrame;

        public event EventHandler<FrameEventArgs> UpdateFrame;

        public event EventHandler<FrameEventArgs> PostUpdateFrame;

        public event EventHandler<EventArgs> Unload
        {
            add => gameWindow.Unload += value;
            remove => gameWindow.Unload -= value;
        }

        public event EventHandler<MouseWheelEventArgs> MouseWheel;

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

        public event EventHandler<KeyboardKeyEventArgs> KeyDown;

        public event EventHandler<KeyPressEventArgs> KeyPress;

        public event EventHandler<KeyboardKeyEventArgs> KeyUp;

        public event EventHandler<MouseMoveEventArgs> MouseMove;

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

        public event EventHandler<MouseButtonEventArgs> MouseDown;

        public event EventHandler<MouseButtonEventArgs> MouseUp;

        public event EventHandler<EventArgs> Move
        {
            add => gameWindow.Move += value;
            remove => gameWindow.Move -= value;
        }
    }
}