using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class PolyRenderer : AptComponent
    {
        public PolyPoint[] Points;
        public override void Render(FrameEventArgs a) => ScreenHandler.Poly(Points, this.Transform);
    }
}