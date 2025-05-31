using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public FruitData[] fruitPrefabs; // 모든 과일 등록 (순서대로)
    private List<int> unlockedLevels = new List<int>();
    private Dictionary<FruitData, float> spawnWeights = new Dictionary<FruitData, float>();

    void Awake()
    {
        Instance = this;

        // 초기 해금 과일: 체리(1), 딸기(2), 포도(3)
        unlockedLevels.Add(1);
        unlockedLevels.Add(2);
        unlockedLevels.Add(3);

        RecalculateWeights();
    }

    public void OnFruitMerged(FruitType newType)
    {
        int level = (int)newType;
        if (!unlockedLevels.Contains(level))
        {
            unlockedLevels.Add(level);
            RecalculateWeights();
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

        // fallback
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

        // 정규분포로 확장
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

    // ✅ 추가된 게임 오버 처리 함수
    public void GameOver(string playerName)
    {
        Debug.Log("Game Over! " + playerName + " 패배");

        Time.timeScale = 0f; // 일시정지 (필요 시 UI, 리셋 등 확장 가능)
    }
}