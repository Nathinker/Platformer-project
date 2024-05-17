using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float damage = -1;
    [SerializeField] string damageTag = "Player";
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            if (collision.gameObject.TryGetComponent<Health>(out var hl))
            {
                hl.AddHealth(damage);
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("World"))
        {
            Destroy(gameObject);
        }
    }
}
