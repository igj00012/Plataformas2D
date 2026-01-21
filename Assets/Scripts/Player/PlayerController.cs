using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovementController
{
    [SerializeField] HPManager HPManager;

    public static PlayerController instance;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void Update()
    {
        UpdateRawMove();
        base.Update();
    }

    private float critProb = 0.4f;
    private void UpdateRawMove()
    {
        Vector2 rawMove = Vector2.zero;

        if (Keyboard.current.aKey.isPressed)
        {
            rawMove += Vector2.left;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            rawMove += Vector2.right;
        }

        desiredMove = rawMove;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            mustJump = true;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Random.Range(0f, 1f) <= critProb)
            {
                criticalHit = true;
            }

            PerformPunch();
        }
    }

    public override void NotifyHit(Hitbox2D hitbox)
    {
        currentHealth -= hitbox.GetDamage();

        animator.SetTrigger("Hit");

        HPManager.UpdateHP(currentHealth);

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

            //Destroy(gameObject);
        }
    }
}
