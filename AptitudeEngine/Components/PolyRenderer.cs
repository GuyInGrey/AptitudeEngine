using AptitudeEngine.Events;

namespace AptitudeEngine.Components
{
    public class PolyRenderer : AptComponent
    {
        public PolyPoint[] Points;
        public override void Render(FrameEventArgs a) => ScreenHandler.Poly(Points);
    }
}