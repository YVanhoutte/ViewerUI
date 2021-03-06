﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.bricsys.tune.UI.Nav
{
    [RequireComponent(typeof(ListElement))]
    public class PerpendicularListNavigationConstraint : NavigationConstraint
    {
        protected ListElement m_myListElement;

        internal override void Setup()
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
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnLeft == null)
                        finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeftPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeft();
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnRight == null)
                        finalNav.selectOnRight = m_mySelectable.FindSelectableOnRightPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnRight = m_mySelectable.FindSelectableOnRight();
                    finalNav.selectOnUp = m_mySelectable.FindSelectableOnUp();
                    finalNav.selectOnDown = m_mySelectable.FindSelectableOnDown();
                    break;
                case LimitedNavigation.Vertical:
                    finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeft();
                    finalNav.selectOnRight = m_mySelectable.FindSelectableOnRight();
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnUp == null) //Only apply the override if the base navigation is explicit and null
                        finalNav.selectOnUp = m_mySelectable.FindSelectableOnUpPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnUp = m_mySelectable.FindSelectableOnUp();
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnDown == null)
                        finalNav.selectOnDown = m_mySelectable.FindSelectableOnDownPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnDown = m_mySelectable.FindSelectableOnDown();
                    break;
                case LimitedNavigation.All:
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnLeft == null)
                        finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeftPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnLeft = m_mySelectable.FindSelectableOnLeft();
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnRight == null)
                        finalNav.selectOnRight = m_mySelectable.FindSelectableOnRightPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnRight = m_mySelectable.FindSelectableOnRight();
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnUp == null) //Only apply the override if the base navigation is explicit and null
                        finalNav.selectOnUp = m_mySelectable.FindSelectableOnUpPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnUp = m_mySelectable.FindSelectableOnUp();
                    if (m_sourceNavigation.mode == Navigation.Mode.Explicit && m_sourceNavigation.selectOnDown == null)
                        finalNav.selectOnDown = m_mySelectable.FindSelectableOnDownPerpendicular(FilterOnListElements);
                    else
                        finalNav.selectOnDown = m_mySelectable.FindSelectableOnDown();
                    break;
            }
            FilterInactiveNavigations(ref finalNav);
            FilterCanceledNavigations(ref finalNav);
            m_mySelectable.navigation = finalNav;
        }
    }
}
