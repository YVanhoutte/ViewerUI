using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchableElement : MonoBehaviour {

    public string Text { get { return m_text.text; } }
    [SerializeField] private Text m_text;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if(m_text == null)
        {
            Debug.LogWarning("No Text is assigned to Searchable Element " + name);
            m_text = GetComponentInChildren<Text>();
        }
    }
}
