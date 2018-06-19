using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorComponent : MonoBehaviour {

    [SerializeField] private float m_speed;
    private Vector3 m_inputVector = Vector3.zero;
    private float[] m_axises = new float[3] { 0.0f, 0.0f, 0.0f};

    public void RotateYaw(float axis)
    {
        m_inputVector.y = axis;
    }

    public void RotatePitch(float axis)
    {
        m_inputVector.x = axis;
    }

    public void RotateRoll(float axis)
    {
        m_inputVector.z = axis;
    }

    private void OnEnable()
    {
        if (InputEventsHandler.Instance == null)
            return;
        InputEventsHandler.Instance.OnCameraX += RotateYaw;
        InputEventsHandler.Instance.OnCameraY += RotatePitch;
    }

    private void OnDisable()
    {
        if (InputEventsHandler.Instance == null)
            return;
        InputEventsHandler.Instance.OnCameraX -= RotateYaw;
        InputEventsHandler.Instance.OnCameraY -= RotatePitch;
    }

    private void Start()
    {
        OnEnable();
    }

    private void Update ()
    {
        if (ToggleableWindow.CurrentWindowsCount > 0)
            return;
        transform.Rotate(m_inputVector * m_speed * Time.deltaTime, Space.Self);
    }
}
