using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    public bool readyJump = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0; // Disable the default gravity
        direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity
        rb.AddForce(Vector2.down * gravity, ForceMode2D.Force);

        // Limit vertical speed to avoid continuous acceleration
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -10f, 10f));

        if (Input.GetButton("Jump"))
        {
            readyJump = true;
        }

        if (readyJump)
        {
            Jump();
            readyJump = false;
        }
    }

    public void Jump()
    {
        Debug.Log("Jump method is called.");
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Force applied for jump.");
        }
    }

    public bool IsGrounded()
    {
        int groundLayerMask = LayerMask.GetMask("Ground");

        float raycastLength = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);

        return hit.collider != null;
    }
}
