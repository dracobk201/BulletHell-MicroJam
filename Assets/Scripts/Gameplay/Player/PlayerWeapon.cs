using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private FloatReference RPM = default(FloatReference);
    [SerializeField] private GameObjectCollection playerBullets = default(GameObjectCollection);
    [SerializeField] private AudioClipGameEvent sfxToPlay = default(AudioClipGameEvent);
    [SerializeField] private AudioClip playerShootingAudio = default(AudioClip);
    private bool _playerIsDead;
    private float _nextShot = 0;

    private void Start()
    {
        _playerIsDead = false;
    }

    public void ShootBullet()
    {
        //if (!_canShoot || _playerIsDead || !isGameStarted.Value) return;
        if (_playerIsDead || Time.time < _nextShot) return;
        _nextShot = Time.time + 60 / RPM.Value;
        var initialPosition = transform.position;
        var initialRotation = transform.rotation;

        for (int i = 0; i < playerBullets.Count; i++)
        {
            if (!playerBullets[i].activeInHierarchy)
            {
                playerBullets[i].transform.localPosition = initialPosition;
                playerBullets[i].transform.localRotation = initialRotation;
                playerBullets[i].SetActive(true);
                sfxToPlay.Raise(playerShootingAudio);
                break;
            }
        }
    }

    public void PlayerIsDead()
    {
        _playerIsDead = true;
    }
}
