﻿﻿using System;
using NUnit.Framework;
using Game;
using Game.Portals;
using System.Collections.Generic;
using OpenTK;
using System.Linq;
using Game.Common;
using Game.Physics;

namespace GameTests
{
    [TestFixture]
    public class SceneNodeTests
    {
        //#region GetWorldVelocity tests

        ///// <summary>
        ///// Returns a simple to calculate approximation of world velocity.  
        ///// Has the side effect of moving IPortalable instances slightly.
        ///// </summary>
        //public Transform2 ApproximateVelocity(SceneNode node)
        //{
        //    Transform2 previous = node.WorldTransform;
        //    float epsilon = 0.00005f;
        //    foreach (IPortalable p in node.Scene.SceneNodeList.OfType<IPortalable>())
        //    {
        //        p.SetTransform(p.GetTransform().Add(p.GetVelocity().Multiply(epsilon)));
        //    }
        //    Transform2 current = node.GetWorldTransform();
        //    return current.Minus(previous).Multiply(1/epsilon);
        //}

        //public Transform2 GetRandomTransform(Random random)
        //{
        //    return new Transform2(
        //            new Vector2((float)random.NextDouble() * 100 - 50, (float)random.NextDouble() * 100 - 50),
        //            1 + (float)random.NextDouble(),
        //            (float)random.NextDouble() * 2 + 1f,
        //            random.NextDouble() > 0.5);
        //}

        //public Transform2 GetRandomVelocity(Random random)
        //{
        //    return Transform2.CreateVelocity(
        //            new Vector2((float)random.NextDouble() * 100 - 50, (float)random.NextDouble() * 100 - 50),
        //            (float)random.NextDouble() * 100 - 50,
        //            (float)random.NextDouble() - 0.5f);
        //}

        ///// <summary>
        ///// If the node has no parent then its world velocity equals its local velocity.
        ///// </summary>
        //[Test]
        //public void GetWorldVelocityTest0()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetVelocity(Transform2.CreateVelocity(new Vector2(2.3f, -55f), 23.11f, 13.4f));
        //    Assert.IsTrue(node.GetWorldVelocity() == node.GetVelocity());
        //}

        ///// <summary>
        ///// Changing the node's transform should have no effect on the world velocity.
        ///// </summary>
        //[Test]
        //public void GetWorldVelocityTest1()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(100f, 33f), 13f, 311f));
        //    node.SetVelocity(Transform2.CreateVelocity(new Vector2(-1.3f, -5f), 3.11f, -14f));
        //    Assert.IsTrue(node.GetWorldVelocity() == node.GetVelocity());
        //}

        ///// <summary>
        ///// If the node is parented to another node that has default orientation and no velocity 
        ///// then the child node's world velocity equals it's local velocity.
        ///// </summary>
        //[Test]
        //public void GetWorldVelocityTest2()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(100f, 33f), 13f, 311f));
        //    node.SetVelocity(Transform2.CreateVelocity(new Vector2(-1.3f, -5f), 3.11f, -14f));
            
        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);

        //    Assert.IsTrue(node.GetWorldVelocity() == node.GetVelocity());
        //}

        ///// <summary>
        ///// The child nodes angular velocity should equal it's angular velocity plus parent angular 
        ///// velocity if everything else is at default.
        ///// </summary>
        //[Test]
        //public void GetWorldVelocityTest3()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2());
        //    node.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 10f));

        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Assert.AreEqual(node.GetWorldVelocity().Rotation, node.GetVelocity().Rotation + parent.GetVelocity().Rotation, 0.00001);
        //    Assert.AreEqual(node.GetWorldVelocity().Position, Vector2.Zero);
        //    Assert.AreEqual(node.GetWorldVelocity().Scale, Vector2.Zero);
        //}

        //[Test]
        //public void GetWorldVelocityTest4()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 worldVelocity = node.GetWorldVelocity();
        //    Assert.AreEqual(node.GetVelocity().Rotation + parent.GetVelocity().Rotation, worldVelocity.Rotation, 0.00001);
        //    Assert.AreEqual(new Vector2(0, 2 * 5.5f), worldVelocity.Position);
        //    Assert.AreEqual(Vector2.Zero, worldVelocity.Scale);
        //}

        //[Test]
        //public void GetWorldVelocityTest5()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 worldVelocity = node.GetWorldVelocity();
        //    Assert.AreEqual(node.GetVelocity().Rotation + parent.GetVelocity().Rotation, worldVelocity.Rotation, 0.00001);
        //    Assert.AreEqual(new Vector2(0, 2 * 5.5f), worldVelocity.Position);
        //    Assert.AreEqual(Vector2.Zero, worldVelocity.Scale);
        //}

