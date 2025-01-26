using UnityEngine;

public class MoveToPlayerStart : MonoBehaviour
{
    [Header("Player Start Transform")]
    [Tooltip("Assign the Transform where the object should move on Start.")]
    public Transform playerStart;

    [Header("Gizmo Settings")]
    [Tooltip("The color of the gizmo in the editor.")]
    public Color gizmoColor = Color.green;

    [Tooltip("The size of the gizmo sphere.")]
    public float gizmoSize = 0.5f;

    private void Start()
    {
        // Check if PlayerStart is assigned
        if (playerStart == null)
        {
            Debug.LogError("PlayerStart Transform is not assigned on " + gameObject.name);
            return;
        }

        // Move the object to the PlayerStart position
        playerStart.position = transform.position;
        playerStart.rotation = transform.rotation;
        Debug.Log(gameObject.name + " moved to PlayerStart position.");
    }

    private void OnDrawGizmos()
    {
        if (playerStart != null)
        {
            // Set gizmo color
            Gizmos.color = gizmoColor;

            // Draw a sphere at the PlayerStart position
            Gizmos.DrawSphere(playerStart.position, gizmoSize);

            // Draw a line connecting the current position to the PlayerStart position
            Gizmos.DrawLine(transform.position, playerStart.position);
        }
    }
}
