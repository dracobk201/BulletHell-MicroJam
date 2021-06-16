using System.Collections;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Bullet : MonoBehaviour
{
    [SerializeField] private FloatReference bulletVelocity = default(FloatReference);
    [SerializeField] private FloatReference bulletTimeOfLife = default(FloatReference);
    [SerializeField] private GameEvent enemyImpacted = default(GameEvent);
    [SerializeField] private GameEvent playerImpacted = default(GameEvent);
    private float _velocityMultiplier;
    
    private void OnEnable()
    {
        StartCoroutine(AutoDestruction());
        _velocityMultiplier = 1;
        if (gameObject.tag.Equals(Global.EnemyBulletTag))
            _velocityMultiplier = 0.6f;
    }

    private void Update()
    {
        transform.position += transform.up * bulletVelocity.Value * _velocityMultiplier * Time.deltaTime;
    }

    private void Destroy()
    {
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(bulletTimeOfLife.Value);
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;
        if (targetTag.Equals(Global.EnemyTag) && gameObject.tag.Equals(Global.PlayerBulletTag))
        {
            Destroy();
            enemyImpacted.Raise();
        }
        else if (targetTag.Equals(Global.PlayerTag) && gameObject.tag.Equals(Global.EnemyBulletTag))
        {
            Destroy();
            playerImpacted.Raise();
        }
    }
}
