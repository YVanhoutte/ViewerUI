using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class OrientToDevice : MonoBehaviour
{
    private DeviceOrientation orientation;
    private RectTransform m_myRect;

    private void Awake()
    {
        m_myRect = GetComponent<RectTransform>();
    }

    private void Update ()
    {
		if(orientation != Input.deviceOrientation)
        {
            orientation = Input.deviceOrientation;
            Quaternion newRotation = Quaternion.identity;
            switch(orientation)
            {
                case DeviceOrientation.Portrait:
                case DeviceOrientation.PortraitUpsideDown:
                    newRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case DeviceOrientation.LandscapeLeft:
                case DeviceOrientation.LandscapeRight:
                    break;
            }
            m_myRect.rotation = newRotation;
        }
	}
}
