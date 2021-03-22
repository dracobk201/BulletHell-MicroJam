using UnityEngine;
using ScriptableObjectArchitecture;

public class PlayerBulletMagazine : MonoBehaviour
{
    [SerializeField] private GameObjectCollection playerBullets = default(GameObjectCollection);
    [SerializeField] private IntReference playerBulletsPool = default(IntReference);
    [SerializeField] private GameObject playerBulletPrefab = default(GameObject);

    private void Start()
    {
        playerBullets.Clear();
        InstantiateBullets();
    }

    private void InstantiateBullets()
    {
        for (int i = 0; i < playerBulletsPool.Value; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab) as GameObject;
            bullet.GetComponent<Transform>().SetParent(transform);
            playerBullets.Add(bullet);
            bullet.SetActive(false);
        }
    }
}
