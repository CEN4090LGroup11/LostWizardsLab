using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject goalFX;

    
    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 1f;

    private bool isDragging;
    private bool inHole;

    private void Update() {
        PlayerInput();
        if (LevelManager.main.outOfStrokes && rb.velocity.magnitude <= 0.2f && !LevelManager.main.levelCompleted) {
            LevelManager.main.LevelFailed();
        }
    }

    private bool IsReady() {
        return rb.velocity.magnitude <= 0.2f;
    }

    private void PlayerInput() {
        if (!IsReady()) return;
        // Get the position of the mouse in the world space
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        // Handle mouse input for dragging
        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) {
            DragStart();
        }
        if (Input.GetMouseButton(0) && isDragging) {
            DragChange(inputPos); // Pass inputPos here
        }
        if (Input.GetMouseButtonUp(0) && isDragging) {
            DragRelease(inputPos); // Pass the position to DragRelease
        }
    }

    private void DragStart() {
        isDragging = true;
    }

    private void DragChange(Vector2 pos) {
        Vector2 dir = ((Vector2)transform.position - pos);
    }

    private void DragRelease(Vector2 pos) {
        // Calculate the distance between the ball's current position and the release position
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;

        if (distance < 1f) {
            return;
        }

        LevelManager.main.IncreaseStroke();

        Vector2 dir = (Vector2)transform.position - pos;
        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Goal") CheckWinState();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Goal") CheckWinState();
    }

    private void CheckWinState() {
        if (inHole) return;

        if (rb.velocity.magnitude <= maxGoalSpeed) {
            inHole = true;
        }

        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);

        GameObject fx = Instantiate(goalFX, transform.position, Quaternion.identity);
        Destroy(fx, 2f);

        LevelManager.main.LevelComplete();
    }
}

