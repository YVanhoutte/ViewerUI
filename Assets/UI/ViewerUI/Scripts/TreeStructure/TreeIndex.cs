using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeIndex : MonoBehaviour {

    public int Index { get; private set; }

    private void Awake()
    {
        int index = transform.GetSiblingIndex();
        TreeIndex parent = transform.GetComponentInParent<TreeIndex>();
        if (parent != null)
            index += parent.Index;
        Index = index;
    }
}
