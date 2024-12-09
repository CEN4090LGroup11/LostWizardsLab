using UnityEngine;

public class StepBlocker : MonoBehaviour
{
    public int requiredStep; // The step required to disable access to this area
    private BoxCollider blockerCollider;

    void Start()
    {
        // Check if a StepBlocker for this requiredStep already exists in the scene
        StepBlocker[] existingBlockers = FindObjectsOfType<StepBlocker>();

        // Loop through the existing blockers to check if one already exists with the same requiredStep
        foreach (var blocker in existingBlockers)
        {
            if (blocker.requiredStep == requiredStep && blocker != this)
            {
                // Destroy this object if another StepBlocker for the same step already exists
                Destroy(gameObject);
                return;
            }
        }

        // Get the BoxCollider attached to this GameObject
        blockerCollider = GetComponent<BoxCollider>();

        if (blockerCollider == null)
        {
            Debug.LogError("No BoxCollider attached to this object!");
        }
        else
        {
            // Initially, keep the BoxCollider enabled to block access
            blockerCollider.enabled = true;
        }

        // Use DontDestroyOnLoad to keep this GameObject across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Ensure the StepManager is accessible and active
        if (StepManager.Instance != null)
        {
            // Disable the collider when the current step matches the required step
            if (StepManager.Instance.IsStep(requiredStep))
            {
                blockerCollider.enabled = false; // Disable the BoxCollider
            }
        }
    }
}
