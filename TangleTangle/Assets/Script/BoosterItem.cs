using UnityEngine;

public class BoosterItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit fruit = collision.GetComponent<Fruit>();
        if (fruit != null && fruit.type < FruitType.Watermelon)
        {
            FruitType nextType = (FruitType)((int)fruit.type + 1);
            MergeManager.Instance.SpawnFruit(nextType, fruit.transform.position, fruit.ownerTag);

            Destroy(fruit.gameObject);
            Destroy(this.gameObject);

            GameManager.Instance.OnFruitMerged(nextType, fruit.ownerTag);
        }
    }
}