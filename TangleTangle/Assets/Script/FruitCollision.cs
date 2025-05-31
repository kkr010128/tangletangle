using UnityEngine;

public class FruitCollision : MonoBehaviour
{
    public string ownerTag = ""; // "PlayerA" 또는 "PlayerB"

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ownerTag == "PlayerA" && collision.collider.CompareTag("GameOverA"))
        {
            GameManager.Instance.GameOver("PlayerA");
        }

        if (ownerTag == "PlayerB" && collision.collider.CompareTag("GameOverB"))
        {
            GameManager.Instance.GameOver("PlayerB");
        }
    }
}