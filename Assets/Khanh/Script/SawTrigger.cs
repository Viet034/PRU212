using UnityEngine;

public class SawTrigger : MonoBehaviour
{
    public GameObject sawBlade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && sawBlade != null)
        {
            // Kích hoạt bánh gai (enable và bắt đầu chạy)
            sawBlade.SetActive(true);
        }
    }
}
