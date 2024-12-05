using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private bool playerInside = false;
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has the tag "Player"
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            // Save the player's position
            PlayerPrefs.SetFloat("LastPlayerPosX", transform.position.x);
            PlayerPrefs.SetFloat("LastPlayerPosY", transform.position.y);
            PlayerPrefs.SetFloat("LastPlayerPosZ", transform.position.z);
            
            // Switch to Scene 2
            SceneManager.LoadScene(sceneName);
        }
    }
}
