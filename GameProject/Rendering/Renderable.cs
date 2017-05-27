﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Common;
using Game.Models;
using System.Runtime.Serialization;

namespace Game.Rendering
{
    [DataContract]
    public class Renderable : IRenderable
    {
        [DataMember]
        public bool Visible { get; set; } = true;

        [DataMember]
        public bool DrawOverPortals { get; set; }

        [DataMember]
        public bool IsPortalable { get; set; } = true;

        [DataMember]
        public Transform2 WorldTransform { get; set; } = new Transform2();
        public Transform2 WorldVelocity => Transform2.CreateVelocity();

        [DataMember]
        public List<Model> Models { get; set; } = new List<Model>();

        public List<Model> GetModels() => new List<Model>(Models);

        public Renderable()
        {
        }

        public Renderable(Transform2 worldTransform)
        {
            WorldTransform = worldTransform;
        }
    }
}
