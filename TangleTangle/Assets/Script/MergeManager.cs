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
        GameManager.Instance.OnFruitMerged(newType);
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