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
    //Continues to the next level
    public void Continue()
    {
        SceneManager.LoadScene("LevelTwo");
    }
    #endregion
}
