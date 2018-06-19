using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFader : MonoBehaviour
{
    [SerializeField] private float m_delay = 2f, m_fadeInLength = 0.5f, m_fadeOutLength = 0.5f;
    private float m_timeLastTouch;
    private bool m_faded = true;

    private Coroutine m_fadeRoutine;

    private void Start ()
    {
        var fadeObjects = FindObjectsOfType<CanvasGroup>();
        for(int i = 0; i < fadeObjects.Length; i++)
        {
            fadeObjects[i].alpha = 0;
            fadeObjects[i].interactable = false;
            fadeObjects[i].blocksRaycasts = false;
        }
	}
	
	private void Update ()
    {
        if (Input.GetMouseButton(0) && m_fadeRoutine == null)
        {
            m_fadeRoutine = StartCoroutine(FadeIn(FindObjectsOfType<CanvasGroup>()));
            FindObjectsOfType<CanvasGroup>();
            m_timeLastTouch = Time.timeSinceLevelLoad;
            m_faded = false;
        }
        else if (Input.GetMouseButton(0))
        {
            m_timeLastTouch = Time.timeSinceLevelLoad;
        }

        if (Time.timeSinceLevelLoad > m_timeLastTouch + m_delay && !m_faded && !ToggleableWindow.IsWindowUp)
        {
            m_fadeRoutine = StartCoroutine(FadeOut(FindObjectsOfType<CanvasGroup>()));
            m_faded = true;
        }
	}

    private IEnumerator FadeIn(CanvasGroup[] canvases)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].interactable = true;
            canvases[i].blocksRaycasts = true;
        }

        float startTime = Time.timeSinceLevelLoad;
        float canvasValue = 0;

        while (canvasValue < 1)
        {
            canvasValue += Time.deltaTime / m_fadeInLength;
            for(int i = 0; i < canvases.Length; i++)
                canvases[i].alpha = canvasValue;
            yield return null;
        }
    }

    private IEnumerator FadeOut(CanvasGroup[] canvases)
    {
        float startTime = Time.timeSinceLevelLoad;

        float canvasValue = 1;

        while (canvasValue > 0)
        {
            canvasValue -= Time.deltaTime / m_fadeOutLength;
            for (int i = 0; i < canvases.Length; i++)
                canvases[i].alpha = canvasValue;
            yield return null;
        }

        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].interactable = false;
            canvases[i].blocksRaycasts = false;
        }

        m_fadeRoutine = null;
    }
}
