using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCanvasActions : MonoBehaviour
{
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private TextMeshProUGUI enemyKilledLabel = default(TextMeshProUGUI);

    public void ShowGameOver()
    {
        enemyKilledLabel.text = $"{enemiesKilled.Value} Enemy Ships Killed";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
