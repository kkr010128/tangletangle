using UnityEngine;

public class DynamiteItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Fruit") || collision.CompareTag("Rock"))
    {
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
}
}