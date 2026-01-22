using System;
using Unity.VisualScripting;
using UnityEngine;

public class CaparazonMovement : MonoBehaviour
{
    [SerializeField] Transform hitBox;

    Animator animator;
    Rigidbody2D rb2D;
    bool isMoving = false;
    bool destroyScheduled = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        hitBox.gameObject.SetActive(false);
    }

    float lifeTime = 5f;
    float speed = 3f;
    float direction = 0f;
    private void Update()
    {
        if (!isMoving) return;

        rb2D.linearVelocityX = direction * speed;
        Debug.Log("Dirección: " + direction);

        if (!destroyScheduled)
        {
            Destroy(gameObject, lifeTime);
            destroyScheduled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isMoving = true;

            float hitOffset = collision.transform.position.x - transform.position.x;
            if (hitOffset < 0) direction = 1f;
            else direction = -1f;

            hitBox.gameObject.SetActive(true);

            animator.SetBool("Walking", true);
        }
        else if (!collision.collider.CompareTag("Player") && isMoving)
        {
            direction *= -1f;

            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);

            animator.SetBool("Walking", true);
        }
    }
}
