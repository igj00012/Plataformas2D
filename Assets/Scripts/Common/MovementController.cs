using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Punch Settings")]
    [SerializeField] Transform punchHit;
    [SerializeField] float punchHitDuration = 0.25f;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float doubleJumpForce = 5f;

    [Header("Health Settings")]
    [SerializeField] protected int maxHealth = 5;

    [Header("Jump Settings")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int maxJumps = 2;

    [SerializeField] GameObject spawnPrefab;
    [SerializeField] float spawnProb = 0.8f;

    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip jump;

    Rigidbody2D rb2D;
    protected Animator animator;
    protected int currentHealth;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    protected Vector2 desiredMove = Vector2.zero;
    protected bool mustJump = false;
    protected bool mustPunch = false;
    protected bool criticalHit = false;
    int currentJumps;
    bool isGrounded;
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
            if (criticalHit)
            {
                animator.SetTrigger("CriticalAttack");
                criticalHit = false;
                mustPunch = false;
                return;
            }
            animator.SetTrigger("PerformPunch");
            mustPunch = false;
        }

        if (desiredMove.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (desiredMove.x > 0f)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);

        if (isGrounded && rb2D.linearVelocityY <= 0.01f)
        {
            currentJumps = 0;
        }

        if (mustJump && currentJumps < maxJumps)
        {
            if (currentJumps > 0)
            {
                rb2D.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            mustJump = false;
            currentJumps++;

            animator.SetTrigger("Jump");

            AudioManager.instance.PlaySFX(jump);
        }
    }

    protected void PerformPunch()
    {
        mustPunch = true;
        punchHit.gameObject.SetActive(true);
        Invoke(nameof(DeactivatePunchHit), punchHitDuration);
    }

    void DeactivatePunchHit()
    {
        punchHit.gameObject.SetActive(false);
    }

    int critDamage = 2;
    public virtual void NotifyHit(Hitbox2D hitbox)
    {
        if (criticalHit)
        {
            currentHealth -= hitbox.GetDamage() * critDamage;
        }
        else currentHealth -= hitbox.GetDamage();

        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            SpawnObject();

            Destroy(gameObject);
        }

        AudioManager.instance.PlaySFX(hit);

    }

    private void SpawnObject()
    {
        if (Random.value <= spawnProb && spawnPrefab != null)
        {
            Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        }
    }
}
