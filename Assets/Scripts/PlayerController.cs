using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Raycast")]
    [SerializeField]
    private float raycastLength = 0.2f;

    [Header("Variables")]
    [SerializeField]
    private Rigidbody2D rb;
    public Vector2 direction;

    public float jumpForce = 8f;


    [Header("Debug")]
    [SerializeField]
    private bool debugRay;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Limit vertical speed to avoid continuous acceleration
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -10f, 10f));

        if (debugRay)
            Debug.DrawRay(transform.position, Vector2.down * raycastLength, Color.red);
    }

    public void Jump()
    {
        if (IsGrounded())// && rb.velocity.y < 0.001)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        int groundLayerMask = LayerMask.GetMask("Ground");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);

        return hit.collider != null;
    }
}
