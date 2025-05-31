using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;   // 생성 대상 프리팹 11개
    public Transform spawnArea;           // 기준 영역 (Transform)
    public float spawnDelay = 1f;         // 반복 시간 간격 (초)

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnRandomObject()
    {
        if (spawnArea == null || prefabsToSpawn.Length == 0) return;

        Vector3 center = spawnArea.position;
        Vector3 size = spawnArea.localScale;

        Vector3 randomPos = new Vector3(
            Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            Random.Range(center.y - size.y / 2, center.y + size.y / 2),
            0f
        );

        GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        GameObject spawned = Instantiate(prefab, randomPos, Quaternion.identity, this.transform);  // 부모 설정
        spawned.transform.localScale = prefab.transform.localScale;
        Destroy(spawned, 4f);
    }
}