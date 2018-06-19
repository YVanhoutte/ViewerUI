using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListElement : MonoBehaviour
{
    public string ListName { get { return m_listName; } }
    [SerializeField] private string m_listName;
}
