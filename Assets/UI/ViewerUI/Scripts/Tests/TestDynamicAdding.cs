using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestDynamicAdding : MonoBehaviour
{
    [SerializeField] private TreeNodeUI prefab;
    private TreeUIController testTarget;
    private int counter = 0;
    private string m_name;

    private void Awake()
    {
        testTarget = GetComponent<TreeUIController>();
    }

    private void Update()
    {
        m_name = "Blah - " + counter;
        if (counter % 2 == 0)
        {
            m_name = "Bleh - " + counter;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null)
            {
                TreeNodeUI current = selected.GetComponentInParent<TreeNodeUI>();
                if (current != null)
                {
                    TreeNodeUI element = Instantiate(prefab);
                    //element.SetName(m_name);
                    testTarget.AddElement(element, current);
                    counter++;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null)
            {
                TreeNodeUI current = selected.GetComponentInParent<TreeNodeUI>();
                if (current != null)
                {
                    //testTarget.RemoveElement(current);
                }
            }
        }
    }
}
