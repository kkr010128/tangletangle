// GameManager.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public FruitData[] fruitPrefabs;
    private List<int> unlockedLevels = new List<int>();
    private Dictionary<FruitData, float> spawnWeights = new Dictionary<FruitData, float>();

    public int scoreA = 0;
    public int scoreB = 0;

    public AudioClip mergeSound;
    public AudioClip itemGetSound;
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;

        unlockedLevels.Add(1);
        unlockedLevels.Add(2);
        unlockedLevels.Add(3);

        RecalculateWeights();

        audioSource = GetComponent<AudioSource>();
    }

    public void OnFruitMerged(FruitType newType, string ownerTag)
    {
        int level = (int)newType;
        if (!unlockedLevels.Contains(level))
        {
            unlockedLevels.Add(level);
            RecalculateWeights();
        }

        int gain = level * 5;
        if (ownerTag == "PlayerA") scoreA += gain;
        else if (ownerTag == "PlayerB") scoreB += gain;

        if (mergeSound != null && audioSource != null)
            audioSource.PlayOneShot(mergeSound);

        var inventories = FindObjectsByType<PlayerItemInventory>(FindObjectsSortMode.None);
        PlayerItemInventory target = inventories.FirstOrDefault(inv => inv.playerTag == ownerTag);

        if (target != null)
        {
            switch (newType)
            {
                case FruitType.Peach:
                    target.AddItem(ItemType.Rock);
                    PlayItemGetSound();
                    break;
                case FruitType.Pear:
                    target.AddItem(ItemType.Dynamite);
                    PlayItemGetSound();
                    break;
                case FruitType.Persimmon:
                    target.AddItem(ItemType.Fertilizer);
                    PlayItemGetSound();
                    break;
                case FruitType.Apple:
                    target.AddItem(ItemType.Pesticide);
                    PlayItemGetSound();
                    break;
            }
        }
    }

    private void PlayItemGetSound()
    {
        if (itemGetSound != null && audioSource != null)
            audioSource.PlayOneShot(itemGetSound, 3f);
    }

    public void CheckItemDrop(FruitType type, string playerTag)
    {
        PlayerItemInventory target = GameObject.FindGameObjectsWithTag(playerTag)
            .Select(obj => obj.GetComponent<PlayerItemInventory>())
            .FirstOrDefault(inv => inv != null);

        if (target == null) return;

        switch (type)
        {
            case FruitType.Peach: target.AddItem(ItemType.Rock); break;
            case FruitType.Pear: target.AddItem(ItemType.Dynamite); break;
            case FruitType.Persimmon: target.AddItem(ItemType.Fertilizer); break;
            case FruitType.Apple: target.AddItem(ItemType.Pesticide); break;
        }
    }

    public FruitData GetRandomFruit()
    {
        float rand = Random.Range(0f, 100f);
        float accum = 0f;

        foreach (var pair in spawnWeights)
        {
            accum += pair.Value;
            if (rand <= accum)
                return pair.Key;
        }

        return spawnWeights.Keys.First();
    }

    private void RecalculateWeights()
    {
        spawnWeights.Clear();

        int maxLevel = unlockedLevels.Max();

        if (maxLevel <= 3)
        {
            spawnWeights[FindFruitDataByLevel(1)] = 50f;
            spawnWeights[FindFruitDataByLevel(2)] = 35f;
            spawnWeights[FindFruitDataByLevel(3)] = 15f;
            return;
        }

        float center = 1f + (maxLevel - 1f) / 2f;
        float sigma = 2.0f;

        foreach (int level in unlockedLevels)
        {
            FruitData data = FindFruitDataByLevel(level);
            if (data == null) continue;

            float raw = Mathf.Exp(-0.5f * Mathf.Pow((level - center) / sigma, 2));
            spawnWeights[data] = raw;
        }

        float sum = spawnWeights.Values.Sum();
        foreach (var key in spawnWeights.Keys.ToList())
            spawnWeights[key] = (spawnWeights[key] / sum) * 100f;
    }

    private FruitData FindFruitDataByLevel(int level)
    {
        foreach (var data in fruitPrefabs)
        {
            if ((int)data.type == level)
                return data;
        }
        return null;
    }

    public void GameOver(string playerName)
    {
        Debug.Log("Game Over! " + playerName + " 패배");
        Time.timeScale = 0f;
    }
}