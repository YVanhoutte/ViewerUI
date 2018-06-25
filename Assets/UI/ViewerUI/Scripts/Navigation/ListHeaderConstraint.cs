using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.bricsys.tune.UI.Nav
{
    public class ListHeaderConstraint : NavigationConstraint
    {
        [SerializeField] private string m_listName;
        [SerializeField] private RectTransform m_listContent;
        private Selectable[] m_contentElements;
        private Selectable FirstListElement { get { return m_contentElements.Length > 0? m_contentElements[0] : null; } }

        protected override void OverrideNavigation()
        {
            m_contentElements = GetContentElements();
            m_mySelectable.navigation = m_sourceNavigation; //Reset to source first
            Navigation finalNav = new Navigation { mode = Navigation.Mode.Explicit };

            switch (m_navigationOverride)
            {
                case LimitedNavigation.Horizontal:
                    finalNav.selectOnLeft = FirstListElement;
                    finalNav.selectOnRight = FirstListElement;
                    finalNav.selectOnUp = m_mySelectable.FindSelectableOnUp();
                    finalNav.selectOnDown = m_mySelectable.FindSelectableOnDown();
                    break;
                case LimitedNavigation.Vertical:
                    finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeft();
                    finalNav.selectOnRight = m_mySelectable.FindSelectableOnRight();
                    finalNav.selectOnUp = FirstListElement;
                    finalNav.selectOnDown = FirstListElement;
                    break;
                case LimitedNavigation.All:
                    finalNav.selectOnLeft = FirstListElement;
                    finalNav.selectOnRight = FirstListElement;
                    finalNav.selectOnUp = FirstListElement;
                    finalNav.selectOnDown = FirstListElement;
                    break;
            }
            FilterInactiveNavigations(ref finalNav);
            FilterCanceledNavigations(ref finalNav);
            m_mySelectable.navigation = finalNav;
            AssignSelfToNextElement();
        }

        private Selectable[] GetContentElements()
        {
            List<Selectable> temp = new List<Selectable>(m_listContent.GetComponentsInChildren<Selectable>());
            int count = temp.Count;
            for(int i = temp.Count -1; i >= 0; i--)
            {
                ListElement element = temp[i].GetComponent<ListElement>();
                if (element == null || element.ListName != m_listName)
                    temp.RemoveAt(i);
            }
            return temp.ToArray();
        }

        private void AssignSelfToNextElement()
        {
            switch(m_navigationOverride)
            {
                case LimitedNavigation.All:
                    OverrideToMe(Vector2.left);
                    OverrideToMe(Vector2.right);
                    OverrideToMe(Vector2.up);
                    OverrideToMe(Vector2.down);
                    break;
                case LimitedNavigation.Horizontal:
                    OverrideToMe(Vector2.left);
                    OverrideToMe(Vector2.right);
                    break;
                case LimitedNavigation.Vertical:
                    OverrideToMe(Vector2.up);
                    OverrideToMe(Vector2.down);
                    break;
            }
        }

        private void OverrideToMe(Vector2 direction)
        {
            if (direction == Vector2.up)
            {
                Selectable target = m_mySelectable.navigation.selectOnUp;
                if(target != null)
                {
                    Navigation nav = target.navigation;
                    nav.selectOnDown = m_mySelectable;
                    target.navigation = nav;
                    if(target.GetComponent<NavigationConstraint>())
                        target.GetComponent<NavigationConstraint>().Setup();
                }
            }
            else if(direction == Vector2.down)
            {
                Selectable target = m_mySelectable.navigation.selectOnDown;
                if (target != null)
                {
                    Navigation nav = target.navigation;
                    nav.selectOnUp = m_mySelectable;
                    target.navigation = nav;
                    if (target.GetComponent<NavigationConstraint>())
                        target.GetComponent<NavigationConstraint>().Setup();
                }
            }
            else if (direction == Vector2.left)
            {
                Selectable target = m_mySelectable.navigation.selectOnLeft;
                if (target != null)
                {
                    Navigation nav = target.navigation;
                    nav.selectOnRight = m_mySelectable;
                    target.navigation = nav;
                    if (target.GetComponent<NavigationConstraint>())
                        target.GetComponent<NavigationConstraint>().Setup();
                }
            }
            else if (direction == Vector2.right)
            {
                Selectable target = m_mySelectable.navigation.selectOnRight;
                if (target != null)
                {
                    Navigation nav = target.navigation;
                    nav.selectOnRight = m_mySelectable;
                    target.navigation = nav;
                    if (target.GetComponent<NavigationConstraint>())
                        target.GetComponent<NavigationConstraint>().Setup();
                }
            }
        }
    }
}
