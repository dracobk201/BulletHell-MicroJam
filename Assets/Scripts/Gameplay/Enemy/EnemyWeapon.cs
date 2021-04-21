using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private GameObjectCollection enemyBullets = default(GameObjectCollection);
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private FloatReference timeToShoot = default(FloatReference);
    [SerializeField] private AudioClipGameEvent sfxToPlay = default(AudioClipGameEvent);
    [SerializeField] private Transform[] bulletInitialTransforms = default(Transform[]);
    [SerializeField] private AudioClip enemyShootingAudio = default(AudioClip);
    private bool _shooting;
    private float _currentTimeToShoot;

    private void Awake()
    {
        _shooting = false;
        _currentTimeToShoot = timeToShoot.Value + Random.Range(0.6f, 1.5f);
    }

    private void Update()
    {
        RotativeShoot();
        _currentTimeToShoot -= Time.deltaTime;
        if (_currentTimeToShoot <= 0)
        {
            ShootBullet();
            _currentTimeToShoot = Mathf.Clamp(timeToShoot.Value - (enemiesKilled.Value * 0.005f), 0, Mathf.Infinity);
        }
    }

    public void ShootBullet()
    {
        if (_shooting || !isGameStarted.Value) return;
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
                    sfxToPlay.Raise(enemyShootingAudio);
                    _shooting = false;
                    break;
                }
            }
        }
    }

    private void RotativeShoot()
    {
        transform.Rotate(0, 0, 10);
    }
}
