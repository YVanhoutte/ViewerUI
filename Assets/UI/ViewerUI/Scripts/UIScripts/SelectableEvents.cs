using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableEvents : MonoBehaviour
{
    static public SelectableEvents Instance { get; private set; }

    static public Action OnAllSelectablesChanged;
    static private int m_allSelectablesCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
        m_allSelectablesCount = Selectable.allSelectables.Count;
    }

    private void Update()
    {
        if(m_allSelectablesCount != Selectable.allSelectables.Count)
        {
            m_allSelectablesCount = Selectable.allSelectables.Count;
            if (OnAllSelectablesChanged != null)
            {
                //Debug.Log("Amount of Objects to update: " + OnAllSelectablesChanged.GetInvocationList().Length);
                OnAllSelectablesChanged();
            }
        }
    }
}
