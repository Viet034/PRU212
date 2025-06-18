using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 15f; // Lực bật có thể chỉnh trong Unity
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Dùng AddForce để không bị ghi đè bởi PlayerController
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0f); // Reset trước khi AddForce
                playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

                animator.SetTrigger("Bounce");
            }
        }
    }

}

