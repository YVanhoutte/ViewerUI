using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DesktopInputEventsHandler : InputEventsHandler
{
    protected override bool ButtonAction1()
    {
        return (Input.GetButtonUp("Fire1"));
    }

    protected override bool ButtonAction2()
    {
        return (Input.GetButtonUp("Fire2"));
    }

    protected override bool ButtonAction3()
    {
        return (Input.GetButtonUp("Fire3"));
    }

    protected override bool ButtonAction4()
    {
        return (Input.GetButtonUp("Fire4"));
    }

    protected override float CameraSideways()
    {
        return (Input.GetAxis("CameraHorizontal"));
    }

    protected override float CameraUpwards()
    {
        return (Input.GetAxis("CameraVertical"));
    }

    protected override float MoveForwards()
    {
        return (Input.GetAxis("Vertical"));
    }

    protected override float MoveSideways()
    {
        return (Input.GetAxis("Horizontal"));
    }

    protected override float MoveUpwards()
    {
        return (Input.GetAxis("Height"));
    }
}
