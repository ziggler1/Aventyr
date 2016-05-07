﻿using Game;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public static class ModelFactory
    {
        public static Model CreatePortal()
        {
            Mesh arrow = new Mesh();
            Game.ModelFactory.AddArrow(arrow, new Vector3(0, -0.5f, 0), new Vector2(0, 1), 0.05f, 0.2f, 0.1f);
            Game.ModelFactory.AddArrow(arrow, new Vector3(), new Vector2(0.2f, 0), 0.05f, 0.2f, 0.1f);
            Model arrowModel = new Model(arrow);
            arrowModel.SetColor(new Vector3(0.1f, 0.1f, 0.5f));
            return arrowModel;
        }
    }
}
