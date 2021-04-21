using UnityEngine;
using ScriptableObjectArchitecture;
using System;

public class ControlController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private Vector2Reference movementAxis = default(Vector2Reference);
    [SerializeField] private GameEvent anyMovementAxisEvent = default(GameEvent);
    [SerializeField] private GameEvent nonMovementAxisEvent = default(GameEvent);
    private bool _isVerticalMovementAxisInUse;
    private bool _isHorizontalMovementAxisInUse;

    [Header("Rotation Variables")]
    [SerializeField] private Vector3Reference rotationAxis = default(Vector3Reference);
    [SerializeField] private GameEvent mouseRotationAxisEvent = default(GameEvent);
    [SerializeField] private GameEvent joystickRotationAxisEvent = default(GameEvent);
    [SerializeField] private GameEvent nonRotationAxisEvent = default(GameEvent);

    [Header("Buttons Variables")]
    [SerializeField] private GameEvent fireButtonEvent = default(GameEvent);
    [SerializeField] private GameEvent dashButtonEvent = default(GameEvent);

    private void Update()
    {
        CheckingVerticalMovementAxis();
        CheckingHorizontalMovementAxis();
        CheckingNonMovementAxis();
        CheckingMouseAxis();
        CheckingRightStickAxis();
        CheckingFireButton();
        CheckingDashButton();
    }

    #region Vertical Movement Actions

    private void CheckingVerticalMovementAxis()
    {
        float verticalAxis = Input.GetAxis(Global.VerticalAxis);
        if ((Math.Abs(verticalAxis) > Global.Tolerance) && !_isVerticalMovementAxisInUse)
            PerformVerticalMovementActions(verticalAxis);
        else
            NoVerticalMovementActions();
    }

    private void PerformVerticalMovementActions(float value)
    {
        _isVerticalMovementAxisInUse = true;
        movementAxis.Value = new Vector2(movementAxis.Value.x, value);
        anyMovementAxisEvent.Raise();
    }

    private void NoVerticalMovementActions()
    {
        _isVerticalMovementAxisInUse = false;
        movementAxis.Value = new Vector2(movementAxis.Value.x, 0);
    }

    #endregion

    #region Horizontal Movement Actions

    private void CheckingHorizontalMovementAxis()
    {
        float horizontalAxis = Input.GetAxis(Global.HorizontalAxis);
        if ((Math.Abs(horizontalAxis) > Global.Tolerance) && !_isHorizontalMovementAxisInUse)
            PerformHorizontalMovementActions(horizontalAxis);
        else
            NoHorizontalMovementActions();
    }

    private void PerformHorizontalMovementActions(float value)
    {
        _isHorizontalMovementAxisInUse = true;
        movementAxis.Value = new Vector2(value, movementAxis.Value.y);
        anyMovementAxisEvent.Raise();
    }

    private void NoHorizontalMovementActions()
    {
        _isHorizontalMovementAxisInUse = false;
        movementAxis.Value = new Vector2(0, movementAxis.Value.y);
    }

    #endregion

    #region Rotation Keyboard Actions

    private void CheckingMouseAxis()
    {
        var mouseVerticalValue = Input.GetAxis(Global.MouseVerticalAxis);
        var mouseHorizontalValue = Input.GetAxis(Global.MouseVerticalAxis);

        if (Math.Abs(mouseVerticalValue) > Global.Tolerance || Math.Abs(mouseHorizontalValue) > Global.Tolerance)
        {
            rotationAxis.Value = Input.mousePosition;
            mouseRotationAxisEvent.Raise();
        }
        else
            NoRotationActions();
    }

    private void CheckingRightStickAxis()
    {
        var rightStickVerticalValue = Input.GetAxis(Global.RightStickVerticalAxis);
        var rightStickHorizontalValue = Input.GetAxis(Global.RightStickHorizontalAxis);

        if (Math.Abs(rightStickVerticalValue) > Global.Tolerance || Math.Abs(rightStickHorizontalValue) > Global.Tolerance) 
        {
            rotationAxis.Value = new Vector2(rightStickHorizontalValue, rightStickVerticalValue);
            joystickRotationAxisEvent.Raise();
        }
        else
            NoRotationActions();
    }

    private void NoRotationActions()
    {
        rotationAxis.Value = new Vector2(0, 0);
        nonRotationAxisEvent.Raise();
    }

    #endregion

    private void CheckingNonMovementAxis()
    {
        if (_isVerticalMovementAxisInUse && _isHorizontalMovementAxisInUse)
            nonMovementAxisEvent.Raise();
    }

    private void CheckingFireButton()
    {
        if (Input.GetAxisRaw(Global.FireAxis) > Global.Tolerance)
            fireButtonEvent.Raise();
    }

    private void CheckingDashButton()
    {
        if (Input.GetAxisRaw(Global.DashAxis) > Global.Tolerance)
            dashButtonEvent.Raise();
    }
}
