using UnityEngine;

public class PesticideItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit fruit = collision.GetComponent<Fruit>();
        if (fruit != null && fruit.type > FruitType.Cherry)
        {
            FruitType prevType = (FruitType)((int)fruit.type - 1);
            MergeManager.Instance.SpawnFruit(prevType, fruit.transform.position, fruit.ownerTag);

            Destroy(fruit.gameObject);
            Destroy(this.gameObject);

            GameManager.Instance.OnFruitMerged(prevType, fruit.ownerTag); // ✅ 여기에서 prevType으로 수정
        }
    }
}