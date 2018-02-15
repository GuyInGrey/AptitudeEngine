using AptitudeEngine.CoordinateSystem;
using System.Collections.Generic;

namespace AptitudeEngine.Components.Pathing
{
    public class Node
    {
        public float DistanceFromStart { get; private set; }
        public float DistanceFromEnd { get; private set; }
        public List<Node> ConnectedNodes { get; set; }

        public Node(Vector2 startPos, Vector2 endPos, Vector2 position)
        {
            ConnectedNodes = new List<Node>();
            Position = position;
            DistanceFromStart = startPos.DistanceFrom(Position);
            DistanceFromEnd = endPos.DistanceFrom(Position);
        }
        
        public Vector2 Position { get; set; }
    }
}