using AptitudeEngine.Enums;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AptitudeEngine.Components.Visuals
{
    public class Camera : AptComponent
    {
        private Matrix4 projection;
        public bool ArrowMovement { get; set; }
        public float ArrowMovementSpeed { get; set; } = 5f;

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

        public override void Render(Events.FrameEventArgs a)
        {
            if (ArrowMovement)
            {
                if (Input.GetKeyDown(InputCode.Right))
                {
                    Move(0.001f * ArrowMovementSpeed, 0f);
                }
                if (Input.GetKeyDown(InputCode.Left))
                {
                    Move(-0.001f * ArrowMovementSpeed, 0f);
                }
                if (Input.GetKeyDown(InputCode.Up))
                {
                    Move(0f, -0.001f * ArrowMovementSpeed);
                }
                if (Input.GetKeyDown(InputCode.Down))
                {
                    Move(0f, 0.001f * ArrowMovementSpeed);
                }
            }
        }

        public void SetAsMain()
            => Context.MainCamera = this;
    }
}