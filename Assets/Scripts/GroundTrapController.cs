using UnityEngine;

public class GroundTrapController : MonoBehaviour
{
    public enum GroundTrapType { Disappear, MoveLeft, MoveRight, MoveUp, None }
    public GroundTrapType trapType = GroundTrapType.None;

    public float trapSpeed = 5f;
    private bool isActivated = false;
    private Rigidbody2D rb;

    [SerializeField] private Collider2D trapCollider; // ← gán collider nằm ở con

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (trapType == GroundTrapType.Disappear && trapCollider != null)
        {
            trapCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated) return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player đã vào trigger!");
            isActivated = true;
            ActivateGroundTrap();
        }
    }

    void ActivateGroundTrap()
    {
        switch (trapType)
        {
            case GroundTrapType.Disappear:
                if (trapCollider != null)
                    Destroy(trapCollider.gameObject);
                break;

            case GroundTrapType.MoveLeft:
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.left * trapSpeed;
                break;

            case GroundTrapType.MoveRight:
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.right * trapSpeed;
                break;

            case GroundTrapType.MoveUp:
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.up * trapSpeed;
                break;

            case GroundTrapType.None:
                break;
        }
    }
}
