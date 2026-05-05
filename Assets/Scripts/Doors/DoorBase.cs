using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class DoorBase : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    [SerializeField] protected bool isOpened = false;

    protected Collider2D doorCollider;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void Interact(PlayerInteraction player);
    public abstract string GetInteractionText();

    protected virtual void OpenDoor()
    {
        isOpened = true;

        if (doorCollider != null)
            doorCollider.enabled = false;

        if (spriteRenderer != null)
            spriteRenderer.color = Color.gray;

        Debug.Log($"{gameObject.name} opened.");
    }
}