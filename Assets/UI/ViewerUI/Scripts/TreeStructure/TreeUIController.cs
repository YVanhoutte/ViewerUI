using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.bricsys.tune.TreeNode;

public class TreeUIController : MonoBehaviour
{
    [SerializeField] private ObjectPooler m_pool;
    [SerializeField] private Transform m_contentPanel;

    public void AddElement(TreeNodeUI data, TreeNodeUI parent)
    {
        if (parent == null)
        {
            data.transform.SetParent(m_contentPanel, false);
        }
        else
        {
            parent.AddChild(data);
        }
    }

    public void CreateTree(IEnumerable<ITreeNode> nodes)
    {
        InstantiateTree(TreeNodeUtils.FindRoot(nodes), null);
    }

    private void InstantiateTree(ITreeNode root, TreeNodeUI parent)
    {
        if (root == null)
            return;
        TreeNodeUI instance = m_pool.NextObject.GetComponent<TreeNodeUI>();
        instance.gameObject.SetActive(true);
        instance.Set(root);
        AddElement(instance, parent);
        foreach(ITreeNode child in root.Children)
        {
            InstantiateTree(child, instance);
        }
    }
}