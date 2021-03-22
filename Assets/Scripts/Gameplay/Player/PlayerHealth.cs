using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private IntReference playerMaxLife = default(IntReference);
    [SerializeField] private IntReference playerActualLife = default(IntReference);
    [SerializeField] private GameEvent playerImpacted = default(GameEvent);
    [SerializeField] private GameEvent playerDead = default(GameEvent);

    private void Start()
    {
        playerActualLife.Value = playerMaxLife.Value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;
        if (targetTag.Equals(Global.EnemyBulletTag) || targetTag.Equals(Global.EnemyTag))
        {
            playerActualLife.Value--;
            playerImpacted.Raise();
            if (playerActualLife.Value <= 0)
                playerDead.Raise();
        }
    }
}
