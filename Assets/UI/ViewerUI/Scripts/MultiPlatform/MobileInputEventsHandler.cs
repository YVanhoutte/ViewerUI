using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputEventsHandler : InputEventsHandler
{
    [SerializeField] private Button m_button1, m_button2, m_button3, m_button4;
    [SerializeField] private Joystick m_moveJoystick, m_cameraJoystick;
    private bool m_button1State, m_button2State, m_button3State, m_button4State;

    private void Start()
    {
        Input.gyro.enabled = true;

        if (m_button1 != null)
            m_button1.onClick.AddListener(() => m_button1State = true);
        if (m_button2 != null)
            m_button2.onClick.AddListener(() => m_button2State = true);
        if (m_button3 != null)
            m_button3.onClick.AddListener(() => m_button3State = true);
        if (m_button4 != null)
            m_button4.onClick.AddListener(() => m_button4State = true);
    }

    protected override bool ButtonAction1()
    {
        if(m_button1State == true)
        {
            m_button1State = false;
            return true;
        }
        return false;
    }

    protected override bool ButtonAction2()
    {
        if (m_button2State == true)
        {
            m_button2State = false;
            return true;
        }
        return false;
    }

    protected override bool ButtonAction3()
    {
        if (m_button3State == true)
        {
            m_button3State = false;
            return true;
        }
        return false;
    }

    protected override bool ButtonAction4()
    {
        if (m_button4State == true)
        {
            m_button4State = false;
            return true;
        }
        return false;
    }

    protected override float CameraSideways()
    {
        return m_cameraJoystick.Horizontal;
    }

    protected override float CameraUpwards()
    {
        //if (Input.gyro.enabled)
        //    return Input.gyro.rotationRate.x * -1;
        return 0;
        return m_cameraJoystick.Vertical * -1;
    }

    protected override float MoveForwards()
    {
        return m_moveJoystick.Vertical;
    }

    protected override float MoveSideways()
    {
        return m_moveJoystick.Horizontal;
    }

    protected override float MoveUpwards()
    {
        return m_cameraJoystick.Vertical;
    }
}
