using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class SpriteAnimator : AptComponent
    {
        /// <summary>
        /// The time that has passed since the last instance of the animation frame increasing by 1.
        /// </summary>
        private float timeSinceLastAnimationFrame = 0;

        /// <summary>
        /// The current frame index in the animtion.
        /// </summary>
        private int frameIndex = 0;
        
        /// <summary>
        /// The animation being used.
        /// </summary>
        public Animation Animation { get; set; }

        public override void Awake() => base.Awake();
        public override void Start() => base.Start();
        public override void PreRender(FrameEventArgs a) => base.PreRender(a);

        public override void Render(FrameEventArgs a)
        {
            //Find the sprite renderer. If there isn't a SpriteRenderer, return, because it can't draw nothing.
            var r = Owner.GetComponentOfType<SpriteRenderer>();
            if (r == null)
            {
                return;
            }

            //If the frames of the animation are null or empty, return, because there are no frames to change between.
            if (Animation.Frames == null || Animation.Frames.Length == 0)
            {
                return;
            }

            //Increase the time since the last animation frame change.
            timeSinceLastAnimationFrame += Owner.Context.DeltaTime;

            //If it is time to change frames, increase the frame index by 1, and reset the clock.
            if (timeSinceLastAnimationFrame >= 1 / (float) Animation.FrameRate)
            {
                frameIndex++;
                timeSinceLastAnimationFrame = 0;
            }

            //If the animation is at the end, reset to beginning.
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