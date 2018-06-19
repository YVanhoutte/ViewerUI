using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.bricsys.tune.TreeNode
{
    public enum Visibility { Full, Partial, None };

    public interface ITreeNode
    {
        string Name { get; }
        Vector3 Position { get; }
        Quaternion? Rotation { get; }
        IEnumerable<ITreeNode> Children { get; }
        Visibility Visibility { get; set; }
        /* TODO
         * ToString, should discuss what this outputs exactly....
         */
    }
}
