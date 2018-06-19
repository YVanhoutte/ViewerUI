using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class SearchBar : MonoBehaviour, ISelectHandler
{
    [SerializeField] private RectTransform m_contentPanel;
    private List<SearchableElement> m_allTexts = new List<SearchableElement>();
    private List<SearchableElement> m_searchTexts = new List<SearchableElement>();
    private InputField m_myInput;

    private void Awake()
    {
        m_myInput = GetComponent<InputField>();
        m_myInput.onValueChanged.AddListener(Search);
    }

    public void OnSelect(BaseEventData eventData)
    {
        m_allTexts.Clear();
        SearchableElement[] allTexts = m_contentPanel.GetComponentsInChildren<SearchableElement>();
        for(int i = 0; i < allTexts.Length; i++)
        {
            m_allTexts.Add(allTexts[i]);
        }
    }

    private void Search(string arg0)
    {
        if(string.IsNullOrEmpty(arg0))
        {
            Reset();
        }
        else
        {
            Reset();
            for(int i = 0; i < m_allTexts.Count; i++)
            {
                if(!m_allTexts[i].Text.Contains(arg0))
                {
                    m_searchTexts.Add(m_allTexts[i]);
                }
            }

            for(int i = 0; i < m_searchTexts.Count; i++)
            {
                m_searchTexts[i].Hide();
            }
        }
    }

    private void Reset()
    {
        for(int i = 0; i < m_searchTexts.Count; i++)
        {
            m_searchTexts[i].Show();
        }
        m_searchTexts.Clear();
    }
}
