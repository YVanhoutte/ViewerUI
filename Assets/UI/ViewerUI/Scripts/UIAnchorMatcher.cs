using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchorMatcher : MonoBehaviour
{
	void Update ()
    {
        if (ToggleableWindow.CurrentWindowsCount > 0)
            return;

        Vector3 offsetPosition = transform.localPosition;
        offsetPosition.y = Camera.main.transform.localPosition.y;
        transform.localPosition = offsetPosition;
    }
}
