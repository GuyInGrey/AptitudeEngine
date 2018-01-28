using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components
{
    public class PolyRenderer : AptComponent
    {
        public PolyPoint[] Points;
        public override void Render(FrameEventArgs a) => ScreenHandler.Poly(Points);
    }
}
