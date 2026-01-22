using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.Die();
        }
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
