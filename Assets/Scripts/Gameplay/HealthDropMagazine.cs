using UnityEngine;
using ScriptableObjectArchitecture;

public class HealthDropMagazine : MonoBehaviour
{
    [SerializeField] private GameObjectCollection healthDrops = default(GameObjectCollection);
    [SerializeField] private IntReference healthDropsPool = default(IntReference);
    [SerializeField] private GameObject healthDropPrefab = default(GameObject);

    private void Start()
    {
        healthDrops.Clear();
        InstantiateHealthDrops();
    }

    private void InstantiateHealthDrops()
    {
        for (int i = 0; i < healthDropsPool.Value; i++)
        {
            GameObject newHealthDrop = Instantiate(healthDropPrefab) as GameObject;
            newHealthDrop.GetComponent<Transform>().SetParent(transform);
            healthDrops.Add(newHealthDrop);
            newHealthDrop.SetActive(false);
        }
    }
}
