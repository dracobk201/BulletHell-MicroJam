using UnityEngine;
using ScriptableObjectArchitecture;


public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2Reference movementAxis = default(Vector2Reference);
    [SerializeField] private FloatReference playerMoveSpeed = default(FloatReference);

    public void Move()
    {
        var dualDirectionMultiplier = (movementAxis.Value.x != 0 && movementAxis.Value.y != 0) ? 0.4f : 1;
        float newstraffe = movementAxis.Value.x * playerMoveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        float newtranslation = movementAxis.Value.y * playerMoveSpeed.Value * dualDirectionMultiplier * Time.deltaTime;
        transform.Translate(newstraffe, newtranslation, 0);
    }
}
