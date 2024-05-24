using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hint : MonoBehaviour
{
    [SerializeField] GameObject hintText;
    
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
        if (collision.gameObject.CompareTag("Player") == true)
        {
            hintText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            hintText.SetActive(false);
        }
    }
}