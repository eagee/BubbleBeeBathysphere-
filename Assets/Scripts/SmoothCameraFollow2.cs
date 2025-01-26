using UnityEngine;

public class SmoothCameraFollow2 : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform[] targets; // The players or objects the camera will follow

    [Header("Follow Settings")]
    public float followSpeed = 2f; // Speed of the camera movement
    // public Vector2 offset = Vector2.zero; // Offset from the target's position

    [Header("Boundary Settings (Optional)")]
    public bool useBoundaries = false; // Enable boundaries for camera movement
    public Vector2 minBoundary; // Minimum x and y values
    public Vector2 maxBoundary; // Maximum x and y values

    [Header("Underwater Settings")]
    public float bobAmplitude = 0.5f; // How much the camera "bobs" up and down
    public float bobFrequency = 1f; // How fast the bobbing happens

    private float bobTimer;

    void LateUpdate()
    {
        if (targets.Length == 0) return;

        float totalX = 0f, totalY = 0f;

        foreach (Transform target in targets) {
            totalX += target.position.x;
            totalY += target.position.y;
        }
        // Calculate target position with offset
        Vector3 targetPosition = new Vector3(
            totalX / targets.Length,
            totalY / targets.Length,
            transform.position.z // Keep the camera's Z position
        );

        // Apply optional bobbing effect for an underwater feel
        bobTimer += Time.deltaTime * bobFrequency;
        float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
        targetPosition.y += bobOffset;

        // Smoothly move the camera towards the target
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Apply boundaries if enabled
        if (useBoundaries)
        {
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minBoundary.x, maxBoundary.x);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minBoundary.y, maxBoundary.y);
        }

        // Update camera position
        transform.position = smoothPosition;
    }
}