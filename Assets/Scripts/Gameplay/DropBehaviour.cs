using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    private CircleCollider2D _dropCollider;

    private void Awake()
    {
        TryGetComponent(out CircleCollider2D targetCollider);
        _dropCollider = targetCollider;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;
        if (targetTag.Equals(Global.PlayerTag))
            Destroy();
    }

    private void Destroy()
    {
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.zero, 0.5f)
            .setOnStart(() => _dropCollider.enabled = false)
            .setEase(LeanTweenType.easeInElastic)
            .setOnComplete(() => gameObject.SetActive(false));
    }
}
