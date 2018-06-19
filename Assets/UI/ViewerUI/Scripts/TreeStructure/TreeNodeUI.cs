using com.bricsys.tune.TreeNode;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeNodeUI : MonoBehaviour
{
    public delegate void TreeNodeChildEvent(TreeNodeUI child);
    public TreeNodeChildEvent OnChildAdded, OnChildRemoved;
    public int Depth
    {
        get { return m_depth; }
        set
        {
            m_depth = value;
            SetDepthTabs();
        }
    }
    public RectTransform ChildElementsPanel { get { return m_childElementsPanel; } }
    [SerializeField] protected Toggle m_expandToggle;
    [SerializeField] protected Button m_visibilityButton;
    [SerializeField] protected Button m_nameButton;
    [SerializeField] private TreeNodeUI m_subElementPrefab;
    [SerializeField] private RectTransform m_childElementsPanel; 
    [SerializeField] private LayoutElement m_spacingPanel;
    private Text m_name;
    private int m_depth;
    private List<TreeNodeToggle> m_subElementToggles = new List<TreeNodeToggle>();
    private List<TreeNodeUI> m_subElements = new List<TreeNodeUI>();
    private ITreeNode m_treeNode;
    [SerializeField] private float m_indentIncrement = 20;

    public void Set(ITreeNode treeNode)
    {
        m_treeNode = treeNode;
        gameObject.name = "Tab_" + m_treeNode.Name;
        int childrenCount = 0;
        foreach(ITreeNode child in m_treeNode.Children)
            childrenCount++;

        if(childrenCount == 0)
            m_visibilityButton.onClick.AddListener(() => ToggleVisibility());
        else
            m_visibilityButton.interactable = false;
        SetVisibilityText();
        m_name.text = treeNode.Name;
        m_nameButton.onClick.AddListener(() => Inspect(treeNode));
    }

    public void AddChild(TreeNodeUI data)
    {
        m_subElements.Add(data);
        data.transform.SetParent(m_childElementsPanel, false);
        data.Depth = Depth + 1;
        ToggleExpandable();
        if(OnChildAdded != null)
            OnChildAdded(data);
    }

    public void RemoveChild(TreeNodeUI data)
    {
        m_subElements.Remove(data);
        ToggleExpandable();
        if(OnChildRemoved != null)
            OnChildRemoved(data);
    }

    private void Awake()
    {
        m_name = m_nameButton.GetComponentInChildren<Text>();
        m_expandToggle.onValueChanged.AddListener(ToggleExpansion);
        ToggleExpandable();
    }

    private void Inspect(ITreeNode treeNode)
    {
        if(InspecterCamera.Active != null)
        {
            if(InspecterCamera.Active.Inspect(treeNode))
                GetComponentInParent<ToggleableWindow>().Toggle();
        }
    }

    private void SetDepthTabs()
    {
        m_spacingPanel.preferredWidth = m_indentIncrement * Depth;
        m_spacingPanel.minWidth = m_indentIncrement * Depth;
        //Debug.Log(string.Format("{0} depth = {1} size = {2}", gameObject.name, Depth, m_spacingPanel.preferredWidth));
    }

    private void ToggleExpandable()
    {
        if (m_subElements.Count == 0)
        {
            m_expandToggle.gameObject.SetActive(false);
            //m_expandToggle.interactable = false;
            //m_expandToggle.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            m_expandToggle.gameObject.SetActive(true);
            //m_expandToggle.interactable = true;
            //m_expandToggle.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    private void ToggleExpansion(bool state)
    {
        m_childElementsPanel.gameObject.SetActive(state);
    }

    private void ToggleVisibility()
    {
        Visibility newState = m_treeNode.Visibility;
        if (newState == Visibility.Full)
            newState = Visibility.None;
        else if (newState == Visibility.None)
            newState = Visibility.Full;
        m_treeNode.Visibility = newState;

        TreeNodeUI[] parents = GetComponentsInParent<TreeNodeUI>();
        for (int i = 0; i < parents.Length; i++) //TODO: Make this a little nicer?
            parents[i].SetVisibilityText();
    }

    private void SetVisibilityText()
    {
        m_visibilityButton.GetComponentInChildren<Text>().text = m_treeNode.Visibility.ToString();
    }
}
