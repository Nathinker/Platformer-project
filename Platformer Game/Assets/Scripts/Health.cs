using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] TextMeshProUGUI printer;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject playerPre;
    Vector2 playerPos;
    Vector2 tempPos;
    public Transform p_RespawnPoint;
    public Transform p_TempPoint;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        printer.text = "Health: " + health;
        playerPos.Set(p_RespawnPoint.position.x, p_RespawnPoint.position.y);
        tempPos.Set(p_TempPoint.position.x, p_TempPoint.position.y);
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
