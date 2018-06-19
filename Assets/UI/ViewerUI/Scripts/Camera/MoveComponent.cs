using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour {

    [SerializeField] private float m_speed;
    private Vector3 m_inputVector = Vector3.zero, m_independantinputVector = Vector3.zero;

    public void MoveX(float axis)
    {
        m_inputVector.x = axis;
    }

    public void MoveY(float axis)
    {
        m_inputVector.z = axis;
    }

    public void MoveZ(float axis)
    {
        m_inputVector.y = axis;
    }

    public void MoveZIndependant(float axis)
    {
        m_independantinputVector.y = axis;
    }

    private void OnEnable()
    {
        if (InputEventsHandler.Instance == null)
            return;
        InputEventsHandler.Instance.OnMoveSideways += MoveX;
        InputEventsHandler.Instance.OnMoveForward  += MoveY;
        InputEventsHandler.Instance.OnMoveUpwards  += MoveZIndependant;
    }

    private void OnDisable()
    {
        if (InputEventsHandler.Instance == null)
            return;
        InputEventsHandler.Instance.OnMoveSideways -= MoveX;
        InputEventsHandler.Instance.OnMoveForward  -= MoveY;
        InputEventsHandler.Instance.OnMoveUpwards  -= MoveZIndependant;
    }

    private void Start()
    {
        OnEnable();
    }

    private void Update()
    {
        if (ToggleableWindow.CurrentWindowsCount > 0)
            return;
        transform.Translate(Camera.main.transform.localRotation * m_inputVector * m_speed * Time.deltaTime);
        transform.Translate(m_independantinputVector * m_speed * Time.deltaTime);
    }
}
