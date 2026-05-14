using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField] private float visionRange = 5f;
    [SerializeField] private LayerMask visionTargetLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Performance")]
    [SerializeField] private float refreshInterval = 0.1f;

    private readonly List<SpriteRenderer> visionTargets = new List<SpriteRenderer>();
    private float refreshTimer;

    private void Start()
    {
        CollectVisionTargets();
        RefreshVision();
    }

    private void Update()
    {
        refreshTimer += Time.deltaTime;

        if (refreshTimer >= refreshInterval)
        {
            refreshTimer = 0f;
            RefreshVision();
        }
    }

    private void CollectVisionTargets()
    {
        visionTargets.Clear();

        SpriteRenderer[] allRenderers = Object.FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None);

        foreach (SpriteRenderer renderer in allRenderers)
        {
            if (renderer == null)
                continue;

            if (renderer.gameObject == gameObject)
                continue;

            if (IsLayerInMask(renderer.gameObject.layer, visionTargetLayer))
            {
                visionTargets.Add(renderer);
            }
        }

        Debug.Log($"PlayerVision collected {visionTargets.Count} vision targets.");
    }

    private void RefreshVision()
    {
        foreach (SpriteRenderer targetRenderer in visionTargets)
        {
            if (targetRenderer == null)
                continue;

            bool visible = IsTargetVisible(targetRenderer.transform.position);
            targetRenderer.enabled = visible;
        }
    }

    private bool IsTargetVisible(Vector2 targetPosition)
    {
        Vector2 playerPosition = transform.position;
        Vector2 direction = targetPosition - playerPosition;
        float distance = direction.magnitude;

        if (distance > visionRange)
            return false;

        RaycastHit2D wallHit = Physics2D.Raycast(
            playerPosition,
            direction.normalized,
            distance,
            wallLayer
        );

        if (wallHit.collider != null)
            return false;

        return true;
    }

    private bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }

    public void RefreshTargets()
    {
        CollectVisionTargets();
        RefreshVision();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}