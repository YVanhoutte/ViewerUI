using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.bricsys.tune.UI.Nav
{
    [RequireComponent(typeof(ListElement))]
    public class NamedListConstraint : NavigationConstraint
    {
        private Transform m_myElement;
        private ListElement m_myListElement;

        protected override void Awake()
        {
            base.Awake();
            m_myElement = transform.parent.parent; //Reaches up to OverViewTreeElement
            m_myListElement = GetComponent<ListElement>();
        }

        protected override void OverrideNavigation()
        {
            m_mySelectable.navigation = m_sourceNavigation;
            Navigation newNav = new Navigation
            {
                mode = Navigation.Mode.Explicit
            };

            switch (m_navigationOverride)
            {
                case LimitedNavigation.Horizontal:
                    SetHorizontal(ref newNav);
                    AutoVertical(ref newNav);
                    break;
                case LimitedNavigation.Vertical:
                    SetVertical(ref newNav);
                    AutoHorizontal(ref newNav);
                    break;
                case LimitedNavigation.All:
                    SetHorizontal(ref newNav);
                    SetVertical(ref newNav);
                    break;
            }

            FilterCanceledNavigations(ref newNav);
            FilterInactiveNavigations(ref newNav);
            m_mySelectable.navigation = newNav;
        }

        private void SetHorizontal(ref Navigation navigation)
        {
            navigation.selectOnLeft = m_mySelectable.FindSelectableOnLeft(FilterOnListElements);
            navigation.selectOnRight = m_mySelectable.FindSelectableOnRight(FilterOnListElements);
        }

        private void SetVertical(ref Navigation navigation)
        {
            int elementSiblingIndex = m_myElement.GetSiblingIndex();
            Transform containingElement = m_myElement.parent.parent;

            if (elementSiblingIndex != 0)
                navigation.selectOnUp = FindLastSelectableWithListName(m_myElement.parent.GetChild(elementSiblingIndex - 1).gameObject);
            else if(containingElement.childCount > 1)
                navigation.selectOnUp = FindLastSelectableWithListName(containingElement.GetChild(0).gameObject); // Goes to its parent up

            if(m_myElement.GetChild(1).childCount > 0) //Check first for children
                navigation.selectOnDown = FindFirstSelectableWithListName(m_myElement.GetChild(1).gameObject);
            else if (elementSiblingIndex < m_myElement.parent.childCount - 1) //Then check for siblings
                navigation.selectOnDown = FindFirstSelectableWithListName(m_myElement.parent.GetChild(elementSiblingIndex + 1).gameObject);
            else //Finally check for siblings of parents
                navigation.selectOnDown = FindLowerInUpperHierarchy(containingElement);            
        }

        private Selectable FindLowerInUpperHierarchy(Transform element)
        {
            int elementSiblingIndex = element.GetSiblingIndex();
            if (elementSiblingIndex < element.parent.childCount - 1)
            {
                Selectable eligible = FindFirstSelectableWithListName(element.parent.GetChild(elementSiblingIndex + 1).gameObject);
                if (eligible != null)
                    return eligible;
            }
            return FindLowerInUpperHierarchy(element.parent);
        }

        private GameObject FindUpperInUpperHierarchy(Transform element)
        {
            int elementSiblingIndex = element.GetSiblingIndex();
            if (elementSiblingIndex > 0)
                return element.parent.GetChild(elementSiblingIndex - 1).gameObject;
            else
                return FindUpperInUpperHierarchy(element.parent);
        }

        private void AutoHorizontal( ref Navigation navigation)
        {
            navigation.selectOnLeft = m_sourceNavigation.selectOnLeft;
            navigation.selectOnRight = m_sourceNavigation.selectOnRight;
        }

        private void AutoVertical(ref Navigation navigation)
        {
            navigation.selectOnUp = m_sourceNavigation.selectOnUp;
            navigation.selectOnDown = m_sourceNavigation.selectOnDown;
        }

        private Selectable FindLastSelectableWithListName(GameObject container)
        {
            var lists = container.GetComponentsInChildren<ListElement>();
            for(int i = lists.Length -1; i >= 0; i--)
            {
                if(lists[i].ListName.Equals(m_myListElement.ListName))
                    return lists[i].GetComponent<Selectable>();          
            }
            return null;
        }

        private Selectable FindFirstSelectableWithListName(GameObject container)
        {
            var lists = container.GetComponentsInChildren<ListElement>();
            for (int i = 0; i < lists.Length; i++)
            {
                if (lists[i].ListName.Equals(m_myListElement.ListName))
                    return lists[i].GetComponent<Selectable>();           
            }
            return null;
        }
    }
}