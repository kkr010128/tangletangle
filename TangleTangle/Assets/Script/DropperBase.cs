// DropperBase.cs
using UnityEngine;

public abstract class DropperBase : MonoBehaviour
{
    public Transform dropPoint;
    public SpriteRenderer nextFruitPreview;

    protected FruitData nextFruit;

    private float dropCooldown = 1.0f;
    private float lastDropTime = -999f;

    protected virtual void Start()
    {
        UpdateNextFruit();
    }

    protected void DropFruit()
    {
        if (Time.time - lastDropTime < dropCooldown)
            return;

        if (nextFruit == null || nextFruit.prefab == null)
            return;

        GameObject fruit = Instantiate(nextFruit.prefab, dropPoint.position, Quaternion.identity);

        FruitCollision fc = fruit.GetComponent<FruitCollision>();
        if (fc != null)
            fc.ownerTag = this.tag;

        

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