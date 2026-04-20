using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Escenas")]
    [SerializeField] private string gameplaySceneName = "Level";

    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        AudioManager.Instance?.PlayMenuMusic();

        if (mainPanel != null) mainPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        AudioManager.Instance?.PlayButtonClick();

        if (string.IsNullOrEmpty(gameplaySceneName))
        {
            Debug.LogError("[MainMenuController] No se asignó el nombre de la escena de juego.");
            return;
        }

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OnSettingsButtonClicked()
    {
        AudioManager.Instance?.PlayButtonClick();

        if (mainPanel != null) mainPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void OnBackFromSettingsClicked()
    {
        AudioManager.Instance?.PlayButtonClick();

        if (mainPanel != null) mainPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        AudioManager.Instance?.PlayButtonClick();

        Debug.Log("[MainMenuController] Saliendo del juego...");
        // Esto cierra el juego fuera del editor.
        Application.Quit();

#if UNITY_EDITOR
        // Solo en el Unity Editor, detiene el modo Play.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}