using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerr : MonoBehaviour
{
    //[Header("Movement Settings")]
    //public float moveSpeed = 5f;
    //public float jumpForce = 5f;

    //[Header("Ground Check Settings")]
    //public Transform groundCheck;
    //public float groundCheckRadius = 0.1f;
    //public LayerMask groundLayer;

    //[Header("Checkpoint Settings")]
    //public Vector2 checkpointPosition;

    //private Rigidbody2D rb;
    //private bool isGrounded;
    //private bool isDead = false;

    //// Sự kiện thông báo khi nhân vật chết
    //public static event Action OnPlayerDeath;

    //void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    checkpointPosition = transform.position; // Đặt checkpoint ban đầu
    //}

    //void Update()
    //{
    //    // Kiểm tra chạm đất
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
    //    isGrounded = false;
    //    foreach (var collider in colliders)
    //    {
    //        if (collider.gameObject != gameObject)
    //        {
    //            isGrounded = true;
    //            break;
    //        }
    //    }

    //    if (!isDead)
    //    {
    //        HandleMovement();

    //    }
    //}

    //void HandleMovement()
    //{
    //    if (Keyboard.current == null)
    //        return;

    //    float moveInput = 0f;
    //    if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
    //    {
    //        moveInput = -1f;
    //    }
    //    else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
    //    {
    //        moveInput = 1f;
    //    }

    //    rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    //}

    //public void OnJump(InputValue value)
    //{
    //    if (value.isPressed && isGrounded)  // Chỉ nhảy khi chạm đất
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    //        isGrounded = false; // Đặt false ngay sau khi nhảy
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Trap") || other.CompareTag("Lava"))
    //    {
    //        Die();
    //    }
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Trap"))
    //    {
    //        Die();
    //    }
    //}

    //void Die()
    //{
    //    if (!isDead)
    //    {
    //        isDead = true;
    //        Debug.Log("Player died!");
    //        OnPlayerDeath?.Invoke(); // Kích hoạt sự kiện để thông báo cho bẫy
    //        transform.position = checkpointPosition;
    //        rb.linearVelocity = Vector2.zero;
    //        isDead = false;
    //    }
    //}

    //public void SetCheckpoint(Vector2 newCheckpoint)
    //{
    //    checkpointPosition = newCheckpoint;
    //    Debug.Log("Checkpoint updated: " + newCheckpoint);
    //}

    //void OnDrawGizmosSelected()
    //{
    //    if (groundCheck != null)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}
    public static event Action OnPlayerDeath; // Sự kiện nếu bạn muốn thông báo ra ngoài

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isDead = false;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float maxJumpVelocity = 12f;
    public Vector2 checkpointPosition;
    private bool isGrounded = false;  // Thêm biến kiểm tra chạm đất

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position; // Đặt checkpoint ban đầu
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.y > maxJumpVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxJumpVelocity);
        }

    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)  // Chỉ nhảy khi chạm đất
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // Đặt false ngay sau khi nhảy
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
        //if (collision.gameObject.CompareTag("Exit"))
        //{
        //    Destroy(gameObject);
        //}

        // Nếu chạm đất thì bật isGrounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap") || other.CompareTag("Lava"))
        {
            Die();
        }
    }
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Player died!");
            OnPlayerDeath?.Invoke(); // Nếu bạn có trap muốn nhận sự kiện này
            transform.position = checkpointPosition; // Hồi sinh tại checkpoint
            rb.linearVelocity = Vector2.zero; // Dừng chuyển động
            isDead = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
