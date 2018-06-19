using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollSpeedUpdater : MonoBehaviour
{
    [SerializeField] private float m_baseSpeed = 1;
    [SerializeField] private RectTransform m_content;
    private ScrollRect m_scrollRect;

    private void Awake ()
    {
        m_scrollRect = GetComponent<ScrollRect>();
	}
	
	private void Update ()
    {
        float contentHeight = m_content.rect.height;
        float viewPortHeight = m_scrollRect.viewport.rect.height;
        m_scrollRect.scrollSensitivity = contentHeight/viewPortHeight * m_baseSpeed;
	}
}
