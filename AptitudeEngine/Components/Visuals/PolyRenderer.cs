﻿using AptitudeEngine.Events;

namespace AptitudeEngine.Components.Visuals
{
    public class PolyRenderer : AptComponent
    {
        /// <summary>
        /// The vertices to draw the polygon at.
        /// </summary>
        public PolyVector[] Vertices;

        public override void Render(FrameEventArgs a) => ScreenHandler.Polygon(Vertices);
    }
}