// PlayerItemInventory.cs
using UnityEngine;
using System.Collections.Generic;

public class PlayerItemInventory : MonoBehaviour
{
    public string playerTag;
    public List<SpriteRenderer> itemSlots;
    public Sprite rockIcon, dynamiteIcon, boosterIcon, poisonIcon;

    private Queue<ItemType> items = new Queue<ItemType>();

    public void AddItem(ItemType itemType)
    {
        if (items.Count >= 4) return;
        items.Enqueue(itemType);
        UpdateUI();
    }

    public void UseNextItem()
    {
        if (items.Count == 0) return;
        ItemType item = items.Dequeue();
        ItemEffectManager.Instance.ActivateItem(item, playerTag);
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].sprite = null;
            itemSlots[i].enabled = false;
        }

        int idx = 0;
        foreach (var item in items)
        {
            if (idx >= itemSlots.Count) break;
            itemSlots[idx].enabled = true;
            itemSlots[idx].sprite = GetIcon(item);
            idx++;
        }
    }

    private Sprite GetIcon(ItemType item)
    {
        return item switch
        {
            ItemType.Rock => rockIcon,
            ItemType.Dynamite => dynamiteIcon,
            ItemType.Fertilizer => boosterIcon,
            ItemType.Pesticide => poisonIcon,
            _ => null
        };
    }
}