using UnityEngine;

public class PlayerBasicAttackState : EntityState
{

    private float attackVelTimer;
    private float lastTimeAttacked;

    private const int ComboStartingIndex = 1;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private int attackDir;

    private bool comboAttackQueued;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.Log("Adjusted combo limit to match attack velocity array length");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndex();

        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

        anim.SetInteger("comboIndex", comboIndex);
        ApplyAttackVel();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVel();

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
            HandleStateExit();
        }
    }

    public override void Exit()
    {
        lastTimeAttacked = Time.time;
        base.Exit();
        comboIndex++;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }

    private void HandleAttackVel()
    {
        attackVelTimer -= Time.deltaTime;
        if (attackVelTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }

    }

    private void ApplyAttackVel()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];

        attackVelTimer = player.attackVelDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }

    private void ResetComboIndex()
    {
        if (Time.time > lastTimeAttacked + player.comboAttackWindow || comboIndex > comboLimit)
        {
            comboIndex = ComboStartingIndex;
        }
    }
}
