using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.bricsys.tune.UI.Nav
{
    [RequireComponent(typeof(Selectable))]
    public abstract class NavigationConstraint : MonoBehaviour
    {
        protected Selectable m_mySelectable;
        protected Navigation m_sourceNavigation;
        [SerializeField] private bool m_noLeftNavigation, m_noRightNavigation, m_noUpNavigation, m_noDownNavigation;

        protected abstract void OverrideNavigation();

        protected virtual void Setup()
        {
            if (m_mySelectable == null)
            {
                m_mySelectable = GetComponent<Selectable>();
                m_sourceNavigation = m_mySelectable.navigation;
            }
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

        private void Start()
        {
            OverrideNavigation();
        }

        private void OnEnable()
        {
            Setup();
            SelectableEvents.OnAllSelectablesChanged += OverrideNavigation;
            OverrideNavigation();
        }

        private void OnDisable()
        {
            SelectableEvents.OnAllSelectablesChanged -= OverrideNavigation;
        }
    }
}
