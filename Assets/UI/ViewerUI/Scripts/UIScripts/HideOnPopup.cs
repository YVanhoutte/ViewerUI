using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HideOnPopup : MonoBehaviour
{
	void Awake ()
    {
        ToggleableWindow.OnWindowToggled += ShowOrHide;
	}

    private void ShowOrHide()
    {
        if (ToggleableWindow.IsWindowUp && gameObject.activeSelf)
            gameObject.SetActive(false);
        else if (!ToggleableWindow.IsWindowUp && !gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
