using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject playerPre;
    #endregion

    #region ReloadLevel
    // Reloads the Level when this function is called
    public void ReloadLevel()
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = originalTimeScale;
    }
    #endregion

    #region Exit
    // Exits the Game when this function is called
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
    #endregion

    #region Next Level
    // Continues to the next level
    public void Continue()
    {
        playerPre.GetComponent<PlayerMovement>().paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelTwo");
    }
    #endregion

    #region Start Game
    // Starts the first level
    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }
    #endregion

    #region Return to Main Menu
    // Returns to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
    #endregion
}
