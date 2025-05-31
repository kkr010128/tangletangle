using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public ItemType type;
    public string ownerTag;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == ItemType.Rock)
        {
            // 아무 효과 없음 (그냥 공간 차지)
        }
        else if (type == ItemType.Dynamite)
        {
            if (collision.CompareTag("Fruit"))
                Destroy(collision.gameObject);
        }
        else if (type == ItemType.Fertilizer)
        {
            Fruit f = collision.GetComponent<Fruit>();
            if (f != null && f.type < FruitType.Watermelon)
            {
                MergeManager.Instance.SpawnFruit((FruitType)((int)f.type + 1), f.transform.position, f.ownerTag);
                Destroy(collision.gameObject);
                GameManager.Instance.OnFruitMerged((FruitType)((int)f.type + 1), f.ownerTag);
            }
        }
        else if (type == ItemType.Pesticide)
        {
            Fruit f = collision.GetComponent<Fruit>();
            if (f != null && f.type > FruitType.Cherry)
            {
                MergeManager.Instance.SpawnFruit((FruitType)((int)f.type - 1), f.transform.position, f.ownerTag);
                Destroy(collision.gameObject);
            }
        }

        Destroy(gameObject);
    }
}