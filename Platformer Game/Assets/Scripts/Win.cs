using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Win : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject winMenu;
    #endregion

    #region OnTriggerEnter2D
    // A function called when a collider2D enters the trigger, when this function is called it stops time and displays the win menu.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0;
            winMenu.SetActive(true);
            collision.GetComponent<PlayerMovement>().paused = true;
        }
    }
    #endregion
}
