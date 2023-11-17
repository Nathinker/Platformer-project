using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] TextMeshProUGUI printer;

    // Start is called before the first frame update
    void Start()
    {
        printer.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHealth(float healthToAdd)
    {
        health += healthToAdd;
        printer.text = "Health: " + health;
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
