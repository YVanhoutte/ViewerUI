using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraController : MonoBehaviour
{
    private int m_maxInputs;
    private float m_rotationVelocity, m_movementVelocity, m_heightMovementVelocity;
    private Vector3 m_rotationInputVector = Vector3.zero, m_movementInputVector = Vector3.zero, m_heightMovementInputVector = Vector3.zero, m_dragVector;
    private List<Vector3> m_movementInputs = new List<Vector3>(), m_rotationInputs = new List<Vector3>(), m_heightmovementInputs = new List<Vector3>();
    [SerializeField] private float m_movementAccelleration, m_maxMovementSpeed, m_movementDrag, m_rotationAccelleration, m_maxRotationSpeed, m_rotationDrag;

    private Transform m_cameraTransform;
    private Vector3 m_cameraOffset;

    public void RotateYaw(float axis)
    {
        m_rotationInputVector.y = axis;
    }

    public void MoveX(float axis)
    {
        m_movementInputVector.x = axis;
    }

    public void MoveY(float axis)
    {
        m_movementInputVector.z = axis;
    }

    public void MoveZ(float axis)
    {
        m_movementInputVector.y = axis;
    }

    private void MoveZIndependant(float axis)
    {
        m_heightMovementInputVector.y = axis;
    }

    private void Awake()
    {
        m_cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        if (InputEventsHandler.Instance == null)
            return;
        InputEventsHandler.Instance.OnCameraX += RotateYaw;
        InputEventsHandler.Instance.OnMoveSideways += MoveX;
        InputEventsHandler.Instance.OnMoveForward += MoveY;
        InputEventsHandler.Instance.OnMoveUpwards += MoveZIndependant;
    }

    private void OnDisable()
    {
        if (InputEventsHandler.Instance == null)
            return;
        InputEventsHandler.Instance.OnCameraX -= RotateYaw;
        InputEventsHandler.Instance.OnMoveSideways -= MoveX;
        InputEventsHandler.Instance.OnMoveForward -= MoveY;
        InputEventsHandler.Instance.OnMoveUpwards -= MoveZIndependant;
    }

    private void Start()
    {
        OnEnable();
    }

    private void FixedUpdate()
    {
        m_maxInputs = (int)(1f / Time.fixedDeltaTime);
        m_movementInputs.Add(m_movementInputVector);
        while (m_movementInputs.Count > m_maxInputs)
        {
            m_movementInputs.RemoveAt(0);
        }
        m_rotationInputs.Add(m_rotationInputVector);
        while (m_rotationInputs.Count > m_maxInputs)
        {
            m_rotationInputs.RemoveAt(0);
        }
        m_heightmovementInputs.Add(m_heightMovementInputVector);
        while(m_heightmovementInputs.Count > m_maxInputs)
        {
            m_heightmovementInputs.RemoveAt(0);
        }
    }

    private void Update()
    {
        //Debug.Log("FPS: " + m_maxInputs);
        //m_maxInputs = Application.targetFrameRate;
        m_cameraOffset = m_cameraTransform.localPosition;

        if (ToggleableWindow.CurrentWindowsCount > 0)
        {
            m_movementInputs.Clear();
            m_movementInputs.Add(Vector3.zero);
            m_rotationInputs.Clear();
            m_rotationInputs.Add(Vector3.zero);
            m_heightmovementInputs.Clear();
            m_heightmovementInputs.Add(Vector3.zero);
            return;
        }

        Vector3 averageMovement = MathUtils.Average(m_movementInputs);
        Vector3 averageRotation = MathUtils.Average(m_rotationInputs);
        Vector3 averageHeightMovement = MathUtils.Average(m_heightmovementInputs);

        //Stokes' Drag, source: https://stackoverflow.com/questions/667034/simple-physics-based-movement?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        m_movementVelocity += (m_movementAccelleration * averageMovement.magnitude) - (m_movementDrag * m_movementVelocity);
        Vector3 movement = averageMovement * Mathf.Clamp(m_movementVelocity, 0, m_maxMovementSpeed) - m_dragVector;
        transform.Translate(Camera.main.transform.localRotation * movement * Time.deltaTime);

        m_heightMovementVelocity += (m_movementAccelleration * averageHeightMovement.magnitude) - (m_movementDrag * m_heightMovementVelocity);
        Vector3 heightMovement = averageHeightMovement * Mathf.Clamp(m_heightMovementVelocity, 0, m_maxMovementSpeed) - m_dragVector;
        transform.Translate(heightMovement * Time.deltaTime);

        m_rotationVelocity += (m_rotationAccelleration *averageRotation.magnitude) - (m_rotationDrag * m_rotationVelocity);
        m_rotationVelocity = Mathf.Clamp(m_rotationVelocity, 0, m_maxRotationSpeed);
        transform.RotateAround(m_cameraTransform.position, transform.up, m_rotationVelocity * averageRotation.y * Time.deltaTime); //Rotate around the camera's center instead of own center. Puke town avoided!
        //Debug.Log("Movement Speed: " + m_movementVelocity + " Rotation Speed: " + m_rotationVelocity);
    }
}
