using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swantiez.unity.tools.utils;
using UnityEngine;

namespace swantiez.unity.tools.tests
{
    [TestClass]
    public class MathUtilsTests
    {
        [TestMethod]
        public void GetSignedAngleBetweenVectors_Test1()
        {
            Vector3 v1 = new Vector3(1, 1, 0);
            Vector3 v2 = new Vector3(1, 0, 0);
            const float expectedAngle = 45f;
            float actualAngle = MathUtils.GetSignedAngleBetweenVectors(v1, v2);
            Assert.AreEqual(expectedAngle, actualAngle, 0.001f, "Wrong angle returned");
        }
    }
}
