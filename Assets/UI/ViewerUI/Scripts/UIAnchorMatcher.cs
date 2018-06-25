using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchorMatcher : MonoBehaviour
{
    [SerializeField][Range(0.1f,2f)] private float m_distance = 1.0f;
	void Update ()
    {
        if (ToggleableWindow.CurrentWindowsCount > 0)
            return;

        Vector3 rayFromCam = Camera.main.transform.forward;
        rayFromCam.y = 0;
        rayFromCam.Normalize();
        transform.position = Camera.main.transform.position + rayFromCam * m_distance;
        Quaternion adjustedCamRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.LookRotation(adjustedCamRotation * Vector3.forward, Vector3.up);
    }
}
