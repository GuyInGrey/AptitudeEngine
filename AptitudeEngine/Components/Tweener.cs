using AptitudeEngine.Enums;
using AptitudeEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptitudeEngine.Components
{
    public class Tweener : AptComponent
    {
        public Vector2 Toward { get; private set; }
        public Vector2 From { get; private set; }
        public float CurrentTime { get; private set; }
        public float TimeTake { get; private set; }
        public TweenType Type { get; private set; }

        public void SetPosition(Vector2 to, TweenType type, float take)
        {
            if (type == TweenType.Instant)
            {
                Transform.Position = to;
                From = to;
                Toward = to;
                Type = TweenType.Instant;
                CurrentTime = 0;
                TimeTake = 0;
            }
            else
            {
                From = Transform.Position;
                Toward = to;
                Type = type;
                CurrentTime = 0;
                TimeTake = take;
            }
        }

        public override void Render(FrameEventArgs a)
        {
            var percent = CurrentTime / TimeTake;

            if (percent < 1)
            {
                CurrentTime += (float)a.Delta;

                Transform.Position = From + (Toward - From) * TweenValue(percent);
            }
        }

        private float TweenValue(float t)
        {
            switch (Type)
            {
                case TweenType.QuadraticInOut:
                    return (t * t) / ((2 * t * t) - (2 * t) + 1);
                case TweenType.CubicInOut:
                    return (t * t * t) / ((3 * t * t) - (3 * t) + 1);
                case TweenType.QuarticOut:
                    return -((float)Math.Pow((t - 1), 4)) + 1;
                default:
                    return t;
            }
        }
    }
}