using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    private BoxCollider2D _enemyCollider;

    private void Awake()
    {
        TryGetComponent(out BoxCollider2D targetCollider);
        _enemyCollider = targetCollider;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        LeanTween.scale(gameObject, Vector3.one, 2f)
            .setOnStart(() => _enemyCollider.enabled = false)
            .setEase(LeanTweenType.easeInExpo)
            .setOnComplete(() => _enemyCollider.enabled = true);
    }

    private void Destroy()
    {
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.zero, 0.5f)
            .setOnStart(() => _enemyCollider.enabled = false)
            .setEase(LeanTweenType.easeInExpo)
            .setOnComplete(() => gameObject.SetActive(false));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;
        if (targetTag.Equals(Global.PlayerBulletTag) || targetTag.Equals(Global.PlayerTag))
        {
            enemiesKilled.Value++;
            Destroy();
        }
    }
}
