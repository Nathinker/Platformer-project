using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 3;
    [SerializeField] private TextMeshProUGUI printer;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject playerPre;
    private Vector2 playerPos;
    public Transform p_RespawnPoint;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        printer.text = "Health: " + health;
        playerPos.Set(p_RespawnPoint.position.x, p_RespawnPoint.position.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddHealth(float healthToAdd)
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
        isDead = true;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        playerPre.SetActive(false);
    }

    public void Respawn()
    {
        isDead = false;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1;
        health = 3;
        printer.text = "Health: " + health;
        transform.position = playerPos;
        playerPre.SetActive(true);
    }
}
