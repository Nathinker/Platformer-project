using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject winMenu;

    // A function called when a collider2D enters the trigger, when this function is called it stops time and displays the win menu.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }
}
