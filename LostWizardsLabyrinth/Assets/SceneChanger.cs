using UnityEngine;
using UnityEngine.SceneManagement; // Needed to change scenes

public class SceneChanger : MonoBehaviour
{
    private bool playerInZone = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the zone
        if (other.CompareTag("Player")) // Ensure your player object has the "Player" tag
        {
            playerInZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player leaves the zone
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    void Update()
    {
        // Check if the player is in the zone and presses the F key
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Minesweeper"); // Replace with your scene's name
        }
    }
}

