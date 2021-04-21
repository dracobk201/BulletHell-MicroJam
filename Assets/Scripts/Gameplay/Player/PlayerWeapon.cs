using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private FloatReference gunCooldown = default(FloatReference);
    [SerializeField] private GameObjectCollection playerBullets = default(GameObjectCollection);
    [SerializeField] private AudioClipGameEvent sfxToPlay = default(AudioClipGameEvent);
    [SerializeField] private Transform bulletInitialTransform = default(Transform);
    [SerializeField] private AudioClip playerShootingAudio = default(AudioClip);
    private bool _playerIsDead;
    private bool _canShoot;
    private float _cooldown;

    private void Start()
    {
        _playerIsDead = false;
        _canShoot = true;
        _cooldown = gunCooldown.Value;
    }

    private void Update()
    {
        _cooldown--;
        if (_cooldown <= 0)
        {
            _canShoot = true;
            _cooldown = gunCooldown.Value;
        }
    }

    public void ShootBullet()
    {
        if (!_canShoot || _playerIsDead || !isGameStarted.Value) return;

        var initialPosition = bulletInitialTransform.position;
        var initialRotation = bulletInitialTransform.rotation;

        for (int i = 0; i < playerBullets.Count; i++)
        {
            if (!playerBullets[i].activeInHierarchy)
            {
                playerBullets[i].transform.localPosition = initialPosition;
                playerBullets[i].transform.localRotation = initialRotation;
                playerBullets[i].SetActive(true);
                sfxToPlay.Raise(playerShootingAudio);
                _canShoot = false;
                break;
            }
        }
    }

    public void PlayerIsDead()
    {
        _playerIsDead = true;
    }
}
