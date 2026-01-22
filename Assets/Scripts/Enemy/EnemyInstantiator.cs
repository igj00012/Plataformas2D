using System.Collections;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenEnemyInstantiations = 5f;

    Sight2D sight2D;

    private void Awake()
    {
        sight2D = GetComponent<Sight2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(InstantiateEnemies(timeBetweenEnemyInstantiations));
    }

    bool active = false;
    private void Update()
    {
        if (sight2D.IsPlayerInSight() && !active)
        {
            StartCoroutine(InstantiateEnemies(timeBetweenEnemyInstantiations));

            active = true;
        }
    }

    IEnumerator InstantiateEnemies(float timeBetweenInstantiations)
    {
        while (true)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenInstantiations);
        }
    }
}
