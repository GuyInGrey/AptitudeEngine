using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AptitudeEngine.Components.Visuals
{
    public class Camera : AptComponent
    {
        private Matrix4 projection;

        public void SetPosition(Vector2 pos) =>
            SetPosition(pos.X, pos.Y);

        public void SetPosition(float x, float y)
        {
            Transform.Position = new CoordinateSystem.Vector2(Transform.Position.X + x, Transform.Position.Y + y);
            GL.LoadMatrix(ref projection);
            GL.Translate(new Vector3(-Transform.Position.X, -Transform.Position.Y, 0));
        }

        public void Move(float x, float y)
            => SetPosition(x, y);

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