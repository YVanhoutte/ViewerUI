using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle), typeof(ListElement))]
public class TreeTogglesController : MonoBehaviour
{
    [SerializeField] private RectTransform m_content;
    private Toggle m_myToggle;
    private ListElement m_listElement;

    private void Awake()
    {
        m_listElement = GetComponent<ListElement>();
        m_myToggle = GetComponent<Toggle>();
        m_myToggle.onValueChanged.AddListener(ToggleList);
    }

    private void ToggleList(bool state)
    {
        ListElement[] listelements = m_content.GetComponentsInChildren<ListElement>();
        List<Toggle> toggles = new List<Toggle>();
        for(int i = 0; i < listelements.Length; i++)
        {
            if(listelements[i].ListName == m_listElement.ListName)
            {
                Toggle toggle = listelements[i].GetComponent<Toggle>();
                if (toggle != null && toggle.IsInteractable())
                    toggles.Add(toggle);
            }
        }
        for(int i = toggles.Count - 1; i >= 0; i--)
        {
            toggles[i].isOn = state;
        }
    }
}
