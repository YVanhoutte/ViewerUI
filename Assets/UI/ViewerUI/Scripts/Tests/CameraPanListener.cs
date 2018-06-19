using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanListener : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        InspecterCamera.OnCameraPanStarted += DebugStart;
        InspecterCamera.OnCameraPanEnded += DebugEnd;
	}

    private void DebugStart()
    {
        Debug.Log("Camera Pan Started");
    }

    private void DebugEnd()
    {
        Debug.Log("Camera Pan Ended");
    }
}
