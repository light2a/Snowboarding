using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomGravityController : MonoBehaviour
{
    public FloatVariable GravityUp;
    public FloatVariable GravityDown;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check vertical velocity
        if (rb.velocity.y > 0.01f && rb.velocity.magnitude> 1)
        {
            // Moving upward
            rb.gravityScale = this.GravityUp.Value;

        }
        else if (rb.velocity.y < -0.01f)
        {
            // Falling downward
            rb.gravityScale = this.GravityDown.Value;
        }
        // Optional: keep gravity scale unchanged if vertical speed is near zero
    }
}
