using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class EventSystemExpansion : MonoBehaviour
{
    public delegate void EventAction(GameObject selection);
    static public EventAction OnSelectionChanged;
    private GameObject m_currentSelected;

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != m_currentSelected)
        {
            m_currentSelected = EventSystem.current.currentSelectedGameObject;
            if (OnSelectionChanged != null)
                OnSelectionChanged(m_currentSelected);
        }
    }
}
