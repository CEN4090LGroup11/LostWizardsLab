using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private bool playerInside = false;
    public string sceneName;
    private Transform player; // Reference to the player

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has the tag "Player"
        {
            playerInside = true;
            player = other.transform; // Store the reference to the player
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            player = null; // Reset the player reference when they exit the trigger
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            // Ensure the player reference is not null before saving
            if (player != null)
            {
                // Save the player's position
                PlayerPrefs.SetFloat("LastPlayerPosX", player.position.x);
                PlayerPrefs.SetFloat("LastPlayerPosY", player.position.y);
                PlayerPrefs.SetFloat("LastPlayerPosZ", player.position.z);

                // Save the player position data
                PlayerPrefs.Save();

                // Switch to the specified scene
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}

