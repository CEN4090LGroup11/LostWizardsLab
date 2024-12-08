using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseCanvas; // Assign the pause Canvas in the Inspector

    public void ShowPauseMenu()
    {
        if (pauseCanvas != null)
        {
            bool isActive = pauseCanvas.activeSelf; // Check current state
            pauseCanvas.SetActive(!isActive);      // Toggle visibility
        }
    }
}
    
