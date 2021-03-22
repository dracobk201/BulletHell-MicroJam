using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObjectCollection enemyBullets = default(GameObjectCollection);
    [SerializeField] private FloatReference timeToShoot = default(FloatReference);
    [SerializeField] private GameEvent enemyShot = default(GameEvent);
    [SerializeField] private Transform[] bulletInitialTransforms = default(Transform[]);
    private bool _shooting;
    private float _currentTimeToShoot;

    private void Start()
    {
        _currentTimeToShoot = timeToShoot.Value + Random.Range(0,2f);
    }

    private void Update()
    {
        _currentTimeToShoot -= Time.deltaTime;
        if (_currentTimeToShoot <= 0)
        {
            ShootBullet();
            _currentTimeToShoot = timeToShoot.Value;
        }
    }

    public void ShootBullet()
    {
        if (_shooting) return;
        _shooting = true;

        for (int i = 0; i < bulletInitialTransforms.Length; i++)
        {
            var initialPosition = bulletInitialTransforms[i].position;
            var initialRotation = bulletInitialTransforms[i].rotation;

            for (int j = 0; j < enemyBullets.Count; j++)
            {
                if (!enemyBullets[j].activeInHierarchy)
                {
                    enemyBullets[j].transform.localPosition = initialPosition;
                    enemyBullets[j].transform.localRotation = initialRotation;
                    enemyBullets[j].SetActive(true);
                    enemyShot.Raise();
                    _shooting = false;
                    break;
                }
            }
        }
        
    }
}
