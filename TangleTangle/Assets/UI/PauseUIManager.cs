using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIManager : MonoBehaviour
{
    public static PauseUIManager Instance;

    public GameObject pausePanel;

    public UnityEngine.UI.Image pauseButtonImage;
    public Sprite pauseSprite;
    public Sprite resumeSprite;
    public AudioSource bgmSource;

    void Awake()
    {
        Instance = this;
        pausePanel.SetActive(false);
    }

    public void TogglePause()
    {
        bool isPausing = Time.timeScale == 1f;

        Time.timeScale = isPausing ? 0f : 1f;
        pausePanel.SetActive(isPausing);

        if (pauseButtonImage != null)
        {
            pauseButtonImage.sprite = isPausing ? resumeSprite : pauseSprite;
        }

        if (bgmSource != null)
        {
            if (isPausing) bgmSource.Pause();
            else bgmSource.UnPause();
        }
    }

    public void OnResumeButton()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        // "MainMenu" 이름으로 이미 string mainScene 저장되어 있어야 함
        SceneManager.LoadScene("MainMenu");
    }
}