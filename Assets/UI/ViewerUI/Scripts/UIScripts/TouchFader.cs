using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public sealed class TouchFader : MonoBehaviour
{
    [SerializeField] private float m_delay = 2f, m_fadeInLength = 0.5f, m_fadeOutLength = 0.5f;
    [SerializeField] private bool m_requireClickOnRect = false;
    private float m_timeLastTouch;
    private bool m_faded = true, m_canFadeIn = true;
    private CanvasGroup[] m_canvases;
    private Coroutine m_coroutine;

    public void DelayFadeOut()
    {
        m_timeLastTouch = Time.timeSinceLevelLoad;
    }

    private void Start ()
    {
        m_canvases = GetComponentsInChildren<CanvasGroup>();
        if(m_canvases.Length == 0)
        {
            Debug.LogWarning(string.Format("{0} has a TouchFader component but no CanvasGroups in itself or its children to work on. Touchfader has been deactivated.", name));
            enabled = false;
        }

        for(int i = 0; i < m_canvases.Length; i++)
        {
            m_canvases[i].alpha = 0;
            m_canvases[i].interactable = false;
            m_canvases[i].blocksRaycasts = false;
        }
        ToggleableWindow.OnWindowToggled += DelayFadeOut;
	}
	
	private void Update ()
    {
        bool pointerOnGO = false;
#if UNITY_ANDROID || UNITY_IOS
        pointerOnGO = EventSystem.current.IsPointerOverGameObject(0);
#else
        pointerOnGO = EventSystem.current.IsPointerOverGameObject();
#endif
        if (Input.GetMouseButtonDown(0) && m_canFadeIn && !pointerOnGO)
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn());
            m_timeLastTouch = Time.timeSinceLevelLoad;
            m_faded = false;
        }
        else if (Input.GetMouseButton(0) && !m_requireClickOnRect)
            DelayFadeOut();

        if (Time.timeSinceLevelLoad > m_timeLastTouch + m_delay && !m_faded && !ToggleableWindow.IsWindowUp)
        {
            StartCoroutine(FadeOut());
            m_faded = true;
        }
	}

    private IEnumerator FadeIn()
    {
        m_canFadeIn = false;
        ToggleCanvasPhysical(true);
        float startTime = Time.timeSinceLevelLoad;
        float canvasValue = m_canvases[0].alpha;

        while (canvasValue < 1)
        {
            canvasValue += Time.deltaTime / m_fadeInLength;
            for(int i = 0; i < m_canvases.Length; i++)
                m_canvases[i].alpha = canvasValue;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        m_canFadeIn = true;
        ToggleCanvasPhysical(false);
        float startTime = Time.timeSinceLevelLoad;
        float canvasValue = m_canvases[0].alpha;

        while (canvasValue > 0)
        {
            canvasValue -= Time.deltaTime / m_fadeOutLength;
            for(int i = 0; i < m_canvases.Length; i++)
                m_canvases[i].alpha = canvasValue;
            yield return null;
        }
    }

    private void ToggleCanvasPhysical(bool stateTo)
    {
        for (int i = 0; i < m_canvases.Length; i++)
        {
            m_canvases[i].interactable = stateTo;
            m_canvases[i].blocksRaycasts = stateTo;
        }
    }
}
