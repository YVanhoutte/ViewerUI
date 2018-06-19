using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.bricsys.tune.UI.Nav
{
    [RequireComponent(typeof(ListElement))]
    public class PerpendicularListNavigationConstraint : NavigationConstraint
    {
        private enum LimitedNavigation { None = 0, Horizontal = 1, Vertical = 2, All = 3 };

        [SerializeField] private LimitedNavigation m_navigationOverride;
        private ListElement m_myListElement;

        protected override void Setup()
        {
            base.Setup();
            if (m_myListElement == null)
                m_myListElement = GetComponent<ListElement>();
        }

        protected override void OverrideNavigation()
        {
            m_mySelectable.navigation = m_sourceNavigation; //Reset to source first
            Navigation finalNav = new Navigation { mode = Navigation.Mode.Explicit };

            switch (m_navigationOverride)
            {
                case LimitedNavigation.Horizontal:
                    finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeftPerpendicular(FilterOnListElements);
                    finalNav.selectOnRight = m_mySelectable.FindSelectableOnRightPerpendicular(FilterOnListElements);
                    finalNav.selectOnUp = m_mySelectable.FindSelectableOnUp();
                    finalNav.selectOnDown = m_mySelectable.FindSelectableOnDown();
                    break;
                case LimitedNavigation.Vertical:
                    finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeft();
                    finalNav.selectOnRight = m_mySelectable.FindSelectableOnRight();
                    finalNav.selectOnUp = m_mySelectable.FindSelectableOnUpPerpendicular(FilterOnListElements);
                    finalNav.selectOnDown = m_mySelectable.FindSelectableOnDownPerpendicular(FilterOnListElements);
                    break;
                case LimitedNavigation.All:
                    finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeftPerpendicular(FilterOnListElements);
                    finalNav.selectOnRight = m_mySelectable.FindSelectableOnRightPerpendicular(FilterOnListElements);
                    finalNav.selectOnUp = m_mySelectable.FindSelectableOnUpPerpendicular(FilterOnListElements);
                    finalNav.selectOnDown = m_mySelectable.FindSelectableOnDownPerpendicular(FilterOnListElements);
                    break;
            }
            FilterInactiveNavigations(ref finalNav);
            FilterCanceledNavigations(ref finalNav);
            m_mySelectable.navigation = finalNav;
        }

        private bool FilterOnListElements(Selectable a, Selectable b)
        {
            ListElement al = a.GetComponent<ListElement>();
            ListElement bl = b.GetComponent<ListElement>();

            if (al == null && bl == null)
                return true;
            if ((al == null && bl != null) || (al != null && bl == null))
                return false;
            return al.ListName.Equals(bl.ListName);
        }
    }
}
