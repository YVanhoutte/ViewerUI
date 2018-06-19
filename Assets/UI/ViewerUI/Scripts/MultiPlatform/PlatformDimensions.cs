using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScreenSizeHandler
{
    //static public float ScreenPhysicalSize { get { return (Mathf.Sqrt(Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2))) / Screen.dpi; } } //Pythagoras theorem => screen diagonal / dpi == size in inches
    //static private float m_screenPhysicalSizeTreshold = 8.0f;
    static public float ScaleFactor {
        get {
#if UNITY_IOS || UNITY_ANDROID
            return 1f;
#else
            return 0.5f;
#endif
        }
    }
}
