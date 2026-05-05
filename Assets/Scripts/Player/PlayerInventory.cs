using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Data (Debug Only)")]
    [SerializeField] private List<ItemType> debugItemTypes = new List<ItemType>();
    [SerializeField] private List<int> debugItemAmounts = new List<int>();

    private Dictionary<ItemType, int> inventory = new Dictionary<ItemType, int>();

    private void Awake()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        inventory.Clear();

        foreach (ItemType type in System.Enum.GetValues(typeof(ItemType)))
        {
            inventory.Add(type, 0);
        }

        SyncDebugLists();
    }

    /* =========================
       PUBLIC API
       ========================= */

    public void AddItem(ItemType itemType, int amount = 1)
    {
        if (amount <= 0) return;

        inventory[itemType] += amount;
        SyncDebugLists();

        Debug.Log($"[Inventory] +{amount} {itemType} (Total: {inventory[itemType]})");
    }

    public bool HasItem(ItemType itemType, int amount = 1)
    {
        return inventory[itemType] >= amount;
    }

    public bool UseItem(ItemType itemType, int amount = 1)
    {
        if (!HasItem(itemType, amount))
        {
            Debug.Log($"[Inventory] Item tidak cukup: {itemType}");
            return false;
        }

        inventory[itemType] -= amount;
        SyncDebugLists();

        Debug.Log($"[Inventory] -{amount} {itemType} (Sisa: {inventory[itemType]})");
        return true;
    }

    public int GetItemAmount(ItemType itemType)
    {
        return inventory[itemType];
    }

    /* =========================
       DEBUG SUPPORT
       ========================= */

    private void SyncDebugLists()
    {
        debugItemTypes.Clear();
        debugItemAmounts.Clear();

        foreach (var item in inventory)
        {
            debugItemTypes.Add(item.Key);
            debugItemAmounts.Add(item.Value);
        }
    }
}