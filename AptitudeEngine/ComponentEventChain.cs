using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptitudeEngine.Assets;
using AptitudeEngine.Components;
using AptitudeEngine.Enums;
using AptitudeEngine.Events;
using AptitudeEngine.Logging;

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

        public virtual void PreUpdate() { }
        public virtual void Update() { }
        public virtual void PostUpdate() { }
        public virtual void PreRender(FrameEventArgs a) { }
        public virtual void Render(FrameEventArgs a) { }
        public virtual void PostRender(FrameEventArgs a) { }

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
    }
}