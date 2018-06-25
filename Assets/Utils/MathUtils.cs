using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class MathUtils
{
    // Methods for averaging float, int, Vector2, and Vector3 lists and arrays.
    // Explicit implementation of List and Array to avoid GC alloc in boxing collections to IEnumerables in call. Cumbersome, but more optimized!
    #region Average Methods
    #region Floats
    static public float Average(List<float> values)
    {
        float result = 0;
        foreach (float value in values)
            result += value;
        return values.Count > 0 ? result / values.Count : result;
    }

    static public float Average(float[] values)
    {
        float result = 0;
        foreach (float value in values)
            result += value;
        return values.Length > 0 ? result / values.Length : result;
    }
    #endregion
    #region Ints
    static public float Average(List<int> values)
    {
        float result = 0;
        foreach (int value in values)
            result += value;
        return values.Count > 0 ? result / values.Count : result;
    }

    static public float Average(int[] values)
    {
        float result = 0;
        foreach (int value in values)
            result += value;
        return values.Length > 0 ? result / values.Length : result;
    }
    #endregion
    #region Vector2s
    static public Vector2 Average(List<Vector2> values)
    {
        Vector2 result = Vector2.zero;
        foreach (Vector2 value in values)
            result += value;
        return values.Count > 0 ? result / values.Count : result;
    }

    static public Vector2 Average(Vector2[] values)
    {
        Vector2 result = Vector2.zero;
        foreach (Vector2 value in values)
            result += value;
        return values.Length > 0 ? result / values.Length : result;
    }
    #endregion
    #region Vector3s
    static public Vector3 Average(List<Vector3> values)
    {
        Vector3 result = Vector3.zero;
        foreach (Vector3 value in values)
            result += value;
        return values.Count > 0 ? result / values.Count : result;
    }

    static public Vector3 Average(Vector3[] values)
    {
        Vector3 result = Vector3.zero;
        foreach (Vector3 value in values)
            result += value;
        return values.Length > 0 ? result / values.Length : result;
    }
    #endregion
    #endregion
}
