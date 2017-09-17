﻿using Game;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Common;

namespace GameTests
{
    public class NodeWall : NodePortalable, IWall
    {
        public IList<Vector2> Vertices { get; set; } = new List<Vector2>();

        public NodeWall(Scene scene)
            : base(scene)
        {
        }

        public IList<Vector2> GetWorldVertices()
        {
            Vector2[] worldVertices = Vector2Ex.Transform(Vertices, WorldTransform.GetMatrix()).ToArray();
            return worldVertices;
        }
    }
}