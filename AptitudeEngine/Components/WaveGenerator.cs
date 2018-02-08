using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components
{
    public class WaveGenerator : AptComponent
    {
        private float _Radius = 0.5f;

        public float Radius
        {
            get => _Radius;
            set
            {
                _Radius = value.Clamp(0, 1000000000f);
                CurrentCircleLocation = new Vector2(0, -Radius);
            }
        }

        public float ValueX => CurrentCircleLocation.X;
        public float ValueY => CurrentCircleLocation.Y;

        public Vector2 CurrentCircleLocation = new Vector2(0, -0.5f);

        public float Frequency { get; set; } = 1f;

        public override void Render(FrameEventArgs a)
        =>
            CurrentCircleLocation = CurrentCircleLocation.Rotate(Vector2.Zero, Frequency * ((float)Math.PI / 180));
    }
}