        //[Test]
        //public void GetWorldVelocityTest6()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest7()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(), 1, -13.3f));
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest8()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(-1.9f, 3.3f), 1, -13.3f));
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest9()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(-1.9f, 3.3f), 3.7f, -13.3f));
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest10()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(-1.9f, 3.3f), -3.7f, -13.3f));
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest11()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(2, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(-1.9f, 3.3f), 3.7f, -13.3f, true));
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest12()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(3, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(Vector2.Zero, 2f));
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 0f, 1f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest13()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(3, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(Vector2.Zero, 2f, -5.3f, true));
        //    node.SetParent(parent);
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 0f, 1f));

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest14()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(Vector2.Zero, 24));
        //    node.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 0, 0));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(Vector2.Zero, 1));
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 0, 2));
        //    node.SetParent(parent);

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest15()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(Vector2.Zero, 4));
        //    node.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 0, 5));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(Vector2.Zero, 2));
        //    parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 0, 4));
        //    node.SetParent(parent);

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest16()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(0, 0), 1, 1, false));
        //    node.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 10));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0), 1, 0, true));
        //    //parent.SetVelocity(Transform2.CreateVelocity(new Vector2(0,0), 0));
        //    node.SetParent(parent);

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest17()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(0, 0), 1, 1, false));
        //    node.SetVelocity(Transform2.CreateVelocity(new Vector2(3, 0), 10));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0), -1, 2, true));
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(0,0), 5));
        //    node.SetParent(parent);

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityTest18()
        //{
        //    Random random = new Random(0);
        //    for (int i = 0; i < 50; i++)
        //    {
        //        Scene scene = new Scene();
        //        NodePortalable node = new NodePortalable(scene);
        //        node.SetTransform(GetRandomTransform(random));
        //        node.SetVelocity(GetRandomVelocity(random));

        //        NodePortalable parent = new NodePortalable(scene);
        //        parent.SetTransform(GetRandomTransform(random));
        //        parent.SetVelocity(GetRandomVelocity(random));
        //        node.SetParent(parent);

        //        Transform2 result = node.GetWorldVelocity();
        //        Transform2 expected = ApproximateVelocity(node);
        //        Assert.IsTrue(result.AlmostEqual(expected, 0.05f, 0.05f));
        //    }
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest0()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(30, 0)));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(2, 0)));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest1()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));
        //    //node.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 10f));

        //    NodePortalable parent = new NodePortalable(scene);
        //    //parent.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 5.5f));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(30, 0), 3.3f, 5.5f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(2, 0)));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest2()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));
        //    //node.SetVelocity(Transform2.CreateVelocity(Vector2.Zero, 10f));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1f));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(30, 0), 1f, 0f));
        //    //exit.SetVelocity(Transform2.CreateVelocity(new Vector2(2, 0)));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest3()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));
        //    node.SetVelocity(Transform2.CreateVelocity(new Vector2(-2.3f, -56), 10f, 0.4f));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(1.5f, 5.2f), 1f, 2.3f));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(30, 0), 1f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest4()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1f, 0f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.05f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest5()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0.45f)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1f, 0f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.06f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest6()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), -1.5f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1f, 0f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.06f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest7()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0.45f)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0)));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), -15f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1f, 0f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 10f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.06f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest8()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0f)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0), 1f));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 0f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 2f, 0f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.06f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest9()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0.45f)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0), 1f));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), -4f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1.5f, 0f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 6f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.06f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest10()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0f)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0), 1f, (float)Math.PI/4));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), -4f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1.5f, 0f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 6f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest11()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 12)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(1, 1.2f)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 5), 1f, (float)Math.PI / 4));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), -4f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1.5f, 3.5f));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 6f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest12()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0), 1f, 0));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 2f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1f, 0, true));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 0f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest13()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0), 1f, 0, true));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 2f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), 1f, 0, true));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 0f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest14()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0), 1.5f, 0, true));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 2f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), -1.2f, 0, true));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 3f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest15()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, -0.2f)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0.45f)));
        //    node.SetParent(parent);

        //    FloatPortal enter = new FloatPortal(scene);
        //    enter.SetTransform(new Transform2(new Vector2(4, 0), 1.5f, 0, true));
        //    enter.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 2f, 0f));

        //    FloatPortal exit = new FloatPortal(scene);
        //    exit.SetTransform(new Transform2(new Vector2(100, 0), -1.2f, 0));
        //    exit.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 3f, 0f));

        //    exit.Linked = enter;
        //    enter.Linked = exit;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest16()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, 0)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0)));
        //    node.SetParent(parent);

        //    FloatPortal enter0 = new FloatPortal(scene);
        //    enter0.SetTransform(new Transform2(new Vector2(4, 0), 1f, 0));
        //    enter0.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 0f, 0f));

        //    FloatPortal exit0 = new FloatPortal(scene);
        //    exit0.SetTransform(new Transform2(new Vector2(100, 0), 1f, (float)Math.PI));
        //    exit0.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 0f, 0f));

        //    exit0.Linked = enter0;
        //    enter0.Linked = exit0;

        //    FloatPortal enter1 = new FloatPortal(scene);
        //    enter1.SetTransform(new Transform2(new Vector2(104, 0), 1f, 0));
        //    enter1.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1f, 0f));

        //    FloatPortal exit1 = new FloatPortal(scene);
        //    exit1.SetTransform(new Transform2(new Vector2(50, 30), 1f, 0));
        //    exit1.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 0f, 0f));

        //    exit1.Linked = enter1;
        //    enter1.Linked = exit1;

        //    node.GetWorldTransform();
        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityWithPortalTest17()
        //{
        //    Scene scene = new Scene();
        //    NodePortalable node = new NodePortalable(scene);
        //    node.SetTransform(new Transform2(new Vector2(10, -0.2f)));

        //    NodePortalable parent = new NodePortalable(scene);
        //    parent.SetTransform(new Transform2(new Vector2(0, 0.4f)));
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(1f, 5f), 3f, 0.3f));
        //    node.SetParent(parent);

        //    FloatPortal enter0 = new FloatPortal(scene);
        //    enter0.SetTransform(new Transform2(new Vector2(4, 0), 1.2f, 0, true));
        //    enter0.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1.9f, 0f));

        //    FloatPortal exit0 = new FloatPortal(scene);
        //    exit0.SetTransform(new Transform2(new Vector2(100, 0), 1.5f, (float)Math.PI));
        //    exit0.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), -1.3f, 0f));

        //    exit0.Linked = enter0;
        //    enter0.Linked = exit0;

        //    FloatPortal enter1 = new FloatPortal(scene);
        //    enter1.SetTransform(new Transform2(new Vector2(104, 0), 1.3f, -2f));
        //    enter1.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), 1f, 0f));

        //    FloatPortal exit1 = new FloatPortal(scene);
        //    exit1.SetTransform(new Transform2(new Vector2(50, 30), -0.4f, 0.5f, true));
        //    exit1.SetVelocity(Transform2.CreateVelocity(new Vector2(0, 0), -2f, 0f));

        //    exit1.Linked = enter1;
        //    enter1.Linked = exit1;

        //    Transform2 result = node.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(node);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityFixturePortalTest0()
        //{
        //    Scene scene = new Scene();
        //    NodeWall parent = new NodeWall(scene);
        //    parent.Vertices = PolygonExt.SetNormals(new Vector2[] {
        //        new Vector2(0, 0),
        //        new Vector2(2, 0),
        //        new Vector2(0, 2)
        //    });
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(4.2f, -3.4f), -2.1f, 0.2f));

        //    FixturePortal childPortal = new FixturePortal(scene, parent, new PolygonCoord(0, 0.5f));

        //    Transform2 result = childPortal.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(childPortal);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityFixturePortalTest1()
        //{
        //    Scene scene = new Scene();
        //    Wall parent = new Wall(scene);
        //    parent.Vertices = PolygonFactory.CreateRectangle(4, 4);
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(4.2f, -3.4f), -2.1f, 0.2f));

        //    FixturePortal childPortal = new FixturePortal(scene, parent, new PolygonCoord(0, 0.5f));

        //    FloatPortal enter = new FloatPortal(scene);
        //    FloatPortal exit = new FloatPortal(scene);
        //    enter.Linked = exit;
        //    exit.Linked = enter;

        //    enter.SetTransform(new Transform2(new Vector2(0, 1), 1, (float)Math.PI/2));
        //    exit.SetTransform(new Transform2(new Vector2(100, 0)));

        //    childPortal.Path.Enter(enter, childPortal);

        //    Transform2 result = childPortal.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(childPortal);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityFixturePortalTest1Temp()
        //{
        //    Scene scene = new Scene();
        //    Wall parent = new Wall(scene);
        //    parent.Vertices = PolygonFactory.CreateRectangle(4, 4);
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(1f, 0f), 0f, 0f));

        //    FixturePortal childPortal = new FixturePortal(scene, parent, new PolygonCoord(0, 0.5f));

        //    FloatPortal enter = new FloatPortal(scene);
        //    FloatPortal exit = new FloatPortal(scene);
        //    enter.Linked = exit;
        //    exit.Linked = enter;

        //    enter.SetTransform(new Transform2(new Vector2(0, 1), 1, (float)Math.PI / 2));
        //    exit.SetTransform(new Transform2(new Vector2(100, 0)));

        //    childPortal.Path.Enter(enter, childPortal);

        //    Transform2 result = childPortal.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(childPortal);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}

        //[Test]
        //public void GetWorldVelocityFixturePortalTest2()
        //{
        //    Scene scene = new Scene();
        //    Wall parent = new Wall(scene);
        //    parent.Vertices = PolygonFactory.CreateRectangle(4, 4);
        //    parent.SetVelocity(Transform2.CreateVelocity(new Vector2(4.2f, -3.4f), -2.1f, 0.2f));

        //    FixturePortal childPortal = new FixturePortal(scene, parent, new PolygonCoord(0, 0.5f));
        //    FloatPortal childExit = new FloatPortal(scene);
        //    childExit.SetTransform(new Transform2(new Vector2(20, 20)));
        //    childExit.Linked = childPortal;
        //    childPortal.Linked = childExit;


        //    FloatPortal enter = new FloatPortal(scene);
        //    FloatPortal exit = new FloatPortal(scene);
        //    enter.Linked = exit;
        //    exit.Linked = enter;

        //    enter.SetTransform(new Transform2(new Vector2(0, 1), 1, (float)Math.PI / 2));
        //    exit.SetTransform(new Transform2(new Vector2(100, 0)));

        //    childPortal.Path.Enter(enter, childPortal);

        //    Transform2 result = childPortal.GetWorldVelocity();
        //    Transform2 expected = ApproximateVelocity(childPortal);
        //    Assert.IsTrue(result.AlmostEqual(expected, 0.1f));
        //}
        //#endregion

        //#region TransformUpdate tests
        //[Test]
        //public void TransformUpdateTest0()
        //{
        //    Scene scene = new Scene();
        //    FloatPortal portal = new FloatPortal(scene);

        //    FloatPortal child = new FloatPortal(scene);
        //    portal.Linked = child;
        //    child.Linked = portal;

        //    child.SetParent(portal);

        //    Transform2 begin = new Transform2(new Vector2(5, 0));
        //    child.SetTransform(begin);

        //    Assert.IsTrue(child.WorldTransformPrevious == begin);

        //    Transform2 t = new Transform2(new Vector2(1, 1));
        //    portal.SetTransform(t);

        //    Assert.IsTrue(child.WorldTransformPrevious.AlmostEqual(begin.Transform(t)));
        //}

        //[Test]
        //public void TransformUpdateTest1()
        //{
        //    Scene scene = new Scene();
        //    NodeWall wall = new NodeWall(scene);
        //    wall.Vertices = PolygonFactory.CreateRectangle(2, 2);

        //    FixturePortal child = new FixturePortal(scene, wall, new PolygonCoord(0, 0.5f));

        //    Transform2 begin = child.GetTransform();
        //    Assert.IsTrue(child.WorldTransformPrevious == begin);

        //    child.SetPosition(wall, new PolygonCoord(1, 0.6f));
        //    Transform2 moved = child.GetTransform();
        //    Assert.IsTrue(child.WorldTransformPrevious == moved);

        //    Transform2 t = new Transform2(new Vector2(1, 1));
        //    wall.SetTransform(t);

        //    Assert.IsTrue(child.WorldTransformPrevious.AlmostEqual(moved.Transform(t)));
        //}

        //[Test]
        //public void TransformUpdateTest2()
        //{
        //    Scene scene = new Scene();
        //    Actor actor = new Actor(scene, PolygonFactory.CreateRectangle(2, 2));

        //    FixturePortal child = new FixturePortal(scene, actor, new PolygonCoord(0, 0.5f));

        //    Transform2 begin = child.GetTransform();
        //    Assert.IsTrue(child.WorldTransformPrevious == begin);

        //    child.SetPosition(actor, new PolygonCoord(1, 0.6f));
        //    Transform2 moved = child.GetTransform();
        //    Assert.IsTrue(child.WorldTransformPrevious == moved);

        //    Transform2 t = new Transform2(new Vector2(1, 1));
        //    actor.SetTransform(t);

        //    Assert.IsTrue(child.WorldTransformPrevious.AlmostEqual(moved.Transform(t)));
        //}

        //[Test]
        //public void TransformUpdateTest3()
        //{
        //    Scene scene = new Scene();
        //    Actor actor = new Actor(scene, PolygonFactory.CreateRectangle(2, 2));
            

        //    FixturePortal p0 = new FixturePortal(scene);

        //    FixturePortal p1 = new FixturePortal(scene, actor, new PolygonCoord(1, 0.5f));

        //    p0.Linked = p1;
        //    p1.Linked = p0;

        //    p0.SetPosition(actor, new PolygonCoord(0, 0.5f), 1, true);
            


        //    actor.SetTransform(new Transform2(new Vector2(1, 2)));

        //    p1.SetPosition(actor, new PolygonCoord(1, 0.5f), 1, true);

        //    Assert.IsTrue(p0.WorldTransformPrevious.AlmostEqual(p0.GetWorldTransformPortal()));
        //    Assert.IsTrue(p1.WorldTransformPrevious.AlmostEqual(p1.GetWorldTransformPortal()));
        //}
        //#endregion
    }
}