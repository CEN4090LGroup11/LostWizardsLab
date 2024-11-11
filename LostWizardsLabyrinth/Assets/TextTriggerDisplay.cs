using UnityEngine;
using TMPro;

public class TriggerTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI interactText; 
    private bool playerInRange = false;

    void Start()
    {
        
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Interact Text is not assigned!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger area");
            if (interactText != null)
            {
                interactText.gameObject.SetActive(true);
                playerInRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger area");
            if (interactText != null)
            {
                interactText.gameObject.SetActive(false);
                playerInRange = false;
            }
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interacted with the Wizard!");
        }
    }
}
