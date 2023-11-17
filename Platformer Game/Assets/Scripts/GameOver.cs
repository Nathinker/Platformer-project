using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject playerPre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPre.GetComponent<Health>().isDead == true)
        {
            gameOverPanel.SetActive(true);
        }
        else if (playerPre.GetComponent<Health>().isDead == false)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
    }
}
