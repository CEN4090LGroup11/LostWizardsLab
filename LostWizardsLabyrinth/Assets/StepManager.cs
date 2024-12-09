using UnityEngine;

public class StepManager : MonoBehaviour
{
    public static StepManager Instance; // Singleton instance

    public int currentStep = 1; // Tracks the player's current step

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This keeps the StepManager between scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsStep(int step)
    {
        return currentStep == step;
    }

    public void AdvanceStep()
    {
        currentStep++;
        Debug.Log($"Game advanced to step {currentStep}");
    }

    public void SetStep(int step)
    {
        currentStep = step;
        Debug.Log($"Game step set to {currentStep}");
    }
}
