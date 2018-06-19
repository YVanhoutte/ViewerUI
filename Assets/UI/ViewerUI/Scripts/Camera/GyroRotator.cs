using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotator : MonoBehaviour
{
	void Update ()
    {
        if(Input.gyro.enabled)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(Input.gyro.rotationRateUnbiased.x * -1, Input.gyro.rotationRateUnbiased.y * -1, Input.gyro.rotationRateUnbiased.z));
	}
}
