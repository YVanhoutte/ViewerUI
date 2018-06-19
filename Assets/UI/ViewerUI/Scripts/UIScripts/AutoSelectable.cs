using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class AutoSelectable: MonoBehaviour, IPointerEnterHandler
{
    private Selectable m_selectable;

    private void Awake()
    {
        m_selectable = GetComponent<Selectable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_selectable.Select();
    }

    private void OnDestroy()
    {
        if (EventSystem.current == null)
            return;
        if(EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (m_selectable.FindSelectableOnUp() != null)
                m_selectable.FindSelectableOnUp().Select();
            else if (m_selectable.FindSelectableOnDown() != null)
                m_selectable.FindSelectableOnDown().Select();
            else if (m_selectable.FindSelectableOnLeft() != null)
                m_selectable.FindSelectableOnLeft().Select();
            else if (m_selectable.FindSelectableOnRight() != null)
                m_selectable.FindSelectableOnRight().Select();
        }
    }
}
