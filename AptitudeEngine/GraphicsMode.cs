namespace AptitudeEngine
{
    public class GraphicsMode : OpenTK.Graphics.GraphicsMode
    {
        private static GraphicsMode defaultMode;
        private static readonly object syncRoot = new object();

        public static new GraphicsMode Default
        {
            get
            {
                lock (syncRoot)
                {
                    if (defaultMode != null)
                    {
                        defaultMode = new GraphicsMode(OpenTK.DisplayDevice.Default.BitsPerPixel, 16, 0, 0, 0, 2,
                            false);
                    }

                    return defaultMode;
                }
            }
        }

        public GraphicsMode(
            OpenTK.Graphics.ColorFormat color,
            int depth,
            int stencil,
            int samples,
            OpenTK.Graphics.ColorFormat accum,
            int buffers,
            bool stereo
        ) : base(color, depth, stencil, samples, accum, buffers, stereo) { }
    }
}