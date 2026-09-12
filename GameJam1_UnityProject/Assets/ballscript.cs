using UnityEngine;
using UnityEngine.InputSystem;

public class ballscript : MonoBehaviour
{
    [SerializeField] private float speedModifier;
    private PlayerInputActions playerInputActions;

    private Rigidbody rb;
    private float shotBuffer = 0.2f;
    private float launchPower = 0;
    private float shotTime = -99f; // Moved to class level to maintain value between frames

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
        
        if (playerInputActions.Player.Attack.IsPressed())
        {
            // FIX: Start drag sound immediately when dragging initializes
            SoundFXManager.Instance.PlayDragSound();

            Vector2 mousePosition = playerInputActions.Player.Look.ReadValue<Vector2>();
            Vector2 directionVector = playerScreenPosition - mousePosition;
            Vector3 forward = new Vector3(directionVector.x, 0, directionVector.y);
            transform.forward = forward;
            launchPower = Vector2.Distance(mousePosition, playerScreenPosition);
            
            if (playerInputActions.Player.Attack.WasPressedThisFrame())
            {
                shotTime = Time.time;
            }
        }

        if (playerInputActions.Player.Attack.WasReleasedThisFrame())
        {
            // FIX: Halt the looping sound instantly on mouse release
            SoundFXManager.Instance.StopDragSound();

            SoundFXManager.Instance.PlaySFX(SoundFXManager.Instance.clubHittingBallSound);
            SoundFXManager.Instance.PlaySFX(SoundFXManager.Instance.whooshSound);
            
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
