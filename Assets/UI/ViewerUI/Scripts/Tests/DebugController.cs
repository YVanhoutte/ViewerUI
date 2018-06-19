using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    //private void Start()
    //{
    //    if(InputEventsHandler.Instance != null)
    //    {
    //        InputEventsHandler.Instance.OnControl1Pressed += FaceButton1Pressed;
    //        InputEventsHandler.Instance.OnControl2Pressed += FaceButton2Pressed;
    //        InputEventsHandler.Instance.OnControl3Pressed += FaceButton3Pressed;
    //        InputEventsHandler.Instance.OnControl4Pressed += FaceButton4Pressed;
    //    }
    //}

    public void FaceButton1Pressed()
    {
        Debug.Log("Face Button 1 pressed!");
    }

    public void FaceButton2Pressed()
    {
        Debug.Log("Face Button 2 pressed!");
    }

    public void FaceButton3Pressed()
    {
        Debug.Log("Face Button 3 pressed!");
    }

    public void FaceButton4Pressed()
    {
        Debug.Log("Face Button 4 pressed!");
    }
}
