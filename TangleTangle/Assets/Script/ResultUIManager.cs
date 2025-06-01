// ResultUIManager.cs
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ResultUIManager : MonoBehaviour
{
    public static ResultUIManager Instance;

    public GameObject resultPanel;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI scoreTextA;
    public TextMeshProUGUI scoreTextB;
    public string mainScene;

    void Awake()
    {
        Instance = this;
        resultPanel.SetActive(false);
    }

    public void ShowResult(string winner, int scoreA, int scoreB)
    {
        resultPanel.SetActive(true);
        winnerText.text = winner + "\nWINS!!";
        scoreTextA.text = "1P: " + scoreA;
        scoreTextB.text = "2P: " + scoreB;
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f; // 혹시 멈춘 상태에서 돌아가는 것을 방지
        SceneManager.LoadScene(mainScene); // "MainMenu"는 메인 씬 이름
    }
}