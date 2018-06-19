using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InputEventsHandler : MonoBehaviour {

    static public InputEventsHandler Instance { get; private set; }

    public UnityEvent OnControl1Pressed, OnControl2Pressed, OnControl3Pressed, OnControl4Pressed;
    public Action<float> OnMoveSideways, OnMoveForward, OnMoveUpwards, OnCameraX, OnCameraY;

    protected abstract float MoveSideways();
    protected abstract float MoveForwards();
    protected abstract float MoveUpwards();
    protected abstract float CameraSideways();
    protected abstract float CameraUpwards();

    protected abstract bool ButtonAction1();
    protected abstract bool ButtonAction2();
    protected abstract bool ButtonAction3();
    protected abstract bool ButtonAction4();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Update ()
    {
        ExecuteAction(OnControl1Pressed, ButtonAction1());
        ExecuteAction(OnControl2Pressed, ButtonAction2());
        ExecuteAction(OnControl3Pressed, ButtonAction3());
        ExecuteAction(OnControl4Pressed, ButtonAction4());

        ExecuteAction(OnMoveSideways, MoveSideways());
        ExecuteAction(OnMoveForward,  MoveForwards());
        ExecuteAction(OnMoveUpwards,  MoveUpwards());
        ExecuteAction(OnCameraX,      CameraSideways());
        ExecuteAction(OnCameraY,      CameraUpwards());
        //Debug.Log("Axises: " + new Vector3(MoveSideways(), MoveForwards(), MoveUpwards()));
    }

    private void ExecuteAction(Action<float> action, float axis)
    {
        if (action != null)
            action.Invoke(axis);
    }

    private void ExecuteAction(UnityEvent action, bool condition)
    {
        if (condition && action != null)
            action.Invoke();
    }
}
