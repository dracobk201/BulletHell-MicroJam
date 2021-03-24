using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.UI;
using TMPro;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private IntReference playerActualHealth = default(IntReference);
    [SerializeField] private IntReference playerMaxHealth = default(IntReference);
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private Image healthGauge = default(Image);
    [SerializeField] private TextMeshProUGUI enemiesKilledLabel = default(TextMeshProUGUI);

    private void Start()
    {
        enemiesKilledLabel.text = "0";
    }

    public void UpdateHealthGauge()
    {
        healthGauge.fillAmount = (float) playerActualHealth.Value / (float) playerMaxHealth.Value;
    }

    public void UpdateEnemiesKilled()
    {
        enemiesKilledLabel.text = enemiesKilled.Value.ToString();
    }
}
