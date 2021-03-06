using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObjectCollection enemyShips = default(GameObjectCollection);
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private FloatReference timeToSpawn = default(FloatReference);
    [SerializeField] private GameEvent shipSpawned = default(GameEvent);
    [SerializeField] private AudioClipGameEvent bgmToPlay = default(AudioClipGameEvent);
    [SerializeField] private AudioClip bgmAudio = default(AudioClip);
    private bool _playerIsDead;
    private float _actualTimeToSpawn;

    private void Start()
    {
        enemiesKilled.Value = 0;
        _actualTimeToSpawn = timeToSpawn.Value;
        _playerIsDead = false;
        bgmToPlay.Raise(bgmAudio);
    }

    private void Update()
    {
        _actualTimeToSpawn -= Time.deltaTime;
        if (_actualTimeToSpawn <= 0)
        {
            Spawn();
            _actualTimeToSpawn = Mathf.Clamp(timeToSpawn.Value - (enemiesKilled.Value * 0.02f), 0, Mathf.Infinity);
        }
    }

    private void Spawn()
    {
        if (_playerIsDead || !isGameStarted.Value) return;
        var initialPosition = new Vector2(Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f));

        for (int i = 0; i < enemyShips.Count; i++)
        {
            if (!enemyShips[i].activeInHierarchy)
            {
                enemyShips[i].transform.localPosition = initialPosition;
                enemyShips[i].SetActive(true);
                shipSpawned.Raise();
                break;
            }
        }
    }

    public void PlayerIsDead()
    {
        _playerIsDead = true;
    }
}
