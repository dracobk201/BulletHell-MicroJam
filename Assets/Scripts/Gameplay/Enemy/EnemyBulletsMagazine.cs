using UnityEngine;
using ScriptableObjectArchitecture;

public class EnemyBulletsMagazine : MonoBehaviour
{
    [SerializeField] private GameObjectCollection enemyBullets = default(GameObjectCollection);
    [SerializeField] private IntReference enemyBulletsPool = default(IntReference);
    [SerializeField] private GameObject enemyBulletPrefab = default(GameObject);

    private void Start()
    {
        enemyBullets.Clear();
        InstantiateBullets();
    }

    private void InstantiateBullets()
    {
        for (int i = 0; i < enemyBulletsPool.Value; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab) as GameObject;
            bullet.GetComponent<Transform>().SetParent(transform);
            enemyBullets.Add(bullet);
            bullet.SetActive(false);
        }
    }
}
