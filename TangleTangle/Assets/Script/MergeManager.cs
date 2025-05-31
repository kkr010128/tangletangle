using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance;
    public FruitData[] fruitPrefabs;

    void Awake() => Instance = this;

    public void MergeFruits(Fruit a, Fruit b)
    {
        Vector3 spawnPos = (a.transform.position + b.transform.position) / 2;
        FruitType newType = (FruitType)((int)a.type + 1);

        if ((int)newType > (int)FruitType.Watermelon)
        {
            Destroy(a.gameObject);
            Destroy(b.gameObject);
            return;
        }

        GameObject newFruitPrefab = GetFruitPrefab(newType);
        if (newFruitPrefab == null)
        {
            Debug.LogWarning("MergeManager: No prefab found for type " + newType);
            return;
        }

        Instantiate(newFruitPrefab, spawnPos, Quaternion.identity);
        Destroy(a.gameObject);
        Destroy(b.gameObject);

        // ✅ ownerTag 전달
        string ownerTag = a.GetComponent<FruitCollision>()?.ownerTag ?? "Unknown";
        GameManager.Instance.OnFruitMerged(newType, ownerTag);
    }

    public void SpawnFruit(FruitType type, Vector3 position, string ownerTag)
    {
        GameObject prefab = GetFruitPrefab(type);
        if (prefab == null)
        {
            Debug.LogWarning("MergeManager: No prefab for type " + type);
            return;
        }

        GameObject go = Instantiate(prefab, position, Quaternion.identity);
        var collision = go.GetComponent<FruitCollision>();
        if (collision != null)
            collision.ownerTag = ownerTag;
    }

    GameObject GetFruitPrefab(FruitType type)
    {
        foreach (var data in fruitPrefabs)
        {
            if (data.type == type && data.prefab != null)
                return data.prefab;
        }
        Debug.LogWarning("MergeManager: Missing or null prefab for type " + type);
        return null;
    }
}