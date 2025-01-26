using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Configuration")]
    [Tooltip("The prefab to spawn.")]
    public GameObject objectToSpawn;

    [Tooltip("The interval (in seconds) between spawns.")]
    public float spawnRate = 2f;

    [Tooltip("Minimum scale for the spawned objects.")]
    public float minScale = 1f;

    [Tooltip("Maximum scale for the spawned objects.")]
    public float maxScale = 2f;

    [Header("Bounding Box Settings")]
    [Tooltip("Width of the spawning area.")]
    public float boundingBoxWidth = 10f;

    [Tooltip("Fixed Y position for spawning.")]
    public float fixedYPosition = 0f;

    [Tooltip("Minimum Y offset from the fixed Y position.")]
    public float minYOffset = -1f;

    [Tooltip("Maximum Y offset from the fixed Y position.")]
    public float maxYOffset = 1f;

    [Tooltip("Color of the gizmo for the bounding box.")]
    public Color gizmoColor = Color.green;

    private void Start()
    {
        // Start spawning objects at the specified rate
        InvokeRepeating(nameof(SpawnObject), 0f, spawnRate);
    }

    private void SpawnObject()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("ObjectSpawner: No object assigned to spawn.");
            return;
        }

        // Calculate random spawn position within the bounding box
        float randomX = Random.Range(-boundingBoxWidth / 2f, boundingBoxWidth / 2f);
        float randomY = fixedYPosition + Random.Range(minYOffset, maxYOffset);
        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0);

        // Spawn the object
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Apply random scale to the object
        float randomScale = Random.Range(minScale, maxScale);
        spawnedObject.transform.localScale = Vector3.one * randomScale;
    }

    private void OnDrawGizmos()
    {
        // Draw the bounding box in the editor
        Gizmos.color = gizmoColor;

        // Define the bounding box for visualization
        Vector3 center = transform.position + new Vector3(0, fixedYPosition, 0);
        Vector3 size = new Vector3(boundingBoxWidth, maxYOffset - minYOffset, 0);

        Gizmos.DrawWireCube(center, size);
    }
}
