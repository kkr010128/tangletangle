using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreTextA;
    public TextMeshProUGUI scoreTextB;

    void Update()
    {
        scoreTextA.text = "SCORE: " + GameManager.Instance.scoreA;
        scoreTextB.text = "SCORE: " + GameManager.Instance.scoreB;
    }
}