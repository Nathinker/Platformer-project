using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Menu
public class Menu : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject playerPre;
    #endregion

    #region ReloadLevel
    // Reloads the Level when this function is called
    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
#endregion
