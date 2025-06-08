using UnityEngine;

public class FallingTile : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool activated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activated && collision.gameObject.CompareTag("Player"))
        {
            activated = true;
            Invoke("Drop", 0.4f);  // delay sụp xuống
        }
    }

    void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
