using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [Range(0, 10)]
    public float smoothFactor;
    private Vector2 offset;
    public Vector2 minValue, maxValue;
    Vector3 playerPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPosition = player.position;
        transform.position = playerPosition;
        Vector2 boundPos = new Vector2(
        Mathf.Clamp(playerPosition.x, minValue.x, maxValue.x),
        Mathf.Clamp(playerPosition.y, minValue.y, maxValue.y));
        transform.position = Vector2.Lerp(transform.position, boundPos,
        smoothFactor);
    }
}
