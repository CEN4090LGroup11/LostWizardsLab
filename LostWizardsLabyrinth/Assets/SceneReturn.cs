using UnityEngine;

public class SceneReturn : MonoBehaviour
{
    public Transform player; // Reference to the player object (Drag the player in the Inspector)

    private Vector3 startingPosition = new Vector3(812f, 0f, 772.1f); // Default starting position

    void Start()
    {
        // Check if the game is running in the Editor or Build
        #if UNITY_EDITOR
            // In the editor, always reset position
            PlayerPrefs.DeleteAll();
        #else
            // In a build, reset only if needed
            if (!PlayerPrefs.HasKey("LastPlayerPosX"))
            {
                // Reset to default spawn position
                SetPlayerPosition(startingPosition);
            }
        #endif

        // Continue with your normal start logic
        if (PlayerPrefs.HasKey("LastPlayerPosX") && PlayerPrefs.HasKey("LastPlayerPosY") && PlayerPrefs.HasKey("LastPlayerPosZ"))
        {
            float savedX = PlayerPrefs.GetFloat("LastPlayerPosX", 0f);
            float savedY = PlayerPrefs.GetFloat("LastPlayerPosY", 1f);
            float savedZ = PlayerPrefs.GetFloat("LastPlayerPosZ", 0f);
            Vector3 savedPosition = new Vector3(savedX, Mathf.Max(savedY, 1f), savedZ);
            SetPlayerPosition(savedPosition);
        }
        else
        {
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
