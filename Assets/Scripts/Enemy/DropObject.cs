using UnityEngine;

public class DropObject : MonoBehaviour
{
    [SerializeField] float lifeTime;

    [SerializeField] AudioClip sfx;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(sfx);
            Destroy(gameObject);
        }
    }
}
