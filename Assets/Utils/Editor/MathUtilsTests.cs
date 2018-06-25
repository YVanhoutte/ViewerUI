using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public sealed class MathUtilsTests
{
    [Test]
    public void AverageFloat_1_2_3_Is2()
    {
        List<float> floats = new List<float>() { 1, 2, 3 };
        float result = MathUtils.Average(floats);
        Assert.AreEqual(2, result);
    }

    [Test]
    public void AverageInt_1_2Is1AndHalf()
    {
        List<int> ints = new List<int>() { 1, 2 };
        float result = MathUtils.Average(ints);
        Assert.AreEqual(1.5f, result);
    }

    [Test]
    public void AverageVector2_1_2Is1AndHalf()
    {
        List<Vector2> vectors = new List<Vector2>() { Vector2.one, Vector2.one * 2 };
        Vector2 result = MathUtils.Average(vectors);
        Assert.AreEqual(Vector2.one * 1.5f, result);
    }

    [Test]
    public void AverageVector3_1_2Is1AndHalf()
    {
        List<Vector3> vectors = new List<Vector3>() { Vector3.one, Vector3.one * 2 };
        Vector3 result = MathUtils.Average(vectors);
        Assert.AreEqual(Vector3.one * 1.5f, result);
    }
}
