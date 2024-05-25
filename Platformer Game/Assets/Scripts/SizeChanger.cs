using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    private bool sizeChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Transform>().localScale.x >= 1f && collision.GetComponent<Transform>().localScale.x <= 2f && !sizeChanged)
            {
                sizeChanged = true;
                collision.GetComponent<Transform>().localScale /= 2f;
                collision.GetComponent<PlayerMovement>().moveSpeed *= 1.25f;
            }
            else if (collision.GetComponent<Transform>().localScale.x >= 0.5f && collision.GetComponent<Transform>().localScale.x <= 1f && !sizeChanged)
            {
                sizeChanged = true;
                collision.GetComponent<Transform>().localScale *= 2f;
                collision.GetComponent<PlayerMovement>().moveSpeed /= 1.25f;
            }
            sizeChanged = false;
        }
    }
}
