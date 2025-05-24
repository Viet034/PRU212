using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundAndWall;
    //[SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    //[SerializeField] private LayerMask wall;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 0.5f;

    private Animator animator; 
    private Rigidbody2D rb;
    private Move movementa;
    private Vector2 moveInput;
    private Vector3 originalScale;
    
    private bool isWallJumping;
    private float wallJumpTimer = 0.2f;
    private float walljumpCounter;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Movement();


    }

    private void Movement()
    {
        movementa = new Move();
        movementa.Enable();
        originalScale = transform.localScale;
      
        //Cách đăng ký mới của Entity
        movementa.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        movementa.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        movementa.Player.Jump.performed += ctx => Jump();
    }
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //UpdateAnimation();
        //WallSlide();
        if(isWallJumping)
        {
            walljumpCounter -= Time.deltaTime;
            if(walljumpCounter <= 0)
            {
                isWallJumping = false;
            }
        }
    }

    private void HandleMovement()
    {
        if (isWallJumping) return;

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        if(moveInput.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x),originalScale.y, originalScale.z);
        }else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundAndWall);
    }
    private bool isWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, groundAndWall);
    }
    //private int wallDirect()
    //{
    //    bool right = Physics2D.OverlapCircle(wallCheck.position + Vector3.right * 0.1f, 0.05f, groundAndWall);
        
    //    bool left = Physics2D.OverlapCircle(wallCheck.position + Vector3.left * 0.1f, 0.05f, groundAndWall);
    //    if (right) return 1;
    //    if (left) return -1;
    //    return 0;
    //}
    private void Jump()
    {
        
        if (isGrounded()) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        //else if (isWall() && !isGrounded() && !isWallJumping)
        //{
        //    //float wallJumpingDirection = transform.localScale.x > 0 ? -1 : 1;
        //    //if ((wallJumpingDirection == -1 && moveInput.x < 0) || (wallJumpingDirection == 1 && moveInput.x > 0))
        //    //{
        //    //    rb.linearVelocity = new Vector2(wallJumpingDirection * moveSpeed, jumpForce);
        //    //    transform.localScale = new Vector3(Mathf.Sign(wallJumpingDirection) * Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        //    //}
        //    //if (wallDirc != 0 && ((wallDirc == 1 && moveInput.x < -0.1f) || (wallDirc == -1 && moveInput.x > 0.1f)))
        //    //{
        //    //    float wallJumpX = 8f;
        //    //    float wallJumpY = 15f;
        //    //    rb.linearVelocity = new Vector2(-wallDirc * wallJumpX, wallJumpY);
        //    //    isWallJumping = true;
        //    //    Invoke(nameof(StopWallJumping), 0.2f);
        //    //    transform.localScale = new Vector3(Mathf.Sign(-wallDirc) * Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        //    //}
        //    if(Mathf.Abs(moveInput.x) > 0.1f){
        //        int wallDirc = wallDirect();
        //        if((wallDirc == 1 && moveInput.x < 0)|| (wallDirc == -1 && moveInput.x > 0))
        //        {
        //            return;
        //        }
        //        isWallJumping = true;
        //        walljumpCounter = wallJumpTimer;

                
        //        rb.linearVelocity = new Vector2(-wallDirc * moveSpeed, jumpForce);
        //        transform.localScale = new Vector3(Mathf.Sign(-wallDirc) * Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        //    }
        //}

        
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }
    //private void UpdateAnimation()
    //{
    //    bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
    //    bool isJumping = !isGrounded();
    //    bool isSliding = !isGrounded() && isWall() && rb.linearVelocity.y < 0;
    //    animator.SetBool("isRunning", isRunning);
    //    animator.SetBool("isJumping", isJumping);
    //    animator.SetBool("isSliding", isSliding);
    //}
    //private void WallSlide()
    //{
    //    if(!isGrounded() && isWall() && rb.linearVelocity.y < 0 && !isWallJumping)
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -1f);
    //    }
    //    int wallDir = wallDirect();
    //    if(wallDir == 1)
    //    {
    //        transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    //    }else if( wallDir == -1)
    //    {
    //        transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    //    }
    //}

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Player died!");
    }
}
