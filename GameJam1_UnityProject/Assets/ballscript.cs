using UnityEngine;
using UnityEngine.InputSystem;

public class ballscript : MonoBehaviour
{
    [SerializeField] private float speedModifier;
    private PlayerInputActions playerInputActions;

    private Rigidbody rb;
    private float shotBuffer = 0.2f;
    private float launchPower = 0;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();

    }

    void Update()
    {
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        float shotTime = -99f;
        if (playerInputActions.Player.Attack.IsPressed())
        {
            Vector2 mousePosition = playerInputActions.Player.Look.ReadValue<Vector2>();
            Vector2 directionVector = playerScreenPosition - mousePosition;
            Vector3 forward = new Vector3(directionVector.x, 0, directionVector.y);
            transform.forward = forward;
            launchPower = Vector2.Distance(mousePosition, playerScreenPosition);
            shotTime = Time.time;
        }

        if (playerInputActions.Player.Attack.WasReleasedThisFrame())
        {
            if (Time.time - shotTime > shotBuffer)
            {
                Launch(launchPower);
            }
        }

    }

    private void Launch(float power)
    {
        rb.linearVelocity = transform.forward * power * speedModifier;
        Debug.Log("Launch");
    }
}