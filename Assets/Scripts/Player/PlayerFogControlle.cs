using UnityEngine;

public class PlayerFogController : MonoBehaviour
{
    public Transform player;

    [Header("Fog Settings")]
    public Color fogColor = Color.black;
    public FogMode fogMode = FogMode.ExponentialSquared;
    public float fogDensity = 0.04f;

    void Start()
    {
        ApplyFog();
    }

    void Update()
    {
        if (player == null) return;

        ApplyFog();
    }

    void ApplyFog()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogDensity = fogDensity;
    }
}