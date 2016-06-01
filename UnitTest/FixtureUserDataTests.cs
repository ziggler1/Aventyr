﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;
using OpenTK;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class FixtureUserDataTests
    {
        public Actor CreateGround(Scene scene)
        {
            Vector2[] verts = new Vector2[] {
                new Vector2(),
                new Vector2(3, 0),
                new Vector2(3, 3),
                new Vector2(2.5f, 4),
                new Vector2(0, 3),
            };
            Actor ground = ActorFactory.CreateEntityPolygon(scene, new Transform2(), verts);
            ground.Name = "ground";
            return ground;
        }

        public Scene CreateSceneWithPortal()
        {
            Scene scene = new Scene();
            Actor ground = CreateGround(scene);
            scene.World.ProcessChanges();
            Fixture fixture = ground.Body.FixtureList[0];
            FixturePortal portal = new FixturePortal(scene, ground, new PolygonCoord(0, 0.3f));
            FloatPortal portalExit = new FloatPortal(scene);
            portalExit.Linked = portal;
            portal.Linked = portal;

            FixtureExt.GetUserData(fixture).ProcessChanges();
            return scene;
        }

        [TestMethod]
        public void PortalParentTest0()
        {
            Scene scene = CreateSceneWithPortal();
            Actor ground = (Actor)scene.FindByName("ground");

            Assert.IsTrue(ground.Body.FixtureList.Count == 3, "There should be three fixtures.  The original fixture and two fixtures that are a part of the FixturePortal.");
        }

        [TestMethod]
        public void PortalParentTest1()
        {
            Scene scene = CreateSceneWithPortal();
            Actor ground = (Actor)scene.FindByName("ground");
            int parentCount = 0;
            foreach (Fixture f in ground.Body.FixtureList)
            {
                FixtureUserData userData = FixtureExt.GetUserData(f);
                if (userData.IsPortalParentless() == false)
                {
                    parentCount++;
                }
            }
            Assert.IsTrue(parentCount == 2);
        }

        [TestMethod]
        public void PortalParentTest2()
        {
            Scene scene = CreateSceneWithPortal();
            Actor ground = (Actor)scene.FindByName("ground");
            FixturePortal portal = scene.GetPortalList().OfType<FixturePortal>().First();

            FixtureUserData userData;
            userData = FixtureExt.GetUserData(ground.Body.FixtureList[0]);
            Assert.IsTrue(userData.PortalParents[0] == null && userData.PortalParents[1] == null);
            userData = FixtureExt.GetUserData(ground.Body.FixtureList[1]);
            Assert.IsTrue(userData.PartOfPortal(portal));
            userData = FixtureExt.GetUserData(ground.Body.FixtureList[2]);
            Assert.IsTrue(userData.PartOfPortal(portal));
        }

        [TestMethod]
        public void PortalParentTest3()
        {
            Scene scene = CreateSceneWithPortal();
            Actor ground = (Actor)scene.FindByName("ground");
            FixturePortal portal0 = scene.GetPortalList().OfType<FixturePortal>().First();

            FixturePortal portal1 = new FixturePortal(scene, ground, new PolygonCoord(0, 0.6f));
            FloatPortal portal2 = new FloatPortal(scene);
            //Make sure this portal isn't sitting on top of the float portal linked to portal0.
            portal2.SetTransform(new Transform2(new Vector2(5, 0)));
            portal1.Linked = portal2;
            portal2.Linked = portal1;
            FixtureUserData userData = FixtureExt.GetUserData(ground.Body.FixtureList[0]);
            userData.ProcessChanges();

            int parentCount = 0;
            foreach (Fixture f in ground.Body.FixtureList)
            {
                userData = FixtureExt.GetUserData(f);
                if (userData.IsPortalParentless() == false)
                {
                    parentCount++;
                }
            }
            Assert.IsTrue(parentCount == 3);
        }

        [TestMethod]
        public void PortalParentTest4()
        {
            Scene scene = CreateSceneWithPortal();
            Actor ground = (Actor)scene.FindByName("ground");
            FixturePortal portal0 = scene.GetPortalList().OfType<FixturePortal>().First();

            FixturePortal portal1 = new FixturePortal(scene, ground, new PolygonCoord(0, 0.6f));
            FloatPortal portal2 = new FloatPortal(scene);
            //Make sure portal2 isn't sitting on top of the float portal linked to portal0.
            portal2.SetTransform(new Transform2(new Vector2(5, 0)));
            portal1.Linked = portal2;
            portal2.Linked = portal1;
            FixtureUserData userData = FixtureExt.GetUserData(ground.Body.FixtureList[0]);
            userData.ProcessChanges();

            PolygonShape shape;
            shape = (PolygonShape)ground.Body.FixtureList[0].Shape;
            Assert.IsTrue(shape.Vertices.Count == 5);
            shape = (PolygonShape)ground.Body.FixtureList[1].Shape;
            Assert.IsTrue(shape.Vertices.Count == 3);
            shape = (PolygonShape)ground.Body.FixtureList[2].Shape;
            Assert.IsTrue(shape.Vertices.Count == 4);
            shape = (PolygonShape)ground.Body.FixtureList[3].Shape;
            Assert.IsTrue(shape.Vertices.Count == 3);
        }
    }
}
