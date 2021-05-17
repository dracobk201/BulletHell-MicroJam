using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private IntReference playerMaxLife = default(IntReference);
    [SerializeField] private IntReference playerActualLife = default(IntReference);
    [SerializeField] private IntReference healthDropAmount = default(IntReference);
    [SerializeField] private GameEvent playerImpacted = default(GameEvent);
    [SerializeField] private GameEvent playerDead = default(GameEvent);
    [SerializeField] private AudioClipGameEvent sfxToPlay = default(AudioClipGameEvent);
    [SerializeField] private AudioClip playerDeadAudio = default(AudioClip);
    [SerializeField] private AudioClip playerHealAudio = default(AudioClip);

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
            {
                isGameStarted.Value = false;
                sfxToPlay.Raise(playerDeadAudio);
                playerDead.Raise();
            }
        }
        else if (targetTag.Equals(Global.HealthDropTag))
        {
            playerActualLife.Value = Mathf.Clamp(playerActualLife.Value + healthDropAmount.Value, 0, playerMaxLife.Value);
            sfxToPlay.Raise(playerHealAudio);
        }
    }
}
