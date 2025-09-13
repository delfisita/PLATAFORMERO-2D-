using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5f;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;

    [Header("Colliders")]
    public Collider2D standingCollider;
    public Collider2D crouchCollider;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    private bool isCrouching;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (!isDashing)
        {
            float speed = isCrouching ? crouchSpeed : moveSpeed;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(Dash(horizontal));
        }

        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }

    private void Crouch(bool state)
    {
        if (isCrouching == state) return;
        isCrouching = state;
        if (standingCollider != null) standingCollider.enabled = !state;
        if (crouchCollider != null) crouchCollider.enabled = state;
    }

    private IEnumerator Dash(float direction)
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(direction * dashSpeed, 0f);
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = false;
                return;
            }
        }
    }
}
