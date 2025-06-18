using UnityEngine;

public class FakePlatform : MonoBehaviour
{
    public float delayBeforeDisappear = 0.5f;
    private bool triggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            Invoke("Disappear", delayBeforeDisappear);
        }
    }

    void Disappear()
    {
        gameObject.SetActive(false);
        // Có thể thêm hiệu ứng/âm thanh ở đây
    }
}
