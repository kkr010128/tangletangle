// ResultUIManager.cs
using UnityEngine;
using TMPro;

public class ResultUIManager : MonoBehaviour
{
    public static ResultUIManager Instance;

    public GameObject resultPanel;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI scoreTextA;
    public TextMeshProUGUI scoreTextB;

    void Awake()
    {
        Instance = this;
        resultPanel.SetActive(false);
    }

    public void ShowResult(string winner, int scoreA, int scoreB)
    {
        resultPanel.SetActive(true);
        winnerText.text = winner + " Wins!";
        scoreTextA.text = "Player 1 Score: " + scoreA;
        scoreTextB.text = "Player 2 Score: " + scoreB;
    }
}