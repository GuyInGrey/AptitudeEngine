using System;
using System.Collections.Generic;
using System.Linq;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Enums;
using AptitudeEngine.Events;

namespace AptitudeEngine
{
    public class AptInput : IDisposable
    {
        private Dictionary<InputCode, InputState> keyStates;
        private OrderedHashSet<InputCode> keysWaitingDown;
        private OrderedHashSet<InputCode> keysWaitingUp;

        public Vector2 MouseScreenPosition { get; private set; } = Vector2.Zero;
        public Vector2 MouseScreenPixelPosition { get; private set; } = Vector2.Zero;
        public Vector2 MouseWorldPosition { get; private set; } = Vector2.Zero;
 
        private AptContext context;
        private bool disposed;

        public AptInput(AptContext aptContext)
        {
            context = aptContext;
            context.KeyDown += Context_KeyDown;
            context.KeyUp += Context_KeyUp;
            context.MouseDown += Context_MouseDown;
            context.MouseUp += Context_MouseUp;
            context.PreUpdateFrame += Context_PreUpdateFrame;
            context.MouseMove += Context_MouseMove;

            keyStates = new Dictionary<InputCode, InputState>();
            foreach (var key in Enum.GetValues(typeof(InputCode)).Cast<InputCode>())
            {
                keyStates.Add(key, InputState.Up);
            }

            keysWaitingDown = new OrderedHashSet<InputCode>();
            keysWaitingUp = new OrderedHashSet<InputCode>();
        }

        private void Context_MouseMove(object sender, MouseMoveEventArgs e)
        {
            MouseScreenPixelPosition = new Vector2(e.X, e.Y);

            var mouseXPercent = e.X / context.WindowPixelSize.X;
            var mouseYPercent = e.Y / context.WindowPixelSize.Y;

            MouseWorldPosition = new Vector2((mouseXPercent - 0.5f) + context.MainCamera.Transform.Position.X,
                (mouseYPercent - 0.5f) + context.MainCamera.Transform.Position.Y);
            MouseScreenPosition = new Vector2((mouseXPercent - 0.5f), (mouseYPercent - 0.5f));
        }

        private void Context_PreUpdateFrame(object sender, FrameEventArgs e)
        {
            var alter = new Dictionary<InputCode, InputState>();

            // If the state of a key was already DownThisFrame or UpThisFrame state since the last frame
            // then we should push it to the non-ThisFrame equivelent, indicating that it wasnt just pressed
            // this frame, but has been pressed over multiple frames.
            foreach (var kvp in keyStates)
            {
                switch (kvp.Value)
                {
                    case InputState.DownThisFrame:
                        alter.Add(kvp.Key, InputState.Down);
                        break;
                    case InputState.UpThisFrame:
                        alter.Add(kvp.Key, InputState.Up);
                        break;
                }
            }

            foreach (var kvp in alter)
            {
                keyStates[kvp.Key] = kvp.Value;
            }

            alter.Clear();
            alter = null;

            // the following two iterations check to see if a key has been queued to be Up or Down
            // since the last frame. Because they'd be set this frame, they're going to be set to the
            // ThisFrame versions of their respective states.
            // Keys that are waiting to be Up wont change the state of keys that are already Up or UpThisFrame
            // and keys that are waiting to be Down wont change the state of keys that are already Down or DownThisFrame.
            for (var i = 0; i < keysWaitingUp.Count; i++)
            {
                var key = keysWaitingUp[i];

                switch (keyStates[key])
                {
                    case InputState.Down:
                    case InputState.DownThisFrame:
                        keyStates[key] = InputState.UpThisFrame;
                        break;
                }

                keysWaitingUp.RemoveAt(i);
                i--;
            }

            for (var i = 0; i < keysWaitingDown.Count; i++)
            {
                var key = keysWaitingDown[i];

                switch (keyStates[key])
                {
                    case InputState.Up:
                    case InputState.UpThisFrame:
                        keyStates[key] = InputState.DownThisFrame;
                        break;
                }

                keysWaitingDown.RemoveAt(i);
                i--;
            }

            if (GetKeyDown(InputCode.AltLeft) && GetKeyDown(InputCode.F4))
            {
                context.Exit();
            }
        }

        private void Context_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            // if the key is pressed and then lifted in a single frame
            // then ensure that it wont be processed as being down
            // and queue it to be processed as being up.
            var key = e.Key;
            keysWaitingDown.TryRemove(key);
            keysWaitingUp.TryAdd(key);
        }

