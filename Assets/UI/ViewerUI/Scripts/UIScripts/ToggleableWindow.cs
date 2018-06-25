using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Canvas))]
public class ToggleableWindow : MonoBehaviour
{
    static public Action OnWindowToggled;
    [SerializeField] private Selectable m_firstSelected;
    public bool IsFocused { get { return CurrentWindow == this; } }
    protected Selectable[] Selectables { get { return GetComponentsInChildren<Selectable>(); } }
    private Canvas m_canvas;

    static public List<ToggleableWindow> ActiveWindows { get { return m_activeWindows; } }
    static public List<ToggleableWindow> AllWindows { get { return m_allWindows; } }
    static public ToggleableWindow CurrentWindow
    {
        get { return m_currentWindow; }
        private set
        {
            if (value == null)
            {
                //Debug.Log("Current Window set to null");
                m_currentWindow = null;
                EventSystem.current.SetSelectedGameObject(null); //Deselect if anything was selected
            }
            if (value != m_currentWindow)
            {
                //Debug.Log("Current Window set to " + value.name);
                m_currentWindow = value;
                m_currentWindow.SelectDefault();
            }
        }
    }
    static public bool IsWindowUp { get { return m_activeWindows.Count > 0; } }
    static public int CurrentWindowsCount { get { return m_activeWindows.Count; } }
    static private ToggleableWindow m_currentWindow;
    static private List<ToggleableWindow> m_activeWindows = new List<ToggleableWindow>();
    static private List<ToggleableWindow> m_allWindows = new List<ToggleableWindow>();
    static private WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

    public void Toggle()
    {
        //Debug.Log("Toggled!");
        gameObject.SetActive(!gameObject.activeSelf);
        var scrollRects = gameObject.GetComponentsInChildren<ScrollRect>();
        foreach(ScrollRect scroll in scrollRects)
        {
            scroll.normalizedPosition = new Vector2(0, 1);
        }
        if (OnWindowToggled != null)
            OnWindowToggled();
    }

    public bool ContainsSelectable(GameObject selectable)
    {
        return (ContainsSelectable(selectable.GetComponent<Selectable>()));
    }

    public bool ContainsSelectable(Selectable selectable)
    {
        bool contains = false;
        if (selectable == null)
        {
            return contains;
        }
        if (m_firstSelected != null && m_firstSelected.gameObject == selectable)
        {
            contains = true;
        }
        else if(Array.IndexOf(Selectables, selectable) >= 0)
        {
            contains = true;
        }
        //Debug.Log(string.Format("Checking if {0} contains {1} : {2}", gameObject.name, selectable.name, contains));
        return contains;
    }

    public void SelectDefault()
    {
        Canvas.ForceUpdateCanvases();
        //Debug.Log("Selecting default on " + gameObject.name);
        var selectablesCache = Selectables;
        if (m_firstSelected != null)
        {
            SelectionMethod(m_firstSelected);
        }
        else if (selectablesCache.Length > 0)
        {
            SelectionMethod(selectablesCache[0]);
        }
        //if (EventSystem.current.currentSelectedGameObject != null)
        //    Debug.Log("Currently selected: " + EventSystem.current.currentSelectedGameObject.name);
    }

    public void Focus()
    {
        CurrentWindow = this;
    }

    protected virtual void SelectionMethod(Selectable selectable)
    {
        selectable.Select();
    }

    private void OnEnable()
    {
        if (!m_activeWindows.Contains(this))
        {
            m_activeWindows.Add(this);
            CurrentWindow = this;
        }
    }

    private void OnDisable()
    {
        if(EventSystem.current == null)
        { }
        else if (EventSystem.current.currentSelectedGameObject == null ||      //If nothing is currently selected OR
            ContainsSelectable(EventSystem.current.currentSelectedGameObject)) //if the currently selected object is in the to be disabled window
        {
            FocusAdjacentWindow(this);
        }
        m_activeWindows.Remove(this);
    }

    private void Awake()
    {
        m_canvas = GetComponent<Canvas>();
        m_allWindows.Add(this);
    }

    private void OnDestroy()
    {
        m_allWindows.Remove(this);
    }

    private bool FocusWindowFrom(ToggleableWindow original, int indexSteps)
    {
        int newIndex = m_activeWindows.IndexOf(original) + indexSteps;
        if (0 <= newIndex && newIndex < m_activeWindows.Count)
        {
            CurrentWindow = m_activeWindows[newIndex];
            return true;
        }
        return false;
    }

    private void FocusAdjacentWindow(ToggleableWindow original)
    {
        if (!FocusWindowFrom(original, -1) && !FocusWindowFrom(original, 1))
        {
            CurrentWindow = null;
        }
    }
}
