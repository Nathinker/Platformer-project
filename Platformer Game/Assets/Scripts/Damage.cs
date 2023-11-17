using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{
    [SerializeField] float damage = -1;
    [SerializeField] string damageTag = "Player";
    Health healthScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            collision.gameObject.GetComponent<Health>().addHealth(damage);
        }
    }
}
