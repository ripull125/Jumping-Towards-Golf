using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // set this so it changes camera position
    }

    public void OpenLevelSelect()
    {
        // set this so it changes camera position to level select position
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
        Application.Quit();
        #endif
    }
}
