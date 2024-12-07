using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // Log a message (only visible in the Unity Editor)
        Debug.Log("Game is exiting...");
    }
}

