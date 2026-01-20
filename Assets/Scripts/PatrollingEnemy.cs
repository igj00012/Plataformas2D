using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float patrolDistance = 5f;

    Rigidbody2D rb2D;
    float direction = 1f;
    Vector3 startPosition;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Update()
    {
        float offset = Mathf.Abs(transform.position.x - startPosition.x);
        if (offset >= patrolDistance)
        {
            direction *= -1f;
            transform.localScale = new Vector3(direction, 1f, 1f);
        }

        rb2D.linearVelocityX = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player detected by PatrollingEnemy");
        }
    }
}
