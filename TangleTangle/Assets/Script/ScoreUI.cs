using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreTextA;
    public TextMeshProUGUI scoreTextB;

    void Update()
    {
        scoreTextA.text = "Player1 Score: " + GameManager.Instance.scoreA;
        scoreTextB.text = "Player2 Score: " + GameManager.Instance.scoreB;
    }
}