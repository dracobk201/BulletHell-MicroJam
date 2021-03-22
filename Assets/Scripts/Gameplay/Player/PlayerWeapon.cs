using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObjectCollection playerBullets = default(GameObjectCollection);
    [SerializeField] private GameEvent playerShot = default(GameEvent);
    [SerializeField] private Transform bulletInitialTransform = default(Transform);
    private bool _playerIsDead;
    private bool _shooting;

    private void Start()
    {
        _playerIsDead = false;
    }

    public void ShootBullet()
    {
        if (_shooting || _playerIsDead) return;
        _shooting = true;
        var initialPosition = bulletInitialTransform.position;
        var initialRotation = bulletInitialTransform.rotation;

        for (int i = 0; i < playerBullets.Count; i++)
        {
            if (!playerBullets[i].activeInHierarchy)
            {
                playerBullets[i].transform.localPosition = initialPosition;
                playerBullets[i].transform.localRotation = initialRotation;
                playerBullets[i].SetActive(true);
                playerShot.Raise();
                _shooting = false;
                break;
            }
        }
    }

    public void PlayerIsDead()
    {
        _playerIsDead = true;
    }
}
