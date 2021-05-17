using UnityEngine;
using ScriptableObjectArchitecture;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private BoolCollection directionLock = default(BoolCollection);
    [SerializeField] private Vector2Reference movementAxis = default(Vector2Reference);
    [SerializeField] private Vector3Reference rotationAxis = default(Vector3Reference);
    [SerializeField] private FloatReference playerMoveSpeed = default(FloatReference);
    [SerializeField] private FloatReference playerDashValue = default(FloatReference);
    [SerializeField] private FloatReference playerDashTime = default(FloatReference);
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private Transform transformToRotate = default(Transform);
    private Camera _gameCamera;
    private bool _playerIsDead;
    private bool _playerIsDashing;
    private float _dashValue;

    private void Start()
    {
        _gameCamera = Camera.main;
        _playerIsDead = false;
        _dashValue = 1;
        directionLock[0] = false;
        directionLock[1] = false;
        directionLock[2] = false;
        directionLock[3] = false;
    }

    public void Move()
    {
        if (_playerIsDead || !isGameStarted.Value) return;
        var dualDirectionMultiplier = (movementAxis.Value.x != 0 && movementAxis.Value.y != 0) ? 0.5f : 1;
        var movementWithRestrictions = CheckRestrictions(movementAxis.Value);
        float newXPosition = movementWithRestrictions.x * playerMoveSpeed.Value * _dashValue * dualDirectionMultiplier * Time.deltaTime;
        float newYPosition = movementWithRestrictions.y * playerMoveSpeed.Value * _dashValue * dualDirectionMultiplier * Time.deltaTime;
        transform.Translate(newXPosition, newYPosition, 0);
    }

    private Vector2 CheckRestrictions(Vector2 value)
    {
        if (directionLock[0] && value.x < 0)
            value.x = 0;
        if (directionLock[1] && value.x > 0)
            value.x = 0;
        if (directionLock[2] && value.y > 0)
            value.y = 0;
        if (directionLock[3] && value.y < 0)
            value.y = 0;
        return value;
    }

    public void RotateWithMouse()
    {
        if (_playerIsDead || !isGameStarted.Value) return;
        Vector3 mouseWorld = _gameCamera.ScreenToWorldPoint(rotationAxis.Value);
        transformToRotate.up = new Vector2(mouseWorld.x, mouseWorld.y) - new Vector2(transform.position.x, transform.position.y);
    }

    public void RotateWithJoystick()
    {
        if (_playerIsDead || !isGameStarted.Value) return;
        transformToRotate.up = new Vector3(rotationAxis.Value.x, -rotationAxis.Value.y, rotationAxis.Value.z);
    }

    public void TriggerDash()
    {
        if (_playerIsDashing) return;
        _dashValue = playerDashValue.Value;
        _playerIsDashing = true;
        StartCoroutine(DashDiscount());
    }

    private IEnumerator DashDiscount()
    {
        float time = playerDashTime.Value;
        while (time > 0)
        {
            _dashValue = Mathf.Lerp(1, playerDashValue.Value, time/playerDashTime.Value);
            yield return null;
            time -= Time.deltaTime;
        }
        _playerIsDashing = false;
    }

    public void PlayerIsDead()
    {
        _playerIsDead = true;
    }
}
