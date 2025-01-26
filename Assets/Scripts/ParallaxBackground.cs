using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Assign your Camera here (optional).")]
    public Transform cameraTransform; 
    // If left unassigned, the script will use Camera.main at runtime.

    [Header("Parallax Factors (smaller = more subtle)")]
    [Tooltip("Horizontal parallax factor, e.g. 0.5 = background moves at half the camera speed.")]
    public float horizontalParallaxFactor = 0.5f;

    [Tooltip("Vertical parallax factor, e.g. 0.1 = background moves at 1/10th the camera speed.")]
    public float verticalParallaxFactor = 0.1f;

    // We'll store the camera's position from the previous frame
    private Vector3 lastCameraPosition;

    private void Start()
    {
        // If no camera was assigned, default to main camera
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Initialize the last known camera position
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        // Calculate how far the camera has moved since the last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Move this background by a fraction of that distance 
        // (horizontal factor for X, vertical factor for Y)
        transform.position += new Vector3(
            deltaMovement.x * horizontalParallaxFactor,
            deltaMovement.y * verticalParallaxFactor,
            0f
        );

        // Update the camera's last position to this frame's position
        lastCameraPosition = cameraTransform.position;
    }
}