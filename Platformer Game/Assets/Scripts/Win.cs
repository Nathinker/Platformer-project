using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Win.cs
public class Win : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject winMenu;
    #endregion

    #region Methods
    // A function called when a collider2D enters the trigger, when this function is called it stops time and displays the win menu.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().paused = true;
        }
    }
    #endregion
}
#endregion
