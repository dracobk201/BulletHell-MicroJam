using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private AudioClipGameEvent sfxToPlay = default(AudioClipGameEvent);
    [SerializeField] private AudioClip enemyDeathAudio = default(AudioClip);

    private BoxCollider2D _enemyCollider;

    private void Awake()
    {
        TryGetComponent(out BoxCollider2D targetCollider);
        _enemyCollider = targetCollider;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        LeanTween.scale(gameObject, Vector3.one, 1f)
            .setOnStart(() => _enemyCollider.enabled = false)
            .setEase(LeanTweenType.easeInExpo)
            .setOnComplete(() => _enemyCollider.enabled = true);
    }

    private void Destroy()
    {
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.zero, 0.5f)
            .setOnStart(() => _enemyCollider.enabled = false)
            .setEase(LeanTweenType.easeInElastic)
            .setOnComplete(() => gameObject.SetActive(false));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;
        if (targetTag.Equals(Global.PlayerBulletTag) || targetTag.Equals(Global.PlayerTag))
        {
            enemiesKilled.Value++;
            sfxToPlay.Raise(enemyDeathAudio);
            Destroy();
        }
    }
}
