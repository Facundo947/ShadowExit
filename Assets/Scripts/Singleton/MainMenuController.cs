using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Escenas")]
    [SerializeField] private string gameplaySceneName = "Level1";

    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        // Esto reproduce la música del menú y el Juego
        AudioManager.Instance?.PlayMenuMusic();
        AudioManager.Instance?.PlayGameplayMusic();

        // Esto muestra el panel principal al abrir
        if (mainPanel != null) mainPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        if (string.IsNullOrEmpty(gameplaySceneName))
        {
            Debug.LogError("[MainMenuController] No se asignó el nombre de la escena de juego.");
            return;
        }

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OnSettingsButtonClicked()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void OnBackFromSettingsClicked()
    {
        if (mainPanel != null) mainPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("[MainMenuController] Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}