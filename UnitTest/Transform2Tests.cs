﻿using System;
using NUnit.Framework;
using Game;
using Game.Common;
using OpenTK;

namespace GameTests
{
    [TestFixture]
    public class Transform2Tests
    {
        #region GetNormal test
        [Test]
        public void GetNormalTest0()
        {
            Transform2 t = new Transform2(new Vector2(100, -200));

            Vector2 normal = t.GetRight();
            Assert.IsTrue(normal == new Vector2(1, 0));
        }
        [Test]
        public void GetNormalTest1()
        {
            Transform2 t = new Transform2(new Vector2(100, -200), 1, (float)Math.PI);

            Vector2 normal = t.GetRight();
            Vector2 reference = new Vector2(-1, 0);
            Assert.IsTrue(Math.Abs(normal.X - reference.X) < 0.00001 && Math.Abs(normal.Y - reference.Y) < 0.00001);
        }
        [Test]
        public void GetNormalTest2()
        {
            Transform2 t = new Transform2(new Vector2(100, -200), 1, (float)Math.PI / 4);

            Vector2 normal = t.GetRight();
            Vector2 reference = new Vector2((float)Math.Cos(Math.PI/4), (float)Math.Sin(Math.PI/4));
            Assert.IsTrue(Math.Abs(normal.X - reference.X) < 0.00001 && Math.Abs(normal.Y - reference.Y) < 0.00001);
        }
        [Test]
        public void GetNormalTest3()
        {
            Transform2 t = new Transform2(new Vector2(100, -200), 1, 0, true);

            Vector2 normal = t.GetRight();
            Vector2 reference = new Vector2(-1, 0);
            Assert.IsTrue(Math.Abs(normal.X - reference.X) < 0.00001 && Math.Abs(normal.Y - reference.Y) < 0.00001);
        }
        [Test]
        public void GetNormalTest4()
        {
            Transform2 t = new Transform2(new Vector2(100, -200), -1, 0, true);

            Vector2 normal = t.GetRight();
            Vector2 reference = new Vector2(1, 0);
            Assert.IsTrue(Math.Abs(normal.X - reference.X) < 0.00001 && Math.Abs(normal.Y - reference.Y) < 0.00001);
        }
        [Test]
        public void GetNormalTest5()
        {
            Transform2 t = new Transform2(new Vector2(100, -200), -1);

            Vector2 normal = t.GetRight();
            Vector2 reference = new Vector2(-1, 0);
            Assert.IsTrue(Math.Abs(normal.X - reference.X) < 0.00001 && Math.Abs(normal.Y - reference.Y) < 0.00001);
        }
        [Test]
        public void GetNormalTest6()
        {
            Transform2 t = new Transform2(new Vector2(100, -200), -1, (float)Math.PI / 4, true);

            Vector2 normal = t.GetRight();
            Vector2 reference = new Vector2((float)Math.Cos(Math.PI / 4), (float)Math.Sin(Math.PI / 4));
            Assert.IsTrue(Math.Abs(normal.X - reference.X) < 0.00001 && Math.Abs(normal.Y - reference.Y) < 0.00001);
        }
        [Test]
        public void GetNormalTest7()
        {
            Transform2 t = new Transform2(new Vector2(100, -200), 1, (float)Math.PI / 4, true);

            Vector2 normal = t.GetRight();
            Vector2 reference = -new Vector2((float)Math.Cos(Math.PI / 4), (float)Math.Sin(Math.PI / 4));
            Assert.IsTrue(Math.Abs(normal.X - reference.X) < 0.0001 && Math.Abs(normal.Y - reference.Y) < 0.0001);
        }
        #endregion
        #region Equal tests
        [Test]
        public void EqualTest0()
        {
            Transform2 transform0 = new Transform2();
            Transform2 transform1 = null;
            Assert.IsFalse(transform0.AlmostEqual(transform1));
        }
        [Test]
        public void EqualTest1()
        {
            Transform2 transform0 = new Transform2(new Vector2(1,-4));
            Transform2 transform1 = new Transform2();
            Assert.IsFalse(transform0.AlmostEqual(transform1));
        }
        [Test]
        public void EqualTest2()
        {
            Transform2 transform0 = new Transform2(new Vector2(1, -4), 50f, 130f);
            Transform2 transform1 = new Transform2(new Vector2(1, -4), 50f, 130f);
            Assert.IsTrue(transform0.AlmostEqual(transform1));
        }
        [Test]
        public void EqualTest3()
        {
            Transform2 transform0 = new Transform2(new Vector2(1, -4), 50f, 130f);
            Transform2 transform1 = new Transform2(new Vector2(1, -4), 4f, 130f);
            Assert.IsFalse(transform0.AlmostEqual(transform1));
        }
        [Test]
        public void EqualTest4()
        {
            Transform2 transform0 = new Transform2(new Vector2(1, -4), 25f, 130f);
            Transform2 transform1 = new Transform2(new Vector2(1, -1), 25f, 130f);
            Assert.IsFalse(transform0.AlmostEqual(transform1));
        }
        [Test]
        public void EqualTest5()
        {
            Transform2 transform0 = new Transform2(new Vector2(1, -4), -100f, 130f);
            Transform2 transform1 = new Transform2(new Vector2(1, -4), -100f, -20f);
            Assert.IsFalse(transform0.AlmostEqual(transform1));
        }
        #endregion
        #region Inverted tests
        [Test]
        public void InvertedTest0()
        {
            Transform2 t = new Transform2();
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(t.GetMatrix() == tInverted.GetMatrix().Inverted());
        }
        [Test]
        public void InvertedTest1()
        {
            Transform2 t = new Transform2();
            t.SetScale(new Vector2(5, -5));
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(t.GetMatrix() == tInverted.GetMatrix().Inverted());
        }
        [Test]
        public void InvertedTest2()
        {
            Transform2 t = new Transform2(new Vector2(), 1, 123);
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(Matrix4Ex.AlmostEqual(t.GetMatrix(), tInverted.GetMatrix().Inverted()));
        }
        [Test]
        public void InvertedTest3()
        {
            Transform2 t = new Transform2(new Vector2(), 1, (float)Math.PI / 5);
            t.SetScale(new Vector2(-5, -5));
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(Matrix4Ex.AlmostEqual(t.GetMatrix(), tInverted.GetMatrix().Inverted()));
        }
        [Test]
        public void InvertedTest4()
        {
            Transform2 t = new Transform2(new Vector2(), 1, (float)Math.PI / 3);
            t.SetScale(new Vector2(5, -5));
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(Matrix4Ex.AlmostEqual(t.GetMatrix(), tInverted.GetMatrix().Inverted()));
        }
        [Test]
        public void InvertedTest5()
        {
            Transform2 t = new Transform2(new Vector2(), 1, (float)(Math.PI / 3.4));
            t.SetScale(new Vector2(-5, 5));
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(Matrix4Ex.AlmostEqual(t.GetMatrix(), tInverted.GetMatrix().Inverted()));
        }
        [Test]
        public void InvertedTest6()
        {
            Transform2 t = new Transform2(new Vector2(2, 1));
            t.SetScale(new Vector2(5, -5));
            //t.Rotation = (float)(Math.PI / 3.4);
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(Matrix4Ex.AlmostEqual(t.GetMatrix(), tInverted.GetMatrix().Inverted()));
        }
        [Test]
        public void InvertedTest7()
        {
            Transform2 t = new Transform2(new Vector2(2, 1), 1, (float)(Math.PI / 3.4));
            t.SetScale(new Vector2(-5, -5));
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(Matrix4Ex.AlmostEqual(t.GetMatrix(), tInverted.GetMatrix().Inverted()));
        }
        [Test]
        public void InvertedTest8()
        {
            Transform2 t = new Transform2(new Vector2(2, 1), 1, (float)(Math.PI / 3.4));
            t.SetScale(new Vector2(-5, 5));
            Transform2 tInverted = t.Inverted();
            Assert.IsTrue(Matrix4Ex.AlmostEqual(t.GetMatrix(), tInverted.GetMatrix().Inverted()));
        }
        #endregion
        #region Transform tests
        [Test]
        public void TransformTest0()
        {
            Transform2 t0 = new Transform2();
            Transform2 t1 = new Transform2(new Vector2(1,2), 3, 123);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest1()
        {
            Transform2 t0 = new Transform2(new Vector2(1,1), -2, 3.21f);
            Transform2 t1 = new Transform2(new Vector2(-100, 2), 3, 123);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest2()
        {
            Transform2 t0 = new Transform2(new Vector2(), 1, 0);
            Transform2 t1 = new Transform2(new Vector2(), 1, 2, true);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest3()
        {
            Transform2 t0 = new Transform2(new Vector2(), 1, 2);
            Transform2 t1 = new Transform2(new Vector2(), 1, 0, true);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest4()
        {
            Transform2 t0 = new Transform2(new Vector2(), 1, 2);
            Transform2 t1 = new Transform2(new Vector2(), 1, 3.4f, true);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest5()
        {
            Transform2 t0 = new Transform2(new Vector2(), 1, 0, true);
            Transform2 t1 = new Transform2(new Vector2(), 1, 2);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest6()
        {
            Transform2 t0 = new Transform2(new Vector2(), 1, 2, true);
            Transform2 t1 = new Transform2(new Vector2(), 1, 0);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest7()
        {
            Transform2 t0 = new Transform2(new Vector2(), 1, 2, true);
            Transform2 t1 = new Transform2(new Vector2(), 1, 3.4f);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        [Test]
        public void TransformTest8()
        {
            Transform2 t0 = new Transform2(new Vector2(), 1, 2, true);
            Transform2 t1 = new Transform2(new Vector2(), 1, 3.4f, true);
            Transform2 result = t0.Transform(t1);
            Assert.IsTrue(Matrix4Ex.AlmostEqual(result.GetMatrix(), t0.GetMatrix() * t1.GetMatrix()));
        }
        #endregion
    }
}
