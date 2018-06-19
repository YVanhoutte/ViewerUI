using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas), typeof(CanvasScaler))]
public class PlatformScaler : MonoBehaviour
{
    private void Awake()
    {
        CanvasScaler myScaler = GetComponent<CanvasScaler>();
#if UNITY_IOS || UNITY_ANDROID
        myScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPhysicalSize;
        myScaler.physicalUnit = CanvasScaler.Unit.Millimeters;
#else
        myScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        myScaler.referenceResolution = new Vector2(1920, 1080);
        myScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        myScaler.matchWidthOrHeight = 0.5f;
#endif
    }
}


/*
 * Measurements for touch controls!
 * Source: https://www.smashingmagazine.com/2012/02/finger-friendly-design-ideal-mobile-touchscreen-target-sizes/
 * Source: https://www.lukew.com/ff/entry.asp?1085
 * Finger width = 20 mm
 * Thumb width  = 25 mm
 * MS recommended touch target size: 9 mm, minimum 7 mm, spacing 2 mm
 */
