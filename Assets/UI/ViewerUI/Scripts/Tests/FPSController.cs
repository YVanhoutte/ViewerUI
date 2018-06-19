using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {

    public bool randomizeFPS;
    [Range(1, 360)] public int randomMin, randomMax;
    [Range(1,360)] public int targetFPS = 60;
    private int m_oldTargetFPS;

    void Start ()
    {
        SetFPS();
	}
	
	void Update ()
    {
        if (randomizeFPS)
            SetFPSRandom();
        else if (m_oldTargetFPS != targetFPS)
            SetFPS();
	}

    private void SetFPSRandom()
    {
        Application.targetFrameRate = UnityEngine.Random.Range(randomMin, randomMax);
    }

    private void SetFPS()
    {
        Application.targetFrameRate = targetFPS;
        m_oldTargetFPS = targetFPS;
    }
}
