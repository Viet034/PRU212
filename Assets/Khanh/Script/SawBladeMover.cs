using UnityEngine;

public class SawBladeMover : MonoBehaviour
{
    public float speed = 5f;
    public Transform endPoint;
    public bool destroyAtEnd = true;

    void Update()
    {
        transform.Rotate(0f, 0f, 360f * Time.deltaTime);

        if (endPoint == null) return;

        // Di chuyển về phía endPoint
        transform.position = Vector2.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

        // Nếu đã đến điểm đích
        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            if (destroyAtEnd)
                Destroy(gameObject); // Biến mất
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit by sawblade!");

            // Gọi method Die() từ script PlayerController nếu có
            PlayerControllerr player = collision.GetComponent<PlayerControllerr>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
