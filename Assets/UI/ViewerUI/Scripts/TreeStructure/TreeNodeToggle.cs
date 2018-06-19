using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle), typeof(ListElement))]
public class TreeNodeToggle : MonoBehaviour
{
    public string ListName { get {
            if (m_list == null)
                m_list = GetComponent<ListElement>();
            return m_list.ListName; } }
    public Toggle Toggle { get {
            if (m_myToggle == null)
                m_myToggle = GetComponent<Toggle>();
            return m_myToggle; } }
    private Toggle m_myToggle;
    private TreeNodeUI m_myNode;
    private ListElement m_list;
    private List<TreeNodeToggle> m_children = new List<TreeNodeToggle>();

	private void Awake ()
    {
        m_myNode = GetComponentInParent<TreeNodeUI>();
        if(m_myNode != null)
        {
            m_myNode.OnChildAdded += AddChild;
            m_myNode.OnChildRemoved += RemoveChild;
        }

        Toggle.onValueChanged.AddListener(UpdateChildren);
	}

    private void OnDestroy()
    {
        if(m_myNode != null)
        {
            m_myNode.OnChildAdded -= AddChild;
            m_myNode.OnChildRemoved -= RemoveChild;
        }
    }

    private void AddChild(TreeNodeUI child)
    {
        int counter = 0;
        TreeNodeToggle[] childtoggles = child.GetComponentsInChildren<TreeNodeToggle>(true);
        for(int i = 0; i < childtoggles.Length; i++)
        {
            if (!m_children.Contains(childtoggles[i]) && childtoggles[i].ListName == ListName)
            {
                m_children.Add(childtoggles[i]);
                counter++;
            }
        }
        //Debug.Log(string.Format("{0} is adding {1} children from {2} found toggleables.", name, counter, childtoggles.Length));
    }

    private void RemoveChild(TreeNodeUI child)
    {
        TreeNodeToggle[] childtoggles = child.GetComponentsInChildren<TreeNodeToggle>(true);
        for (int i = 0; i < childtoggles.Length; i++)
        {
            if (!m_children.Contains(childtoggles[i]))
                m_children.Remove(childtoggles[i]);
        }
    }

    private void UpdateChildren(bool state)
    {
        //Debug.Log(string.Format("{0} is updating {1} children to {2}", name, m_children.Count, state));
        for(int i = 0; i < m_children.Count; i++)
        {
            m_children[i].Toggle.isOn = state;
        }
    }
}
