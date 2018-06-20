using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.bricsys.tune.UI.Nav
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selectable))]
    public abstract class NavigationConstraint : MonoBehaviour
    {
        protected enum LimitedNavigation { None = 0, Horizontal = 1, Vertical = 2, All = 3 };

        protected Selectable m_mySelectable;
        protected Navigation m_sourceNavigation;
        [SerializeField] protected LimitedNavigation m_navigationOverride;
        [SerializeField] private bool m_noLeftNavigation, m_noRightNavigation, m_noUpNavigation, m_noDownNavigation;

        protected abstract void OverrideNavigation();

        internal virtual void Setup()
        {
            m_sourceNavigation = m_mySelectable.navigation;
            Debug.Log(string.Format("Calling Setup on: {0} source nav up: {1} down: {2} left: {3} right: {4}", 
                name, m_sourceNavigation.selectOnUp, m_sourceNavigation.selectOnDown, m_sourceNavigation.selectOnLeft, m_sourceNavigation.selectOnRight));
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

        protected void FilterCanceledNavigations(ref Navigation finalNav)
        {
            if (m_noUpNavigation)
                finalNav.selectOnUp = null;
            if (m_noDownNavigation)
                finalNav.selectOnDown = null;
            if (m_noLeftNavigation)
                finalNav.selectOnLeft = null;
            if (m_noRightNavigation)
                finalNav.selectOnRight = null;
        }

        private void Awake()
        {
            m_mySelectable = GetComponent<Selectable>();
        }

        private void OnEnable()
        {
            Setup();
            SelectableEvents.OnAllSelectablesChanged += OverrideNavigation;
            //OverrideNavigation();
        }

        private void OnDisable()
        {
            SelectableEvents.OnAllSelectablesChanged -= OverrideNavigation;
        }
    }
}
