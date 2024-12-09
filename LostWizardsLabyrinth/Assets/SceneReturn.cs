using UnityEngine;

public class SceneReturn : MonoBehaviour
{
    public Transform player; // Reference to the player object (Drag the player in the Inspector)

    private Vector3 startingPosition = new Vector3(812f, 0f, 772.1f); // Default starting position

    void Start()
    {
        // In both the Unity Editor and builds, check if saved position data exists
        if (PlayerPrefs.HasKey("LastPlayerPosX") && PlayerPrefs.HasKey("LastPlayerPosY") && PlayerPrefs.HasKey("LastPlayerPosZ"))
        {
            float savedX = PlayerPrefs.GetFloat("LastPlayerPosX", 0f);
            float savedY = PlayerPrefs.GetFloat("LastPlayerPosY", 1f);
            float savedZ = PlayerPrefs.GetFloat("LastPlayerPosZ", 0f);

            // Ensure Y value is not below ground level
            Vector3 savedPosition = new Vector3(savedX, Mathf.Max(savedY, 1f), savedZ);

            SetPlayerPosition(savedPosition);
        }
        else
        {
            // No saved data, start at default spawn point
            SetPlayerPosition(startingPosition);
        }
    }

    private void SetPlayerPosition(Vector3 position)
    {
        // Check if player has a Rigidbody
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Use MovePosition for physics-based movement
            rb.MovePosition(position);
        }
        else
        {
            // Set position directly if no Rigidbody
            player.position = position;
        }
    }
}
