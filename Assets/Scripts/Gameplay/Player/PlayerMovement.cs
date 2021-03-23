using UnityEngine;
using ScriptableObjectArchitecture;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2Reference movementAxis = default(Vector2Reference);
    [SerializeField] private Vector3Reference cameraAxis = default(Vector3Reference);
    [SerializeField] private FloatReference playerMoveSpeed = default(FloatReference);
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private Transform transformToRotate = default(Transform);
    private Camera _gameCamera;
    private bool _playerIsDead;

    private void Start()
    {
        _gameCamera = Camera.main;
        _playerIsDead = false;
    }

    public void Move()
    {
        if (_playerIsDead || !isGameStarted.Value) return;
        var dualDirectionMultiplier = (movementAxis.Value.x != 0 && movementAxis.Value.y != 0) ? 0.6f : 1;
        float newstraffe = movementAxis.Value.x * playerMoveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        float newtranslation = movementAxis.Value.y * playerMoveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        transform.Translate(newstraffe, newtranslation, 0);
    }

    public void Rotate()
    {
        if (_playerIsDead || !isGameStarted.Value) return;
        Vector3 mouseWorld = _gameCamera.ScreenToWorldPoint(cameraAxis.Value);
        transformToRotate.up = new Vector2(mouseWorld.x, mouseWorld.y) - new Vector2(transform.position.x, transform.position.y);
    }

    public void PlayerIsDead()
    {
        _playerIsDead = true;
    }
}
