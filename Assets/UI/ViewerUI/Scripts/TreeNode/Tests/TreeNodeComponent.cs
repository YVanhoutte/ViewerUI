using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.bricsys.tune.TreeNode
{
    public class TreeNodeComponent : MonoBehaviour, ITreeNode
    {
        public string Name { get { return name; } }
        public Vector3 Position { get
            {   Quaternion rot = Quaternion.identity;
                if (Rotation != null)
                    rot = (Quaternion)Rotation;
                return transform.position + (rot * (Vector3.back * 5)); } }
        public Quaternion? Rotation { get { return Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); } }
        public IEnumerable<ITreeNode> Children { get { return m_children; } }
        public Visibility Visibility
        {
            get
            {
                int childrenCount = 0;
                Visibility result = Visibility.Full;
                foreach (ITreeNode child in Children)
                {
                    childrenCount++;
                    if (child.Visibility == result)
                        continue;
                    else if (childrenCount == 1)
                        result = child.Visibility;
                    else
                    {
                        result = Visibility.Partial;
                        break;
                    }
                }
                if(childrenCount == 0) //If leaf node, return depending on gameobject active
                {
                    if (!gameObject.activeSelf)
                        result = Visibility.None;
                    else
                        result = Visibility.Full;
                }
                return result;
            }
            set
            {
                int childrenCount = 0;
                foreach(ITreeNode child in Children)
                {
                    childrenCount++;
                }
                //Debug.Log(string.Format("Amount of children in {0}: {1}", name, childrenCount));
                if(childrenCount == 0) //If leaf node, it's settable
                {
                    //Debug.Log(string.Format("Setting leaf node {0} to {1}.", name, value.ToString()));
                    if (value == Visibility.Full)
                        gameObject.SetActive(true);
                    else if (value == Visibility.None)
                        gameObject.SetActive(false);
                }
            }
        }

        [SerializeField] private TreeNodeComponent[] m_children;

        private void Awake()
        {
            m_children = new TreeNodeComponent[transform.childCount];
            for(int i = 0; i < transform.childCount; i++)
            {
                var treeNode = transform.GetChild(i).gameObject.AddComponent<TreeNodeComponent>();
                m_children[i] = treeNode;
            }
        }
    }
}
