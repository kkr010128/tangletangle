using UnityEngine;

public class FruitCollision : MonoBehaviour
{
    public string ownerTag = ""; // "PlayerA" 또는 "PlayerB"

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((ownerTag == "PlayerA" && collision.collider.CompareTag("GameOverA")) ||
            (ownerTag == "PlayerB" && collision.collider.CompareTag("GameOverB")))
        {
            ScoreUI scoreUI = FindObjectOfType<ScoreUI>();
            int scoreA = int.Parse(scoreUI.scoreTextA.text.Replace("SCORE: ", ""));
            int scoreB = int.Parse(scoreUI.scoreTextB.text.Replace("SCORE: ", ""));

            if (scoreA > scoreB)
            {
                GameManager.Instance.GameOver("2P");
            }
            else if (scoreB > scoreA)
            {
                GameManager.Instance.GameOver("1P");
            }
            else
            {
                GameManager.Instance.GameOver("Draw");
            }
        }
    }
}