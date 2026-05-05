using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    [SerializeField] private ItemType itemType;
    [SerializeField] private int amount = 1;

    public void Interact(PlayerInteraction player)
    {
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogError("PlayerInventory not found on Player!");
            return;
        }

        inventory.AddItem(itemType, amount);

        Debug.Log($"Picked up {itemType} x{amount}");

        Destroy(gameObject);
    }

    public string GetInteractionText()
    {
        return $"Pick up {itemType}";
    }
}