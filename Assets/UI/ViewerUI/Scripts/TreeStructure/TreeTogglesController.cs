using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TreeTogglesController : MonoBehaviour
{
    [SerializeField] private RectTransform m_content;
    [SerializeField] private string m_listName;
    private Toggle m_myToggle;

    private void Awake()
    {
        m_myToggle = GetComponent<Toggle>();
        m_myToggle.onValueChanged.AddListener(ToggleList);
    }

    private void ToggleList(bool state)
    {
        ListElement[] listelements = m_content.GetComponentsInChildren<ListElement>();
        List<Toggle> toggles = new List<Toggle>();
        for(int i = 0; i < listelements.Length; i++)
        {
            if(listelements[i].ListName == m_listName)
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
