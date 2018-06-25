using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivityX = 15F;
    [SerializeField] private float sensitivityY = 15F;
    [SerializeField] private float minimumX = -360F;
    [SerializeField] private float maximumX = 360F;
    [SerializeField] private float minimumY = -60F;
    [SerializeField] private float maximumY = 60F;
    [SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;

    private float rotationX = 0F;
    private float rotationY = 0F;
    private Quaternion originalRotation;
    private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

    private void Start()
    {
        if (XRDevice.isPresent) //Kill this if user has a VR headset. This thing messes with rotations otherwise!
            enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        // Make the rigid body not change rotation
        if (rb != null)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (ToggleableWindow.IsWindowUp)
            return;

        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
