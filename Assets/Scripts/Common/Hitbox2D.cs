using System;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox2D : MonoBehaviour
{
    [SerializeField] string affectedTag = "Enemy";
    [SerializeField] int damage = 1;

    List<GameObject> hitObjects;

    private void Awake()
    {
        hitObjects = new List<GameObject>();
    }

    private void OnEnable()
    {
        hitObjects.Clear();

        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

        for (int i = 0; i < colliders.Length; ++i)
        {
            TryHit(colliders[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryHit(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hitObjects.Contains(collision.gameObject))
        {
            hitObjects.Remove(collision.gameObject);
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    void TryHit(Collider2D collision)
    {
        if (hitObjects.Contains(collision.gameObject))
        {
            return;
        }
        else if (collision.CompareTag(affectedTag))
        {
            MovementController movementController = collision.GetComponent<MovementController>();
            movementController.NotifyHit(this);
            hitObjects.Add(collision.gameObject);
        }
    }
}
