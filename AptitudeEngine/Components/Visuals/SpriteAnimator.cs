using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class SpriteAnimator : AptComponent
    {
        private float timeSinceLastAnimationFrame = 0;
        private int frameIndex = 0;

        public Animation Animation { get; set; }

        public override void Awake() => base.Awake();
        public override void Start() => base.Start();
        public override void PreRender(FrameEventArgs a) => base.PreRender(a);

        public override void Render(FrameEventArgs a)
        {
            var r = Owner.GetComponentOfType<SpriteRenderer>();
            if (r == null)
            {
                return;
            }

            if (Animation.Frames == null || Animation.Frames.Length == 0)
            {
                return;
            }

            timeSinceLastAnimationFrame += Owner.Context.DeltaTime;

            if (timeSinceLastAnimationFrame >= 1 / (float) Animation.FrameRate)
            {
                frameIndex++;
                timeSinceLastAnimationFrame = 0;
            }

            if (frameIndex >= Animation.Frames.Length)
            {
                frameIndex = 0;
            }

            var fp = Animation.Frames[frameIndex];
            r.Sprite.Frame = fp;
        }

        public override void PostRender(FrameEventArgs a) => base.PostRender(a);
    }
}