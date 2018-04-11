using System;
using System.Drawing;
using AptitudeEngine.Enums;
using AptitudeEngine.Logger;

namespace AptitudeEngine
{
    public static class DebugHandler
    {
        #region AE_DEBUG_CODES
        public static bool BorderTextures { get; set; } = false;

        public static bool CameraScroll { get; set; } = false;

        public static bool OriginLocator { get; set; } = false;

        public static bool StopCamSizeClamp { get; set; } = false;

        #endregion

        public static AptContext Context { get; private set; }

        public static void Boot(AptContext con)
        {
            AptInput.GlobalKeyUp += AptInput_GlobalKeyUp;
            Context = con;
            Context.RenderFrame += Context_RenderFrame;
        }

        private static void Context_RenderFrame(object sender, Events.FrameEventArgs e)
        {
            if (!StopCamSizeClamp)
            {
                var camSize = Context.ActiveCamera.Transform.Scale;
                Context.ActiveCamera.Transform.Scale = new Vector2(camSize.X.Clamp(0.00001f, 100000), camSize.Y.Clamp(0.00001f, 100000));
            }
        }

        private static void AptInput_GlobalKeyUp(object sender, Events.KeyboardKeyEventArgs e)
        {
            if (Context.Input.GetKeyDown(InputCode.ControlLeft) && Context.Input.GetKeyDown(InputCode.F1))
            {
                BorderTextures = !BorderTextures;
                LoggingHandler.Log("DEBUG-OPTIONS: `bordertextures` is now " + BorderTextures, LogMessageType.Warning);
            }
            if (Context.Input.GetKeyDown(InputCode.ControlLeft) && Context.Input.GetKeyDown(InputCode.F2))
            {
                CameraScroll = !CameraScroll;
                LoggingHandler.Log("DEBUG-OPTIONS: `camerascroll` is now " + CameraScroll, LogMessageType.Warning);
            }
            if (Context.Input.GetKeyDown(InputCode.ControlLeft) && Context.Input.GetKeyDown(InputCode.F3))
            {
                OriginLocator = !OriginLocator;
                LoggingHandler.Log("DEBUG-OPTIONS: `originlocator` is now " + OriginLocator, LogMessageType.Warning);
            }
            if (Context.Input.GetKeyDown(InputCode.ControlLeft) && Context.Input.GetKeyDown(InputCode.F4))
            {
                StopCamSizeClamp = !StopCamSizeClamp;
                LoggingHandler.Log("DEBUG-OPTIONS: `stopcamsizeclamp` is now " + StopCamSizeClamp, LogMessageType.Warning);
            }
        }
    }
}