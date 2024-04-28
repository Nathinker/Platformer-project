using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3;
    [SerializeField] private TextMeshProUGUI printer;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject playerPre;
    private Vector2 playerPos;
    public Transform p_RespawnPoint;
    public bool isDead;
    private float health = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        printer.text = $"Health: {(int)health}";
        playerPos.Set(p_RespawnPoint.position.x, p_RespawnPoint.position.y);
    }

    // Update is called once per frame
    void Update()
    {}

    // Adds the specificed amount of health
    public void AddHealth(float amount)
    {
        health += amount;
        printer.text = $"Health: {(int)health}";
        // If the player's health is 0 or less, the player dies
        if (health <= 0)
        {
            Death();
        }
    }

    // Sets the player as deas, displays the game-over menu, freezes time, and deactivates the player character
    public void Death()
    {
        isDead = true;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        playerPre.SetActive(false);
    }

    // Method to respawn the player, and sets the corresponding variables and objects accordingly
    public void Respawn()
    {
        isDead = false;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        health = startingHealth;
        printer.text = $"Health: {health}";
        transform.position = playerPos;
        playerPre.SetActive(true);
        playerPre.GetComponent<Move>().RespawnPositioning();
    }
}