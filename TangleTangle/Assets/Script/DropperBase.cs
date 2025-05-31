using UnityEngine;

public abstract class DropperBase : MonoBehaviour
{
    public Transform dropPoint;
    public SpriteRenderer nextFruitPreview;

    protected FruitData nextFruit;

    private float dropCooldown = 1.0f;   // 쿨타임 (초)
    private float lastDropTime = -999f; // 마지막 드롭 시점

    protected virtual void Start()
    {
        UpdateNextFruit();
    }

    protected void DropFruit()
    {
        if (Time.time - lastDropTime < dropCooldown)
            return; // 쿨타임 미도래 시 무시

        if (nextFruit == null || nextFruit.prefab == null)
            return;

        // 과일 생성
        GameObject fruit = Instantiate(nextFruit.prefab, dropPoint.position, Quaternion.identity);

        // ownerTag 설정 (GameOver 판정용)
        FruitCollision fc = fruit.GetComponent<FruitCollision>();
        if (fc != null)
            fc.ownerTag = this.tag; // 이 Dropper의 태그를 과일에 전달 ("PlayerA" 또는 "PlayerB")

        lastDropTime = Time.time;
        UpdateNextFruit();
    }

    protected void UpdateNextFruit()
    {
        nextFruit = GameManager.Instance.GetRandomFruit();

        if (nextFruitPreview != null && nextFruit.prefab != null)
        {
            SpriteRenderer r = nextFruit.prefab.GetComponent<SpriteRenderer>();
            if (r != null)
                nextFruitPreview.sprite = r.sprite;
        }
    }
}