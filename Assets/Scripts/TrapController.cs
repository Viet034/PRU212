using UnityEngine;

public class TrapController : MonoBehaviour
{
    public enum TrapType { DropDown, FlyLeft, FlyRight, None }
    public TrapType trapType = TrapType.None;
    public float trapSpeed = 5f;
    public Transform player;       // Gán Player từ Inspector
    public float activationRange = 0.2f; // Khoảng cách để kích hoạt
    private bool isActivated = false;
    [SerializeField] private GameObject trap;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (trap == null)
            trap = transform.Find("Trap").gameObject;
        rb = trap.GetComponent<Rigidbody2D>();
        trap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated && Vector2.Distance(trap.transform.position, player.position) < activationRange)
        {
            isActivated = true;
            if (trap != null)
                trap.SetActive(true);
            ActiveTrap();
            
        }
    }
    void ActiveTrap()
    {
        switch(trapType){
            case TrapType.DropDown:
                rb.linearVelocity = Vector2.down * trapSpeed;
                break;
            case TrapType.FlyLeft:
                rb.linearVelocity = Vector2.left * trapSpeed;
                break;
            case TrapType.FlyRight:
                rb.linearVelocity = Vector2.right * trapSpeed;
                break;
            case TrapType.None:
                break;
        }
    }
}
