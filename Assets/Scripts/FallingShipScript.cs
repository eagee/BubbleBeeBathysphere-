using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class FallingObject : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Assign the particle system to play when the object hits the floor.")]
    public ParticleSystem hitEffect;

    private Rigidbody2D rb2d;
    private CapsuleCollider2D capsuleCollider;

    void Start()
    {
        // Get the required components
        rb2d = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Ensure the Rigidbody2D is set to dynamic
        rb2d.bodyType = RigidbodyType2D.Dynamic;

        // Enable the capsule collider
        capsuleCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with a GameObject tagged as "Floor"
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Make the Rigidbody2D kinematic
            rb2d.gravityScale = 0f;
            rb2d.mass = 0f;
            rb2d.isKinematic = true;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            // Disable the collider
            var offset = capsuleCollider.offset;
            offset.y = -9.68f;
            capsuleCollider.offset = offset;

            // Play the particle effect if assigned
            if (hitEffect != null)
            {
                hitEffect.Play();
            }
        }
    }
}
