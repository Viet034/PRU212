using UnityEngine;

public class FallingWallTrigger : MonoBehaviour
{
    public Rigidbody2D wallRigidbody;  // Kéo RigidBody2D của tường vào đây
    private bool hasFallen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFallen && collision.CompareTag("Player"))
        {
            wallRigidbody.gravityScale = 3f;  // Làm cho tường rơi
            hasFallen = true;
        }
    }
}
