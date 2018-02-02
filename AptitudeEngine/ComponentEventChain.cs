using AptitudeEngine.Events;
using AptitudeEngine.Enums;

namespace AptitudeEngine
{
    public abstract class ComponentEventChain
    {
        internal bool Awoken { get; private set; }
        internal bool Started { get; private set; }

        /// <summary>
        /// Called immediately after instantiation.
        /// </summary>
        public virtual void Awake() { }

        /// <summary>
        /// Called at the start of the first frame after being awoken.
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Called before main update.
        /// </summary>
        public virtual void PreUpdate() { }
        /// <summary>
        /// Called at main update.
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// Called after main update.
        /// </summary>
        public virtual void PostUpdate() { }
        /// <summary>
        /// Called before main render. Use <see cref="ScreenHandler"/> to draw to the screen.
        /// </summary>
        /// <param name="a"><see cref="FrameEventArgs"/> containing delta since the last frame.</param>
        public virtual void PreRender(FrameEventArgs a) { }
        /// <summary>
        /// Called at main render. Use <see cref="ScreenHandler"/> to draw to the screen.
        /// </summary>
        /// <param name="a"><see cref="FrameEventArgs"/> containing delta since the last frame.</param>
        public virtual void Render(FrameEventArgs a) { }
        /// <summary>
        /// Called after main render. Use <see cref="ScreenHandler"/> to draw to the screen.
        /// </summary>
        /// <param name="a"><see cref="FrameEventArgs"/> containing delta since the last frame.</param>
        public virtual void PostRender(FrameEventArgs a) { }
        /// <summary>
        /// Called when a mouse button goes down over the transform of the <see cref="AptComponent.Owner"/>.
        /// </summary>
        /// <param name="e">The MouseButton that went down.</param>
        public virtual void MouseDown(MouseButtonEventArgs e) { }
        /// <summary>
        /// Called when a mouse button goes up over the transform of the <see cref="AptComponent.Owner"/>.
        /// </summary>
        /// <param name="e">The MouseButton that went up.</param>
        public virtual void MouseUp(MouseButtonEventArgs e) { }
        /// <summary>
        /// Called when a mouse button clicks over the transform of the <see cref="AptComponent.Owner"/>. Clicking is when the mouse goes down and up again within 750 milliseconds.
        /// </summary>
        /// <param name="e">The mouse button that clicked.</param>
        public virtual void MouseClick(MouseButtonEventArgs e) { }
        public bool MouseStateDown { get; private set; }

        internal void InternalAwake()
        {
            Awake();
            Awoken = true;
        }
        internal void InternalStart()
        {
            Start();
            Started = true;
        }
        internal void InternalPreUpdate() => PreUpdate();
        internal void InternalUpdate() => Update();
        internal void InternalPostUpdate() => PostUpdate();
        internal void InternalPreRender(FrameEventArgs a) => PreRender(a);
        internal void InternalRender(FrameEventArgs a) => Render(a);
        internal void InternalPostRender(FrameEventArgs a) => PostRender(a);
        internal void InternalMouseDown(MouseButtonEventArgs e)
        {
            MouseStateDown = true;
            MouseDown(e);
        }
        internal void InternalMouseUp(MouseButtonEventArgs e)
        {
            MouseStateDown = false;
            MouseUp(e);
        }
        internal void InternalMouseClick(MouseButtonEventArgs e) => MouseClick(e);
    }
}