using UnityEngine;
using ScriptableObjectArchitecture;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2Reference movementAxis = default(Vector2Reference);
    [SerializeField] private Vector3Reference cameraAxis = default(Vector3Reference);
    [SerializeField] private FloatReference playerMoveSpeed = default(FloatReference);
    [SerializeField] private FloatReference damping = default(FloatReference);
    [SerializeField] private Transform transformToRotate = default(Transform);
    private Camera gameCamera;

    private void Awake()
    {
        gameCamera = Camera.main;
    }

    public void Move()
    {
        var dualDirectionMultiplier = (movementAxis.Value.x != 0 && movementAxis.Value.y != 0) ? 0.4f : 1;
        float newstraffe = movementAxis.Value.x * playerMoveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        float newtranslation = movementAxis.Value.y * playerMoveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        transform.Translate(newstraffe, newtranslation, 0);
    }

    public void Rotate()
    {
        Vector3 mouseWorld = gameCamera.ScreenToWorldPoint(cameraAxis.Value);
        transformToRotate.up = new Vector2(mouseWorld.x, mouseWorld.y) - new Vector2(transform.position.x, transform.position.y);
    }
}
