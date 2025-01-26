using UnityEngine;

public class FishSwimmingWithSineWave : MonoBehaviour
{
    [Header("Swim Area")]
    [Tooltip("The width of the area where the fish can swim.")]
    public float swimWidth = 20f;

    [Tooltip("The height amplitude of the sine wave motion.")]
    public float swimHeight = 0.5f;

    [Header("Swimming Behavior")]
    [Tooltip("How fast the fish moves left and right.")]
    public float horizontalSpeed = 2f;

    [Tooltip("How fast the sine wave oscillates vertically.")]
    public float sineWaveFrequency = 2f;

    private Vector3 startPosition;
    private bool movingRight = true;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // Record the starting position of the fish
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the horizontal movement
        float horizontalOffset = movingRight ? horizontalSpeed * Time.deltaTime : -horizontalSpeed * Time.deltaTime;
        float newX = transform.position.x + horizontalOffset;

        // Reverse direction if the fish reaches the swim area bounds
        if (newX > startPosition.x + swimWidth / 2)
        {
            movingRight = false;
        }
        else if (newX < startPosition.x - swimWidth / 2)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

        // Calculate the vertical sine wave motion
        float sineOffset = Mathf.Sin(Time.time * sineWaveFrequency) * swimHeight;

        // Apply the new position
        transform.position = new Vector3(newX, startPosition.y + sineOffset, startPosition.z);
    }

    private void OnDrawGizmos()
    {
        // Draw the swim area in the Scene view
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(startPosition, new Vector3(swimWidth, swimHeight * 2, 0.1f));
    }
}