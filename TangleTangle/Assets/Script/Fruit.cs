using UnityEngine;

public class Fruit : MonoBehaviour
{
    public string ownerTag;
    
    public FruitType type;
    [HideInInspector] public bool hasMerged = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged) return;

        Fruit other = collision.gameObject.GetComponent<Fruit>();
        if (other != null && other.type == this.type && !other.hasMerged)
        {
            hasMerged = true;
            other.hasMerged = true;
            MergeManager.Instance.MergeFruits(this, other);
        }
    }
}