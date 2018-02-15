using System;
using System.Collections.Generic;
using AptitudeEngine.CoordinateSystem;

namespace AptitudeEngine.Components.Pathing
{
    public class PathFinder : AptComponent
    {
        public List<Node> Nodes;
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }

        public void GenerateNodes(int nodeCnt, float minX, float minY, float maxX, float maxY, float connectionCntPerNode)
        {
            Nodes = new List<Node>();
            var r = new Random();

            var start = Vector2.Random(minX, minY, maxX, maxY);
            var end = Vector2.Random(minX, minY, maxX, maxY);

            StartNode = new Node(start, end, start);
            EndNode = new Node(start, end, end);

            for (var i = 0; i < nodeCnt; i++)
            {
                var n = new Node(start, end, Vector2.Random(minX, minY, maxX, maxY));
                if (Nodes.Count >= connectionCntPerNode)
                {
                    for (var m = 0; m < connectionCntPerNode; m++)
                    {
                        n.ConnectedNodes.Add(Nodes[r.Next(Nodes.Count)]);
                    }
                }
                Nodes.Add(n);
            }

            for (var i = 0; i < connectionCntPerNode; i++)
            {
                for (var m = 0; m < connectionCntPerNode; m++)
                {
                    Nodes[i].ConnectedNodes.Add(Nodes[r.Next(Nodes.Count)]);
                }
            }
        }
    }
}