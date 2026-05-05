using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 1f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    private IInteractable currentInteractable;
    private Collider2D currentInteractableCollider;

    private void Update()
    {
        DetectNearestInteractable();
        HandleInteractionInput();
    }

    private void DetectNearestInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            interactionRange,
            interactableLayer
        );

        if (hits.Length == 0)
        {
            ClearCurrentInteractable();
            return;
        }

        Collider2D nearestCollider = null;
        IInteractable nearestInteractable = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();

            if (interactable == null)
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, hit.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCollider = hit;
                nearestInteractable = interactable;
            }
        }

        if (nearestInteractable == null)
        {
            ClearCurrentInteractable();
            return;
        }

        SetCurrentInteractable(nearestInteractable, nearestCollider);
    }

   private void HandleInteractionInput()
{
    if (!Input.GetKeyDown(interactionKey))
    {
        return;
    }

    if (currentInteractable == null)
    {
        Debug.Log("Tidak ada objek yang bisa diinteraksi.");
        return;
    }
    
    currentInteractable.Interact(this);
    }

    private void SetCurrentInteractable(IInteractable interactable, Collider2D interactableCollider)
    {
        if (currentInteractable == interactable)
        {
            return;
        }

        currentInteractable = interactable;
        currentInteractableCollider = interactableCollider;

        Debug.Log(currentInteractable.GetInteractionText());
    }

    private void ClearCurrentInteractable()
    {
        if (currentInteractable == null)
        {
            return;
        }

        currentInteractable = null;
        currentInteractableCollider = null;

        Debug.Log("Keluar dari area interaksi.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        if (currentInteractableCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, currentInteractableCollider.transform.position);
        }
    }
}