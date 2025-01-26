
using UnityEngine;

public class SmoothCameraFollow2 : MonoBehaviour
{
/*    [Header("Target Settings")]
    public GameObject[] targets; // The players or objects the camera will follow
    public bool[] include_target; // For each target, include in frame if true
    public float max_viewport_size; // Furthest zoom out
    public float min_viewport_size; // Furthest zoom in
    public float zoom_speed;

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
        if (targets.Length != include_target.Length) {
            Debug.LogError("Camera target lists don't match!");
        }

        float totalX = 0f, totalY = 0f;
        int num_targets = 0;
        for (int i = 0; i < targets.Length; i++) {
            if (include_target[i]) {
                num_targets++;
                totalX += targets[i].transform.position.x;
                totalY += targets[i].transform.position.y;
            }
        }
        if (num_targets == 0) {
            Debug.Log("Camera: no targets.");
            return;
        }

        // Calculate target position with offset
        Vector3 targetPosition = new Vector3(
            totalX / num_targets,
            totalY / num_targets,
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

        // Can we see all targets?
        Camera c = GetComponent<Camera>();
        bool all_inner = true;
        bool all_outer = true;
        for (int i = 0; i < targets.Length; i++) {
            if (include_target[i]) {
                Vector3 viewPoint = c.WorldToViewportPoint(targets[i].transform.position);
                bool inInner = viewPoint.x > 0.4 &&
                                 viewPoint.x < 0.6 &&
                                 viewPoint.y > 0.4 &&
                                 viewPoint.y < 0.6;
                bool inOuter = viewPoint.x > 0 &&
                                 viewPoint.x < 1 &&
                                 viewPoint.y > 0 &&
                                 viewPoint.y < 1;
                if (!inOuter) {
                    Debug.Log(targets[i].name + " is not visible!");
                    all_outer = false;
                    all_inner = false;
                    break;
                } else {
                    Debug.Log(targets[i].name + " is visible.");
                    if (!inInner) {
                        all_inner = false;
                    }
                }
            }
        }

        if (all_inner && c.orthographicSize > min_viewport_size) {
            c.orthographicSize -= zoom_speed;
        }
        if (!all_outer && c.orthographicSize < max_viewport_size) {
            c.orthographicSize += zoom_speed;
        }

        // Update camera position
        transform.position = smoothPosition;
    }
    */
}