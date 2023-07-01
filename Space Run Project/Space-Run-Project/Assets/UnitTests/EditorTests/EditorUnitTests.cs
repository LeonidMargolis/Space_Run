using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditorUnitTests
{
    private float valToBoost;

    [SetUp]
    public void SetUp()
    {
        valToBoost = 5f;
    }

    [Test]
    public void UnitTest_IsOneAndHalfBoosted()
    {
        float correctVal = valToBoost * 1.5f;
        Assert.AreEqual(correctVal, BoostFuncs.OneAndHalfBoost(valToBoost));
    }

    [Test]
    public void UnitTest_IsDoubleBoosted()
    {
        float correctVal = valToBoost * 2;
        Assert.AreEqual(correctVal, BoostFuncs.DoubleBoost(valToBoost));
    }

    [TearDown]
    public void TearDown()
    {
        valToBoost = 0.0f;
    }
}
