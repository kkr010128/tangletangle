// ItemEffectManager.cs
using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    public static ItemEffectManager Instance;

    public GameObject rockPrefab;
    public GameObject dynamitePrefab;
    public GameObject boosterPrefab;
    public GameObject poisonPrefab;

    void Awake() => Instance = this;

    public void ActivateItem(ItemType itemType, string ownerTag)
    {
        Vector3 pos;

        switch (itemType)
        {
            case ItemType.Rock:
                pos = FindDropPoint(ownerTag, true);
                Instantiate(rockPrefab, pos, Quaternion.identity);
                break;
            case ItemType.Dynamite:
                pos = FindDropPoint(ownerTag, false);
                Instantiate(dynamitePrefab, pos, Quaternion.identity);
                break;
            case ItemType.Fertilizer:
                pos = FindDropPoint(ownerTag, false);
                Instantiate(boosterPrefab, pos, Quaternion.identity);
                break;
            case ItemType.Pesticide:
                pos = FindDropPoint(ownerTag, true);
                Instantiate(poisonPrefab, pos, Quaternion.identity);
                break;
        }
    }

    Vector3 FindDropPoint(string playerTag, bool toOpponent)
    {
        string target = toOpponent
            ? (playerTag == "PlayerA" ? "PlayerB" : "PlayerA")
            : playerTag;

        GameObject player = GameObject.FindGameObjectWithTag(target);
        DropperBase dropper = player?.GetComponent<DropperBase>();
        return dropper != null ? dropper.dropPoint.position : player.transform.position;
    }
}