using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class SetNameCanvasActions : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField = default(TMP_InputField);
    [SerializeField] private StringReference playfabUsername = default(StringReference);
    [SerializeField] private BoolReference isGameStarted = default(BoolReference);
    [SerializeField] private GameEvent startGame = default(GameEvent);

    public void Awake()
    {
        isGameStarted.Value = false;
        if (!string.IsNullOrEmpty(playfabUsername.Value.Trim()))
            usernameInputField.text = playfabUsername.Value;
    }

    public void StartGame()
    {
        if (usernameInputField.text.Trim().Equals(string.Empty)) return;
        playfabUsername.Value = usernameInputField.text.Trim();
        isGameStarted.Value = true;
        startGame.Raise();
    }
}
