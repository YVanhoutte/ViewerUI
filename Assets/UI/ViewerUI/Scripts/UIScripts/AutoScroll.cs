using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class AutoScroll : MonoBehaviour
{
    public float scrollSpeed = 10f;

    private RectTransform m_contentPanel, m_rectTransform;
    private ScrollRect m_scrollRect;

    private void Awake()
    {
        m_scrollRect = GetComponent<ScrollRect>();
        m_rectTransform = GetComponent<RectTransform>();
        m_contentPanel = m_scrollRect.content;
    }

    private void OnEnable()
    {
        EventSystemExpansion.OnSelectionChanged += UpdateScrollTo;
    }

    private void OnDisable()
    {
        EventSystemExpansion.OnSelectionChanged -= UpdateScrollTo;
    }

    private void UpdateScrollTo(GameObject selection)
    {
        //TODO: WORKS BUT IS A BIT UGLY
        if (selection == null || !selection.transform.IsChildOf(m_contentPanel.transform))
            return;

        Canvas.ForceUpdateCanvases();

        Vector3[] corners = new Vector3[4];
        m_contentPanel.GetWorldCorners(corners);
        Vector3[] selCorners = new Vector3[4];
        RectTransform selectRect = selection.GetComponent<RectTransform>();
        selectRect.GetWorldCorners(selCorners);
        float verticalPos = corners[1].y - selCorners[1].y;
        verticalPos /= (m_contentPanel.rect.height * transform.lossyScale.y);
        //Debug.Log("Vertical Offset: " + verticalPos + " rect height: " + m_contentPanel.rect.height);
        verticalPos = 1 - verticalPos;
        if (verticalPos < 0.05f) //Bit of a hack to snap to the bottom
            verticalPos = 0;
        m_scrollRect.verticalNormalizedPosition = verticalPos;
    }
}
