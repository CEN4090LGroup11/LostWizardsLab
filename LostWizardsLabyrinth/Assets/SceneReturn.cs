using UnityEngine;

public class SceneReturn : MonoBehaviour
{
    public Transform player; // Reference to the player object (Drag the player in the Inspector)

    private Vector3 startingPosition = new Vector3(812f, -5.199997f, 770.1f); // Default starting position

    void Start()
    {
        // Ensure the player object is assigned in the inspector
        if (player == null)
        {
            Debug.LogError("Player object not assigned in SceneReturn script.");
            return;
        }

        // Check if player has saved position in PlayerPrefs, if not, it's the first time
        if (PlayerPrefs.HasKey("LastPlayerPosX") && PlayerPrefs.HasKey("LastPlayerPosY") && PlayerPrefs.HasKey("LastPlayerPosZ"))
        {
            // Retrieve the saved position from PlayerPrefs
            float savedX = PlayerPrefs.GetFloat("LastPlayerPosX", 0f);  // Default to 0 if not set
            float savedY = PlayerPrefs.GetFloat("LastPlayerPosY", 1f);  // Ensure Y is above the ground
            float savedZ = PlayerPrefs.GetFloat("LastPlayerPosZ", 0f);  // Default to 0 if not set

            // Ensure the player position is above the ground and set position
            Vector3 savedPosition = new Vector3(savedX, Mathf.Max(savedY, 1f), savedZ); // Make sure Y is above the ground

            // Set the saved position
            SetPlayerPosition(savedPosition);
        }
        else
        {
            // No saved position found, spawn at default starting position
            SetPlayerPosition(startingPosition);
        }
    }

    private void SetPlayerPosition(Vector3 position)
    {
        // Check if player has a Rigidbody
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // If Rigidbody is present, use MovePosition to respect physics
            rb.MovePosition(position);
        }
        else
        {
            // If no Rigidbody, set position directly (not recommended for physics-based movement)
            player.position = position;
        }
    }
}

/*using UnityEngine;

public class SceneReturn : MonoBehaviour
{
    public Transform player; // Reference to the player object (Drag the player in the Inspector)

    void Start()
    {
        // Ensure the player object is assigned in the inspector
        if (player == null)
        {
            Debug.LogError("Player object not assigned in SceneReturn script.");
            return;
        }

        // Retrieve the saved position from PlayerPrefs
        float savedX = PlayerPrefs.GetFloat("LastPlayerPosX", 0f);  // Default to 0 if not set
        float savedY = PlayerPrefs.GetFloat("LastPlayerPosY", 1f);  // Ensure Y is above the ground
        float savedZ = PlayerPrefs.GetFloat("LastPlayerPosZ", 0f);  // Default to 0 if not set

        // Ensure the player position is above the ground and set position
        Vector3 savedPosition = new Vector3(savedX, Mathf.Max(savedY, 1f), savedZ); // Make sure Y is above the ground

        // Check if player has a Rigidbody
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // If Rigidbody is present, use MovePosition to respect physics
            rb.MovePosition(savedPosition);
        }
        else
        {
            // If no Rigidbody, set position directly (not recommended for physics-based movement)
            player.position = savedPosition;
        }
    }
}
*/