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
    public AudioClip bgmGameOver;

    public AudioSource inGameBGM; // 기존 BGM AudioSource 연결
    private AudioSource audioSource;
    private AudioSource bgmSource;

    void Awake()
    {
        Instance = this;

        unlockedLevels = new List<int> { 1, 2, 3, 4, 5 };

        RecalculateWeights();

        audioSource = GetComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
    }

    public void OnFruitMerged(FruitType newType, string ownerTag)
    {
        int level = (int)newType;

        if (level <= 5 && !unlockedLevels.Contains(level))
        {
            unlockedLevels.Add(level);
            RecalculateWeights();
        }

        int gain = level * 5;
        if (ownerTag == "PlayerA") scoreA += gain;
        else if (ownerTag == "PlayerB") scoreB += gain;

        var inventories = FindObjectsByType<PlayerItemInventory>(FindObjectsSortMode.None);
        PlayerItemInventory target = inventories.FirstOrDefault(inv => inv.playerTag == ownerTag);

        if (target != null)
        {
            switch (newType)
            {
                case FruitType.Peach: target.AddItem(ItemType.Rock); PlayItemGetSound(); break;
                case FruitType.Pear: target.AddItem(ItemType.Dynamite); PlayItemGetSound(); break;
                case FruitType.Persimmon: target.AddItem(ItemType.Fertilizer); PlayItemGetSound(); break;
                case FruitType.Apple: target.AddItem(ItemType.Pesticide); PlayItemGetSound(); break;
            }
        }

        if (mergeSound != null && audioSource != null)
            audioSource.PlayOneShot(mergeSound);
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

        spawnWeights[FindFruitDataByLevel(5)] = 35f; // 체리
        spawnWeights[FindFruitDataByLevel(4)] = 30f; // 딸기
        spawnWeights[FindFruitDataByLevel(3)] = 22.5f; // 포도
        spawnWeights[FindFruitDataByLevel(2)] = 7.5f; // 오렌지
        spawnWeights[FindFruitDataByLevel(1)] = 5f; // 감귤
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

        if (inGameBGM != null)
            inGameBGM.Stop();

        if (bgmGameOver != null && bgmSource != null)
        {
            bgmSource.clip = bgmGameOver;
            bgmSource.Play();
        }
    }
}