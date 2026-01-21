using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float imageWidth;

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = speed * Time.time;
        float remainingDistance = distance % imageWidth;
        transform.position = startPosition + direction * remainingDistance;
    }
}
