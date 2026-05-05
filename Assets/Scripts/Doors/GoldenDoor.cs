using UnityEngine;

public class GoldenDoor : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteraction player)
    {
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogError("PlayerInventory not found!");
            return;
        }

        if (!inventory.HasItem(ItemType.GoldenKey, 1))
        {
            Debug.Log("Need Golden Key");
            return;
        }

        inventory.UseItem(ItemType.GoldenKey, 1);
        OpenDoor();
    }

    public string GetInteractionText()
    {
        return "Open Golden Door";
    }

    private void OpenDoor()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Debug.Log("Level Complete (Demo)");
    }
}