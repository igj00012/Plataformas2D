using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected Vector2 desiredMove = Vector2.zero;
    protected bool mustJump = false;
    protected bool mustPunch = false;
    protected virtual void Update()
    {
        rb2D.linearVelocityX = desiredMove.x * walkSpeed;

        if (desiredMove.x != 0f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (mustPunch)
        {
            animator.SetTrigger("PerformPunch");
            mustPunch = false;
        }

        if (desiredMove.x < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (desiredMove.x > 0f)
        {
            spriteRenderer.flipX = false;
        }

        if (mustJump)
        {
            rb2D.linearVelocityY = jumpForce;
            mustJump = false;
        }
    }
}
