﻿using System;
using Game;
using NUnit.Framework;
using Game.Animation;

namespace GameTests
{
    [TestFixture]
    public class FCurveTests
    {
        public const float ErrorMargin = 0.0001f;
        public float DefaultValue = 5f;

        public Curve CreateFCurve()
        {
            Curve fCurve = new Curve(DefaultValue);
            fCurve.AddKeyframe(new Keyframe(1f, 0f));
            fCurve.AddKeyframe(new Keyframe(6f, 10f));
            return fCurve;
        }

        [Test]
        public void GetLengthTest0()
        {
            Curve fCurve = new Curve();
            Assert.AreEqual(fCurve.Length, 0);
        }

        [Test]
        public void GetLengthTest1()
        {
            Curve fCurve = CreateFCurve();
            Assert.AreEqual(fCurve.Length, 6f);
        }

        [Test]
        public void GetValuesTest0()
        {
            Curve fCurve = CreateFCurve();
            Assert.AreEqual(fCurve.GetValue(1f), 0f, ErrorMargin);
            Assert.AreEqual(fCurve.GetValue(6f), 10f, ErrorMargin);
        }

        /// <summary>
        /// Test that out of range time values return the first or last value for a non-looping FCurve.
        /// </summary>
        [Test]
        public void GetValuesTest1()
        {
            Curve fCurve = CreateFCurve();
            Assert.AreEqual(fCurve.GetValue(-1f), 0f, ErrorMargin);
            Assert.AreEqual(fCurve.GetValue(0f), 0f, ErrorMargin);
            Assert.AreEqual(fCurve.GetValue(100f), 10f, ErrorMargin);
        }

        [Test]
        public void GetValuesTest2()
        {
            Curve fCurve = CreateFCurve();
            Assert.AreEqual(fCurve.GetValue(2f), 2f, ErrorMargin);
        }

        [Test]
        public void GetValuesTest3()
        {
            Curve fCurve = CreateFCurve();
            fCurve.IsLoop = true;
            float time = 11.5f;
            Assert.AreEqual(fCurve.GetValue(time), fCurve.GetValue(time % fCurve.Length));
            time = -5.5f;
            Assert.AreEqual(fCurve.GetValue(time), fCurve.GetValue((time + fCurve.Length) % fCurve.Length));
        }
    }
}
