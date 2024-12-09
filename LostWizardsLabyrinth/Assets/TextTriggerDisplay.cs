using UnityEngine;
using TMPro;

public class TriggerTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI interactText; // Text to display for interaction
    private bool playerInRange = false;

    void Start()
    {
        if (interactText == null)
        {
            Debug.LogError("Interact Text is not assigned!");
        }
        else
        {
            interactText.gameObject.SetActive(false); // Ensure the text is hidden initially
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText?.gameObject.SetActive(true); // Show the interaction text
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText?.gameObject.SetActive(false); // Hide the interaction text
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            StepManager.Instance.AdvanceStep(); // Advance to the next step
            interactText?.gameObject.SetActive(false); // Hide the interaction text
            playerInRange = false; // Reset interaction state
            gameObject.SetActive(false); // Optionally disable this trigger to prevent reuse
        }
    }
}
