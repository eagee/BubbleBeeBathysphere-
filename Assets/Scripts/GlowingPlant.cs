using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // For Light2D

[RequireComponent(typeof(Collider2D))]
public class GlowingPlant : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Assign the 2D Light you want to control.")]
    public Light2D targetLight;

    [Header("Light Settings")]
    [Tooltip("The radius we set when a 'Bubble' enters the trigger.")]
    public float triggeredOuterRadius = 15f;

    public float minimumOuterRadius = 1f;

    [Tooltip("How many seconds it takes to lerp to and from the triggered radius.")]
    public float lerpTime = 2f;

    [Tooltip("How long the light stays at the triggered radius before shrinking.")]
    public float holdTime = 1f;

    [Header("Bob Settings")]
    [Tooltip("How much the light will bob up and down in its outer radius.")]
    public float bobAmplitude = 0.2f;

    [Tooltip("How fast the light bobs.")]
    public float bobFrequency = 2f;

    public bool alwaysTriggered = false;

    private float RandomAlwayOnTrigger;

    private Coroutine lightCoroutine; // Stores the active coroutine for the light

    public string lightTriggeredColor = "#7DF903";

    private void Start()
    {
        RandomAlwayOnTrigger = Random.Range(0f, 3f);
        
        // Ensure the collider is set to trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        targetLight = GetComponent<Light2D>();

        // Initialize the light
        if (targetLight == null)
        {
            Debug.LogError("No Light2D assigned to GrowingPlant script on " + gameObject.name);
        }
        else
        {
            targetLight.pointLightOuterRadius = minimumOuterRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that entered is tagged "Bubble"
        if (other.CompareTag("Bubble"))
        {
            if (lightCoroutine != null)
            {
                return;
            }

            // Start the lerping coroutine
            lightCoroutine = StartCoroutine(LerpLightRadius());
        }
    }
    
    public static Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        
        return Color.black; // Fallback color
    }
	private void Update()
    {
		
		if(lightCoroutine != null)
		{
            targetLight.color = HexToColor(lightTriggeredColor);
			return;
		}

        if (alwaysTriggered)
        {
            if (RandomAlwayOnTrigger > 0f)
            {
                RandomAlwayOnTrigger -= Time.deltaTime;
            }

            if (RandomAlwayOnTrigger < 0)
            {
                lightCoroutine = StartCoroutine(LerpLightRadius());
            }
        }

        targetLight.color = HexToColor("#6B572F");
        // Currently disabled b/c janky
        //float baseRadius = targetLight.pointLightOuterRadius;        
        //
        //// Add a small bob offset
        //float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        //
        //// Combine them to get the final radius
        //targetLight.pointLightOuterRadius = baseRadius + bobOffset;
    }

    private IEnumerator LerpLightRadius()
    {
        float elapsed = 0f;

        // Lerp from the current radius to triggeredOuterRadius with quadratic easing
        float initialRadius = targetLight.pointLightOuterRadius;
        while (elapsed < lerpTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lerpTime;

            // Quadratic easing for smooth transitions
            t = t * t * (3 - 2 * t); // Smoothstep: Ease-in and Ease-out

            targetLight.pointLightOuterRadius = Mathf.Lerp(initialRadius, triggeredOuterRadius, t);

            // Add bobbing effect
            float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
            targetLight.pointLightOuterRadius += bobOffset;

            yield return null;
        }

        // Hold at triggeredOuterRadius
        elapsed = 0f;
        while (elapsed < holdTime)
        {
            elapsed += Time.deltaTime;

            // Keep adding bobbing effect
            float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
            targetLight.pointLightOuterRadius = triggeredOuterRadius + bobOffset;

            yield return null;
        }

        // Lerp back to minimumOuterRadius with quadratic easing
        elapsed = 0f;
        while (elapsed < lerpTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lerpTime;

            // Quadratic easing for smooth transitions
            t = t * t * (3 - 2 * t); // Smoothstep: Ease-in and Ease-out

            targetLight.pointLightOuterRadius = Mathf.Lerp(triggeredOuterRadius, minimumOuterRadius, t);

            // Add bobbing effect
            float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
            targetLight.pointLightOuterRadius += bobOffset;

            yield return null;
        }

        // Ensure it snaps to the minimum radius at the end
        targetLight.pointLightOuterRadius = minimumOuterRadius;
        lightCoroutine = null;
    }

}
