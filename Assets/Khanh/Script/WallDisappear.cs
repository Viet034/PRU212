using UnityEngine;

public class WallDisappear : MonoBehaviour
{
    public GameObject wallToHide; // Kéo Wall vào đây từ Editor
    private int triggerCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggerCount++;
            Debug.Log("Player passed through: " + triggerCount + " times");

            if (triggerCount >= 2 && wallToHide != null)
            {
                wallToHide.SetActive(false); // Ẩn tường
            }
        }
    }
}
