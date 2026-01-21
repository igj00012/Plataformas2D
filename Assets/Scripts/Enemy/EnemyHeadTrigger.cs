using UnityEngine;

public class EnemyHeadTrigger : MonoBehaviour
{
    [SerializeField] PatrollingEnemy patrollingEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.attachedRigidbody.linearVelocityY <= 0f)
            {
                Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
                patrollingEnemy.OnHeadStomped(playerRb);
            }
        }
    }
}