        private void Context_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            // if the key is lifted and then pressed in a single frame
            // then ensure that it wont be processed as being up
            // and queue it to be processed as being down.
            var key = e.Key;
            keysWaitingUp.TryRemove(key);
            keysWaitingDown.TryAdd(key);
        }

        DateTime downTime;
        private void Context_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var key = e.Key;

            // if the button is lifted and then pressed in a single frame
            // then ensure that it wont be processed as being up
            // and queue it to be processed as being down.
            keysWaitingUp.TryRemove(key);
            keysWaitingDown.TryAdd(key);
            downTime = DateTime.Now;

            //Tell all AptObjects, and therefore AptComponents, that a mouse button went up.
            context.RecurseGameObjects(delegate (AptObject i)
            {
                var rec = new AptRectangle(i.TotalPosition, i.Transform.Size);

                if (rec.ContainsVector(MouseWorldPosition))
                {
                    i.MouseDown(e);
                }
            });
        }

        private void Context_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var key = e.Key;

            //If time between the mouse button going down and going up is less than 750 ms, count it as a click.
            if (downTime != null)
            {
                var a = DateTime.Now - downTime;
                if (a.TotalMilliseconds < 750)
                {
                    Context_MouseClick(e);
                }
            }

            // if the button is pressed and then lifted in a single frame
            // then ensure that it wont be processed as being down
            // and queue it to be processed as being up.
            keysWaitingDown.TryRemove(key);
            keysWaitingUp.TryAdd(key);

            //Tell all AptObjects, and therefore AptComponents, that a mouse button went up.
            context.RecurseGameObjects(delegate (AptObject i)
            {
                var rec = new AptRectangle(i.TotalPosition, i.Transform.Size);

                if (rec.ContainsVector(MouseWorldPosition))
                {
                    i.MouseUp(e);
                }
            });
        }

        private void Context_MouseClick(MouseButtonEventArgs e) =>
            //Tell all AptObjects, and therefore AptComponents, that a mouse button went up.
            context.RecurseGameObjects(delegate (AptObject i)
            {
                var rec = new AptRectangle(i.TotalPosition, i.Transform.Size);

                if (rec.ContainsVector(MouseWorldPosition))
                {
                    i.MouseClick(e);
                }
            });

        /// <summary>
        /// Gets whether the state of <paramref name="key"/> is <see cref="InputState.Down"/> or <see cref="InputState.DownThisFrame"/> or not.
        /// </summary>
        /// <param name="key">The key to check the down state of.</param>
        /// <returns>
        /// <para>True if the state of <paramref name="key"/> is <see cref="InputState.Down"/> or <see cref="InputState.DownThisFrame"/>.</para>
        /// 
        /// <para>False otherwise.</para>
        /// </returns>
        public bool GetKeyDown(InputCode key)
        {
            var state = GetKeyState(key);
            return state == InputState.Down || state == InputState.DownThisFrame;
        }

        /// <summary>
        /// Gets whether the state of <paramref name="key"/> is <see cref="InputState.Up"/> or <see cref="InputState.UpThisFrame"/> or not.
        /// </summary>
        /// <param name="key">The key to check the up state of.</param>
        /// <returns>
        /// <para>True if the state of <paramref name="key"/> is <see cref="InputState.Up"/> or <see cref="InputState.UpThisFrame"/>.</para>
        /// 
        /// <para>False otherwise.</para>
        /// </returns>
        public bool GetKeyUp(InputCode key)
        {
            var state = GetKeyState(key);
            return state == InputState.Up || state == InputState.UpThisFrame;
        }

        /// <summary>
        /// Gets whether the state of <paramref name="key"/> is <see cref="InputState.DownThisFrame"/> or not.
        /// </summary>
        /// <param name="key">The key to check the up state of.</param>
        /// <returns>
        /// <para>True if the state of <paramref name="key"/> is <see cref="InputState.DownThisFrame"/>.</para>
        /// 
        /// <para>False otherwise.</para>
        /// </returns>
        public bool GetKeyDownThisFrame(InputCode key)
            => GetKeyState(key) == InputState.DownThisFrame;

        /// <summary>
        /// Gets whether the state of <paramref name="key"/> is <see cref="InputState.UpThisFrame"/> or not.
        /// </summary>
        /// <param name="key">The key to check the up state of.</param>
        /// <returns>
        /// <para>True if the state of <paramref name="key"/> is <see cref="InputState.UpThisFrame"/>.</para>
        /// 
        /// <para>False otherwise.</para>
        /// </returns>
        public bool GetKeyUpThisFrame(InputCode key)
            => GetKeyState(key) == InputState.UpThisFrame;

        /// <summary>
        /// Gets the state of <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key to get the state of.</param>
        /// <returns>The state of <paramref name="key"/></returns>
        public InputState GetKeyState(InputCode key)
            => keyStates[key];

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                context.KeyDown -= Context_KeyDown;
                context.KeyUp -= Context_KeyUp;
                context.MouseUp -= Context_MouseUp;
                context.MouseDown -= Context_MouseDown;
                context.PreUpdateFrame -= Context_PreUpdateFrame;
                keyStates.Clear();
                keysWaitingDown.Clear();
                keysWaitingUp.Clear();
            }

            context = null;
            keyStates = null;
            keysWaitingDown = null;
            keysWaitingUp = null;

            disposed = true;
        }
    }
}
