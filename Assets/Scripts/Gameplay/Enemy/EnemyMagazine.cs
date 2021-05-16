using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyMagazine : MonoBehaviour
{
    [SerializeField] private GameObjectCollection enemyShips = default(GameObjectCollection);
    [SerializeField] private IntReference enemyShipsPool = default(IntReference);
    [SerializeField] private GameObject enemyShipPrefab = default(GameObject);

    private void Start()
    {
        enemyShips.Clear();
        InstantiateShips();
    }

    private void InstantiateShips()
    {
        for (int i = 0; i < enemyShipsPool.Value; i++)
        {
            GameObject bullet = Instantiate(enemyShipPrefab) as GameObject;
            bullet.GetComponent<Transform>().SetParent(transform);
            enemyShips.Add(bullet);
            bullet.SetActive(false);
        }
    }
}
