using OpenTK;
using OpenTK.Graphics.OpenGL;
using AptitudeEngine;

namespace AptitudeEngine.Components
{
    public class Camera : AptComponent
    {
        private Matrix4 projection;

        public void SetPosition(Vector2 pos) =>
            SetPosition(pos.X, pos.Y);

        public void SetPosition(float x, float y)
        {
            Transform.Position = new Vector2(x, y);
            GL.LoadMatrix(ref projection);
            GL.Translate(new Vector3(-x, y, 0));
        }

        public void Move(float x, float y)
            => SetPosition(Transform.Position.X + x, Transform.Position.Y + y);

        public void Move(Vector2 vec)
            => Move(vec.X, vec.Y);

        public override void Awake()
        {
            projection = Matrix4.CreateOrthographic(Transform.Size.X, -Transform.Size.Y, 0f, 100f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.Translate(Transform.Position);
        }

        public void SetAsMain()
            => Context.MainCamera = this;
    }
}