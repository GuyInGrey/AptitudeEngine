using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AptitudeEngine.Enums;

namespace AptitudeEngine
{
	public class AptInput : IDisposable
	{
		private Dictionary<KeyCode, KeyState> keyStates;
		private OrderedHashSet<KeyCode> keysWaitingDown;
		private OrderedHashSet<KeyCode> keysWaitingUp;
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

			keyStates = new Dictionary<KeyCode, KeyState>();
			foreach (var key in Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
			{
				keyStates.Add(key, KeyState.Up);
			}

			keysWaitingDown = new OrderedHashSet<KeyCode>();
			keysWaitingUp = new OrderedHashSet<KeyCode>();
		}

		private void Context_PreUpdateFrame(object sender, OpenTK.FrameEventArgs e)
		{
			var alter = new Dictionary<KeyCode, KeyState>();

			// If the state of a key was already DownThisFrame or UpThisFrame state since the last frame
			// then we should push it to the non-ThisFrame equivelent, indicating that it wasnt just pressed
			// this frame, but has been pressed over multiple frames.
			foreach (var kvp in keyStates)
			{
				switch (kvp.Value)
				{
					case KeyState.DownThisFrame:
						alter.Add(kvp.Key, KeyState.Down);
						break;
					case KeyState.UpThisFrame:
						alter.Add(kvp.Key, KeyState.Up);
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
					case KeyState.Down:
					case KeyState.DownThisFrame:
						keyStates[key] = KeyState.UpThisFrame;
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
					case KeyState.Up:
					case KeyState.UpThisFrame:
						keyStates[key] = KeyState.DownThisFrame;
						break;
				}

				keysWaitingDown.RemoveAt(i);
				i--;
			}
		}

		private void Context_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			// if the key is pressed and then lifted in a single frame
			// then ensure that it wont be processed as being down
			// and queue it to be processed as being up.
			var key = (KeyCode)e.Key;
			keysWaitingDown.TryRemove(key);
			keysWaitingUp.TryAdd(key);
		}

		private void Context_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			// if the key is lifted and then pressed in a single frame
			// then ensure that it wont be processed as being up
			// and queue it to be processed as being down.
			var key = e.Key.ToApt();
			keysWaitingUp.TryRemove(key);
			keysWaitingDown.TryAdd(key);
		}

		private void Context_MouseUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
		{
			// if the button is pressed and then lifted in a single frame
			// then ensure that it wont be processed as being down
			// and queue it to be processed as being up.
			var key = e.Button.ToApt();
			keysWaitingDown.TryRemove(key);
			keysWaitingUp.TryAdd(key);
		}

		private void Context_MouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
		{
			// if the button is lifted and then pressed in a single frame
			// then ensure that it wont be processed as being up
			// and queue it to be processed as being down.
			var key = e.Button.ToApt();
			keysWaitingUp.TryRemove(key);
			keysWaitingDown.TryAdd(key);
		}

		/// <summary>
		/// Gets whether the state of <paramref name="key"/> is <see cref="KeyState.Down"/> or <see cref="KeyState.DownThisFrame"/> or not.
		/// </summary>
		/// <param name="key">The key to check the down state of.</param>
		/// <returns>
		/// <para>True if the state of <paramref name="key"/> is <see cref="KeyState.Down"/> or <see cref="KeyState.DownThisFrame"/>.</para>
		/// 
		/// <para>False otherwise.</para>
		/// </returns>
		public bool GetKeyDown(KeyCode key)
		{	
			var state = GetKeyState(key);
			return state == KeyState.Down || state == KeyState.DownThisFrame;
		}

		/// <summary>
		/// Gets whether the state of <paramref name="key"/> is <see cref="KeyState.Up"/> or <see cref="KeyState.UpThisFrame"/> or not.
		/// </summary>
		/// <param name="key">The key to check the up state of.</param>
		/// <returns>
		/// <para>True if the state of <paramref name="key"/> is <see cref="KeyState.Up"/> or <see cref="KeyState.UpThisFrame"/>.</para>
		/// 
		/// <para>False otherwise.</para>
		/// </returns>
		public bool GetKeyUp(KeyCode key)
		{
			var state = GetKeyState(key);
			return state == KeyState.Up || state == KeyState.UpThisFrame;
		}

		/// <summary>
		/// Gets whether the state of <paramref name="key"/> is <see cref="KeyState.DownThisFrame"/> or not.
		/// </summary>
		/// <param name="key">The key to check the up state of.</param>
		/// <returns>
		/// <para>True if the state of <paramref name="key"/> is <see cref="KeyState.DownThisFrame"/>.</para>
		/// 
		/// <para>False otherwise.</para>
		/// </returns>
		public bool GetKeyDownThisFrame(KeyCode key)
			=> GetKeyState(key) == KeyState.DownThisFrame;

		/// <summary>
		/// Gets whether the state of <paramref name="key"/> is <see cref="KeyState.UpThisFrame"/> or not.
		/// </summary>
		/// <param name="key">The key to check the up state of.</param>
		/// <returns>
		/// <para>True if the state of <paramref name="key"/> is <see cref="KeyState.UpThisFrame"/>.</para>
		/// 
		/// <para>False otherwise.</para>
		/// </returns>
		public bool GetKeyUpThisFrame(KeyCode key)
			=> GetKeyState(key) == KeyState.UpThisFrame;

		/// <summary>
		/// Gets the state of <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The key to get the state of.</param>
		/// <returns>The state of <paramref name="key"/></returns>
		public KeyState GetKeyState(KeyCode key)
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
