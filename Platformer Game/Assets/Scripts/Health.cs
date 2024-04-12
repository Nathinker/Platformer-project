using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 3; // The current health of the player
    [SerializeField] private TextMeshProUGUI printer; // The UI text to display the health
    [SerializeField] private GameObject gameOverMenu; // The game over menu UI
    [SerializeField] private GameObject playerPre; // The player prefab
    private Vector2 playerPos; // The position of the player
    public Transform p_RespawnPoint; // The respawn point of the player
    public bool isDead = false; // Flag to check if the player is dead

    // Start is called before the first frame update
    void Start()
    {
        printer.text = "Health: " + health; // Display the initial health
        playerPos.Set(p_RespawnPoint.position.x, p_RespawnPoint.position.y); // Set the initial player position
    }

    // Update is called once per frame
    void Update()
    {
        // Method intentionally left empty.
    }

    // Add health to the player
    public void AddHealth(float healthToAdd)
    {
        health += healthToAdd; // Increase the health
        printer.text = "Health: " + health; // Update the UI text
        if (health <= 0)
        {
            Death(); // If health reaches 0 or below, call the Death method
        }
    }

    // Handle player death
    public void Death()
    {
        isDead = true; // Set the isDead flag to true
        gameOverMenu.SetActive(true); // Show the game over menu
        Time.timeScale = 0; // Pause the game
        playerPre.SetActive(false); // Disable the player prefab
    }

    // Respawn the player
    public void Respawn()
    {
        isDead = false; // Set the isDead flag to false
        gameOverMenu.SetActive(false); // Hide the game over menu
        Time.timeScale = 1; // Resume the game
        health = 3; // Reset the health to its initial value
        printer.text = "Health: " + health; // Update the UI text
        transform.position = playerPos; // Set the player position to the respawn point
        playerPre.SetActive(true); // Enable the player prefab
    }
}