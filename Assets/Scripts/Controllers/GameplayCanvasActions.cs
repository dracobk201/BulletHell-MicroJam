using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.UI;

public class GameplayCanvasActions : MonoBehaviour
{
    [SerializeField] private IntReference playerActualHealth = default(IntReference);
    [SerializeField] private IntReference playerMaxHealth = default(IntReference);
    [SerializeField] private Image healthGauge = default(Image);

    public void UpdateHealthGauge()
    {
        healthGauge.fillAmount = (float) playerActualHealth.Value / (float) playerMaxHealth.Value;
    }
}
