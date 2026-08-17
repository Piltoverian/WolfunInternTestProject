using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerModule
{
    private PlayerData playerData;
    [SerializeField] InputAction moveAction;
    [SerializeField] Rigidbody rb;
    [SerializeField] private Joystick joystick;
    private Vector2 desiredDirection;
    private bool isDashing = false;
    private void Awake()
    {    
        moveAction.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerData= GetComponent<PlayerData>();
    }

    private Vector2 GetInput()
    {
        Vector2 keyboardInput = moveAction.ReadValue<Vector2>();

        // Đồng bộ hiển thị phím lên Joystick
        if (joystick != null)
        {
            if (keyboardInput != Vector2.zero)
            {
                joystick.SetInput(keyboardInput);
            }
            else if (!joystick.IsPressed)
            {
                joystick.SetInput(Vector2.zero);
            }
        }

        // Ưu tiên phím, nếu không có phím thì đọc Joystick
        if (keyboardInput == Vector2.zero && joystick != null && joystick.Direction != Vector2.zero)
        {
            return joystick.Direction;
        }

        return keyboardInput;
    }

    private void RotatePlayer()
    {
        Vector2 input = GetInput();
        Vector3 targetDirection = new Vector3(input.x, 0, input.y).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            playerData.GetPlayerBaseStat().rotationSpeed * Time.deltaTime
        );
    }

    private void MovePlayer()
    {
        if(!isDashing) 
        rb.linearVelocity = playerData.GetPlayerBaseStat().baseSpeed * transform.forward*GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit;
        else
        rb.linearVelocity = playerData.GetPlayerBaseStat().dashSpeed * transform.forward * GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit;
    }

    private void TreatAndReadMoveAction()
    {
        if (isDashing)
        {
            MovePlayer();
            return;
        }

        if (GetInput() != Vector2.zero)
        {
            RotatePlayer();
            MovePlayer();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public override void UpdateModule()
    {
        TreatAndReadMoveAction();
    }

    public void EnableDashing()
    {
        isDashing = true;
        moveAction.Disable();
    }

    public void DisableDashing()
    {
        isDashing = false;
        moveAction.Enable();
    }
}
