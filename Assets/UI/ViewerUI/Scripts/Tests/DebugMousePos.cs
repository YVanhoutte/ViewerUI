using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMousePos : MonoBehaviour
{	
	void Update ()
    {
        Debug.Log("Mouse Position: " + Input.mousePosition);
	}
}
