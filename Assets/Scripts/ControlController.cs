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

    [Header("Camera Variables")]
    [SerializeField] private Vector3Reference cameraAxis = default(Vector3Reference);
    [SerializeField] private GameEvent anyCameraAxisEvent = default(GameEvent);
    [SerializeField] private GameEvent nonCameraAxisEvent = default(GameEvent);

    [Header("Buttons Variables")]
    [SerializeField] private IntReference framesToWait = default(IntReference);
    [SerializeField] private GameEvent fireButtonEvent = default(GameEvent);
    private bool _isFireAxisInUse;
    private int _pressedFramesInFireButton;

    private void Awake()
    {
        _pressedFramesInFireButton = framesToWait.Value;
    }

    private void Update()
    {
        CheckingVerticalMovementAxis();
        CheckingHorizontalMovementAxis();
        CheckingNonMovementAxis();
        CheckingMouseAxis();
        CheckingFireButton();
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

    #region Camera Actions

    private void CheckingMouseAxis()
    {
        var mouseVerticalValue = Input.GetAxis(Global.MouseVerticalAxis);
        var horizontalCameraValue = Input.GetAxis(Global.MouseVerticalAxis);

        if (Math.Abs(mouseVerticalValue) > Global.Tolerance || Math.Abs(horizontalCameraValue) > Global.Tolerance)
            PerformCameraActions();
        else
            NoCameraActions();
    }

    private void PerformCameraActions()
    {
        cameraAxis.Value = Input.mousePosition;
        anyCameraAxisEvent.Raise();
    }

    private void NoCameraActions()
    {
        cameraAxis.Value = new Vector2(0, 0);
        nonCameraAxisEvent.Raise();
    }

    #endregion

    private void CheckingNonMovementAxis()
    {
        if (_isVerticalMovementAxisInUse && _isHorizontalMovementAxisInUse)
            nonMovementAxisEvent.Raise();
    }

    private void CheckingFireButton()
    {
        if (_isFireAxisInUse)
        {
            _pressedFramesInFireButton--;
            if (_pressedFramesInFireButton < 0)
            {
                _isFireAxisInUse = false;
                _pressedFramesInFireButton = framesToWait.Value;
            }
        }
        else
        {
            if (Input.GetAxisRaw(Global.FireAxis) > Global.Tolerance)
            {
                fireButtonEvent.Raise();
                _isFireAxisInUse = true;
            }
        }
        
    }
}
