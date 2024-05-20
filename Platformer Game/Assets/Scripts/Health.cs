using UnityEngine;
using TMPro;
using UnityEditor;

public class Health : MonoBehaviour
{
    #region Fields
    [SerializeField] private float startingHealth = 3;

    [SerializeField] private TextMeshProUGUI printer;

    [SerializeField] private GameObject[] uiMenus;

    [SerializeField] private GameObject playerPre;

    private Vector2 playerPos;

    public Transform p_RespawnPoint;

    public bool isDead;

    private float health = 0;
    #endregion

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        printer.text = $"Health: {(int)health}";
        playerPos.Set(p_RespawnPoint.position.x, p_RespawnPoint.position.y);
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update() { }
    #endregion

    #region AddHealth
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
    #endregion

    #region Death
    // Sets the player as deas, displays the game-over menu, freezes time, and deactivates the player character
    public void Death()
    {
        isDead = true;
        uiMenus[0].SetActive(true);
        Time.timeScale = 0;
        playerPre.SetActive(false);
    }
    #endregion

    #region Respawn
    // Method to respawn the player, and sets the corresponding variables and objects accordingly
    public void Respawn()
    {
        isDead = false;
        foreach (var uiMenu in uiMenus)
        {
            uiMenu.SetActive(false);
        }
        playerPre.GetComponent<PlayerMovement>().paused = false;
        Time.timeScale = 1f;
        health = startingHealth;
        printer.text = $"Health: {health}";
        transform.position = playerPos;
        playerPre.SetActive(true);
        playerPre.GetComponent<PlayerMovement>().ResetInput();
        playerPre.GetComponent<PlayerMovement>().RespawnPositioning();
    }
    #endregion
}
