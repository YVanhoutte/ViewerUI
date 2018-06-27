using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.bricsys.tune.UI.Nav
{
    [RequireComponent(typeof(TreeNodeUI))]
    public class TreeNodeConstraint : MonoBehaviour
    {
        protected TreeNodeUI m_myTreeNode;
        [SerializeField] protected Selectable m_expandToggle, m_nameField, m_VisibleToggle;
        private Navigation m_originalExpand, m_originalName, m_originalVisible;

        protected void Awake()
        {
            m_myTreeNode = GetComponent<TreeNodeUI>();
            m_originalExpand = m_expandToggle.navigation;
            m_originalName = m_nameField.navigation;
            m_originalVisible = m_VisibleToggle.navigation;
        }

        protected void OverrideNavigation()
        {
            TreeNodeConstraint regularUp   = FindUpConstraint();
            TreeNodeConstraint regularDown = FindDownConstraint();
            TreeNodeConstraint expandUp    = FindUpConstraintForExpandToggle();
            TreeNodeConstraint expandDown  = FindDownConstraintForExpandToggle();
            //Debug.Log(string.Format("{0} Up Element: {1} Down Element: {2}", name,
            //    regularUp != null? regularUp.name: "None",
            //    regularDown != null? regularDown.name : "None"));
            //Debug.Log(string.Format("{0} Up Element with active expand: {1} Down Element with active expand: {2}", name,
            //    expandUp != null ? expandUp.name : "None",
            //    expandDown != null ? expandDown.name : "None"));

            m_expandToggle.navigation  = OverrideNavigation(m_originalExpand,  expandUp != null ? expandUp.m_expandToggle : null, 
                                                                                    expandDown != null ? expandDown.m_expandToggle : null);
            m_nameField.navigation     = OverrideNavigation(m_originalName,    regularUp != null ? regularUp.m_nameField : null,
                                                                                    regularDown != null ? regularDown.m_nameField : null);
            m_VisibleToggle.navigation = OverrideNavigation(m_originalVisible, regularUp != null ? regularUp.m_VisibleToggle : null,
                                                                                    regularDown != null ? regularDown.m_VisibleToggle : null);
        }

        private Navigation OverrideNavigation(Navigation originalNavigation, Selectable up, Selectable down)
        {
            Navigation newNav = new Navigation { mode = Navigation.Mode.Explicit };
            newNav.selectOnUp = up;
            newNav.selectOnDown = down;
            AutoHorizontal(ref newNav, originalNavigation);
            FilterInactiveNavigations(ref newNav);
            return newNav;
        }

        protected TreeNodeConstraint FindLastChild()
        {
            TreeNodeConstraint[] constraints = GetComponentsInChildren<TreeNodeConstraint>(false);
            if (constraints.Length == 1) //Since GetComponentsInChildren includes this, with no active children we get this as element 0
                return this;
            else
                return constraints[constraints.Length - 1].FindLastChild();
        }

        private TreeNodeConstraint FindUpConstraintForExpandToggle()
        {
            TreeNodeConstraint up = FindUpConstraint();
            if (up == null)
                return null;

            if (up.m_expandToggle.IsActive())
                return up;
            else
                return up.FindUpConstraintForExpandToggle();
        }

        private TreeNodeConstraint FindUpConstraint()
        {
            int elementSiblingIndex = transform.GetSiblingIndex();
            TreeNodeConstraint containingElement = transform.parent.GetComponentInParent<TreeNodeConstraint>();

            if (elementSiblingIndex != 0)
                return containingElement.transform.GetChild(1).GetChild(elementSiblingIndex - 1).GetComponent<TreeNodeConstraint>().FindLastChild();
            else
                return containingElement;
        }

        private TreeNodeConstraint FindDownConstraintForExpandToggle()
        {
            TreeNodeConstraint down = FindDownConstraint();
            if (down == null)
                return null;

            if (down.m_expandToggle.IsActive())
                return down;
            else
                return down.FindDownConstraintForExpandToggle();
        }

        private TreeNodeConstraint FindDownConstraint()
        {
            int elementSiblingIndex = transform.GetSiblingIndex();
            TreeNodeConstraint containingElement = transform.parent.GetComponentInParent<TreeNodeConstraint>();
            TreeNodeConstraint firstChild = transform.GetChild(1).GetComponentInChildren<TreeNodeConstraint>(false); //This still returns inactive children....

            if (firstChild != null && transform.GetChild(1).gameObject.activeSelf)
                return firstChild;
            if (elementSiblingIndex <= containingElement.transform.GetChild(1).childCount - 2) // -2 because -1 for 0-index, and we want to know if this is NOT the last child
                return containingElement.transform.GetChild(1).GetChild(elementSiblingIndex + 1).GetComponent<TreeNodeConstraint>();
            return containingElement.FindNextConstraint();
        }

        private TreeNodeConstraint FindNextConstraint()
        {
            int elementSiblingIndex = transform.GetSiblingIndex();
            TreeNodeConstraint containingElement = transform.parent.GetComponentInParent<TreeNodeConstraint>();
            if (containingElement == null)
                return null;
            if(elementSiblingIndex <= containingElement.transform.GetChild(1).childCount - 2) // -2 because -1 for 0-index, and we want to know if this is NOT the last child
                return containingElement.transform.GetChild(1).GetChild(elementSiblingIndex + 1).GetComponent<TreeNodeConstraint>();
            return containingElement.FindNextConstraint();
        }

        private void AutoHorizontal(ref Navigation navigation, Navigation originalNavigation)
        {
            navigation.selectOnLeft = originalNavigation.selectOnLeft;
            navigation.selectOnRight = originalNavigation.selectOnRight;
        }

        protected void FilterInactiveNavigations(ref Navigation finalNav)
        {
            if (finalNav.selectOnUp != null && !finalNav.selectOnUp.IsInteractable())
                finalNav.selectOnUp = null;
            if (finalNav.selectOnDown != null && !finalNav.selectOnDown.IsInteractable())
                finalNav.selectOnDown = null;
            if (finalNav.selectOnLeft != null && !finalNav.selectOnLeft.IsInteractable())
                finalNav.selectOnLeft = null;
            if (finalNav.selectOnRight != null && !finalNav.selectOnRight.IsInteractable())
                finalNav.selectOnRight = null;
        }

        private void OnEnable()
        {
            SelectableEvents.OnAllSelectablesChanged += OverrideNavigation;
        }

        private void OnDisable()
        {
            SelectableEvents.OnAllSelectablesChanged -= OverrideNavigation;
        }
    }
}
