using UnityEngine;

public class EnemyController : MovementController
{
    [SerializeField] float distanceToPunch = 0.75f;
    [SerializeField] float timeBetweenPunches = 1f;

    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float probabilityToSpawn = 0.5f;

    Transform player;
    float lastPunchTime;

    protected override void Update()
    {
        player = PlayerController.instance.transform;

        RunToPlayer();

        if (player.gameObject.activeSelf)
        {
            CheckAndPerformPunch();
        }
        else
        {
            desiredMove.x *= -1f;
        }

        base.Update();
    }

    private void OnDestroy()
    {
        if (objectToSpawn != null)
        {
            if (Random.value <= probabilityToSpawn)
            {
                Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            }
        }
    }

    private void CheckAndPerformPunch()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) < distanceToPunch)
        {
            desiredMove = Vector2.zero;

            if (Time.time - lastPunchTime > timeBetweenPunches)
            {
                PerformPunch();
                lastPunchTime = Time.time;
            }
        }
    }

    protected virtual void RunToPlayer()
    {
        if (player.position.x < transform.position.x)
        {
            desiredMove = Vector2.left;
        }
        else
        {
            desiredMove = Vector2.right;
        }
    }
}
