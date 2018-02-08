using AptitudeEngine.Enums;
using AptitudeEngine.Events;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AptitudeEngine.Components.Visuals
{
    public class Camera : AptComponent
    {
        /// <summary>
        /// The OpenGL Projection of the camera.
        /// </summary>
        private Matrix4 projection;

        private Vector2 lastPos = Vector2.Zero;

        /// <summary>
        /// Set the camera's position.
        /// </summary>
        /// <param name="pos">Position to set the camera to.</param>
        public void SetPosition(Vector2 pos) =>
            SetPosition(pos.X, pos.Y);

        /// <summary>
        /// Set the camera's position.
        /// </summary>
        /// <param name="x">The x position to set the camera to.</param>
        /// <param name="y">The y position to set the camera to.</param>
        public void SetPosition(float x, float y)
        {
            //Set the owner's transform to the new location.
            Transform.Position = new CoordinateSystem.Vector2(x, y);

            //Load this matrix as the current one.
            GL.LoadMatrix(ref projection);
            //Set the matrix position to owner's transform's position.
            GL.Translate(new Vector3(-Transform.Position.X, -Transform.Position.Y, 0));
        }

        public override void Awake()
        {
            projection = Matrix4.CreateOrthographic(Transform.Size.X, -Transform.Size.Y, 0f, 100f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.Translate(new Vector3(-Transform.Position.X, -Transform.Position.Y, 0));
        }

        public override void Render(Events.FrameEventArgs a)
        {
            projection = Matrix4.CreateOrthographic(Transform.Size.X, -Transform.Size.Y, 0f, 100f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.Translate(new Vector3(-Transform.Position.X, -Transform.Position.Y, 0));
        }

        /// <summary>
        /// Sets the camera as the context's active camera.
        /// </summary>
        public void SetAsActive()
            => Context.ActiveCamera = this;
    }
}