using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingFloor : MonoBehaviour
{
    TilemapCollider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<TilemapCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collider2D.enabled = false;
        }
    }
}
