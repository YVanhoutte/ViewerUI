using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.bricsys.tune.TreeNode
{
    [RequireComponent(typeof(TreeUIController))]
    public class TreeGeneratorSceneComponents : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TreeUIController>().CreateTree(FindObjectsOfType<TreeNodeComponent>());
        }
    }
}
