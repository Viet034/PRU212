using UnityEngine;

public class DieZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug log để kiểm tra
            Debug.Log("Player đã chạm vào DieZone!");

            // Gọi hàm chết từ Player (nếu có)
            PlayerControllerr player = collision.GetComponent<PlayerControllerr>();
            if (player != null)
            {
                player.Die(); // giả sử bạn có hàm Die trong PlayerController
            }
            else
            {
                // Hoặc huỷ Player nếu không có PlayerController
                Destroy(collision.gameObject);
            }
        }
    }
}
