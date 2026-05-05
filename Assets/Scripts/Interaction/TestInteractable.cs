using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    [Header("Test Settings")]
    [SerializeField] private string interactionText = "Tekan E untuk berinteraksi.";
    [SerializeField] private string interactionResultMessage = "Objek berhasil diinteraksi.";

    public void Interact(PlayerInteraction player)
    {
        Debug.Log(interactionResultMessage);
    }

    public string GetInteractionText()
    {
        return interactionText;
    }
}