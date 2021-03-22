using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObjectCollection enemyShips = default(GameObjectCollection);
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private FloatReference timeToSpawn = default(FloatReference);
    [SerializeField] private GameEvent shipSpawned = default(GameEvent);
    private float _actualTimeToSpawn;

    private void Start()
    {
        _actualTimeToSpawn = timeToSpawn.Value;
    }

    private void Update()
    {
        _actualTimeToSpawn -= Time.deltaTime;
        if (_actualTimeToSpawn <= 0)
        {
            Spawn();
            _actualTimeToSpawn = timeToSpawn.Value - (enemiesKilled.Value * 0.01f);
        }
    }

    private void Spawn()
    {
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
}