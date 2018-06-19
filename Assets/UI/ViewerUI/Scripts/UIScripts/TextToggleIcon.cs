using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextToggleIcon : MonoBehaviour
{
    [SerializeField] private string m_onText, m_offText;
    private Text m_mySymbol;

    private void Awake ()
    {
        m_mySymbol = GetComponent<Text>();
        Toggle myToggle = GetComponentInParent<Toggle>();
        myToggle.onValueChanged.AddListener(ToggleSymbol);
        ToggleSymbol(myToggle.isOn);
	}

    private void ToggleSymbol(bool value)
    {
        if (value == true)
            m_mySymbol.text = m_offText;
        else
            m_mySymbol.text = m_onText;
    }

}
