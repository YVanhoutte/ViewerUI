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

    private void ShowOrHide(int amountOfWindows)
    {
        if (amountOfWindows > 0 && gameObject.activeSelf)
            gameObject.SetActive(false);
        else if (amountOfWindows <= 0 && !gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
