using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private StateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public UnityEngine.Vector2 moveInput { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<UnityEngine.Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = UnityEngine.Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();
    }
}
