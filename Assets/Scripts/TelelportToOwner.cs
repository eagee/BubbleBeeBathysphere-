using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class TeleportToPosition : MonoBehaviour
{
    [Header("Tag to Detect")]
    [Tooltip("Tag to identify the objects to teleport.")]
    public string targetTag = "BathySphere";

    public List<GameObject> objectsToActivate;

    public Transform newCameraTarget;
    public Transform newPlayer1Transform;

    private GameObject otherGameObject;

    private SmoothCameraFollow cameraFollow;

    private Coroutine teleportCoroutine;

    private void Start()
    {
        // Ensure the BoxCollider2D is set as a trigger
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraFollow = mainCamera.GetComponent<SmoothCameraFollow>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the specified tag
        if (other.CompareTag(targetTag))
        {
            // Move the object to this game object's transform
            if (objectsToActivate.Count > 0)
            {
                foreach (GameObject obj in objectsToActivate)
                {
                    obj.SetActive(true);
                }
            }
            teleportCoroutine = StartCoroutine(TeleportInJustABit());
            otherGameObject = other.gameObject;
            cameraFollow.target = newCameraTarget;
        }
    }

    private IEnumerator TeleportInJustABit()
    {
        yield return new WaitForSeconds(3f);

        otherGameObject.transform.position = newPlayer1Transform.transform.position;
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        // Draw gizmo for the BoxCollider2D
        Gizmos.color = Color.red;

        // Get the BoxCollider2D
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            // Draw a wireframe around the collider
            Vector2 size = boxCollider.size;
            Vector3 offset = boxCollider.offset;
            Vector3 center = transform.position + (Vector3)offset;

            Gizmos.DrawWireCube(center, new Vector3(size.x, size.y, 0));
        }

        // Draw gizmo for the game object's position
        Gizmos.DrawSphere(transform.position, 2.0f); // Small sphere to mark the game object's position
    }
}
