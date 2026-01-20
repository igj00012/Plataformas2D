using UnityEngine;

public class Sight2D : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [SerializeField] float checkFrequency = 5f;

    float lastCheckTime = 0f;
    Collider2D[] colliders;
    void Update()
    {
        if ((Time.time - lastCheckTime) > (1f / checkFrequency))
        {
            lastCheckTime = Time.time;
            Debug.Log("Checking sight");
            colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log($"El collider se llama {i} se llama {colliders[i].name}", colliders[i]);
            }
        }
    }

    public bool IsPlayerInSight()
    {
        bool isPlayerInSight = false;

        for (int i = 0; (!isPlayerInSight && (i < colliders.Length)); i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                isPlayerInSight = true;
            }
        }

        return isPlayerInSight;
    }
}
