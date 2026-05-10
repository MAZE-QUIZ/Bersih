using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (QuizManager.Instance != null && QuizManager.Instance.IsQuizActive)
        {
            return;
        }
        
        ReadMovementInput();
    }

    private void FixedUpdate()
    {
                if (QuizManager.Instance != null && QuizManager.Instance.IsQuizActive)
        {
            moveInput = Vector2.zero;
            return;
        }
        
        MovePlayer();

    }

    private void ReadMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;
        moveVelocity = moveInput * moveSpeed;
    }

    private void MovePlayer()
    {
        rb.linearVelocity = moveVelocity;
    }
}