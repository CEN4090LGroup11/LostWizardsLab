using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public void LoadLevel(string LevelName)
    {
        // Check if the target level is MainScene and the current scene is CharPick
        if (LevelName == "MainScene" && SceneManager.GetActiveScene().name == "CharPick")
        {
            ResetPlayerPosition();
        }

        SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Single);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetPlayerPosition()
    {
        PlayerPrefs.DeleteKey("LastPlayerPosX");
        PlayerPrefs.DeleteKey("LastPlayerPosY");
        PlayerPrefs.DeleteKey("LastPlayerPosZ");
        PlayerPrefs.Save();
        Debug.Log("Player position reset for MainScene!");
    }
}
