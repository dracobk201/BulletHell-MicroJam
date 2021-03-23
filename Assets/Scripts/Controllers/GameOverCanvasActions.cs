using UnityEngine;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCanvasActions : MonoBehaviour
{
    [SerializeField] private IntReference enemiesKilled = default(IntReference);
    [SerializeField] private StringCollection currentLeaderboard = default(StringCollection);
    [SerializeField] private TextMeshProUGUI enemyKilledLabel = default(TextMeshProUGUI);
    [SerializeField] private TextMeshProUGUI leaderboardText = default(TextMeshProUGUI);

    public void ShowGameOver()
    {
        enemyKilledLabel.text = $"{enemiesKilled.Value} Enemy Ships Killed";
        leaderboardText.text = "Loading Leaderboard...";
    }

    public void ShowLeaderboard()
    {
        leaderboardText.text = string.Empty;
        foreach (var currentPosition in currentLeaderboard)
        {
            leaderboardText.text += currentPosition;
            leaderboardText.text += "\n";
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
