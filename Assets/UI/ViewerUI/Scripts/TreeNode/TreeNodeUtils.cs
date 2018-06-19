using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.bricsys.tune.TreeNode
{
    static public class TreeNodeUtils
    {
        static public ITreeNode FindRoot(IEnumerable<ITreeNode> nodes)
        {
            ITreeNode first = nodes.FirstOrDefault();
            if (first == null)
                return null;
            return FindParentRecursive(first, nodes);
        }

        static public ITreeNode FindParent(ITreeNode firstNode, IEnumerable<ITreeNode> nodes)
        {
            foreach (ITreeNode node in nodes)
            {
                foreach (ITreeNode child in node.Children)
                {
                    if (child.Equals(firstNode))
                    {
                        return node;
                    }
                }
            }
            return firstNode;
        }

        static private ITreeNode FindParentRecursive(ITreeNode firstNode, IEnumerable<ITreeNode> nodes)
        {
            foreach (ITreeNode node in nodes)
            {
                foreach (ITreeNode child in node.Children)
                {
                    if (child.Equals(firstNode))
                    {
                        //Debug.Log(string.Format("Found a new parent! {0} is the parent of old result {1}", node.Name, firstNode.Name));
                        return FindParentRecursive(node, nodes);
                    }
                }
            }
            return firstNode;
        }
    }
}
