using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    protected EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        Debug.Log($"I Entered {stateName}");
    }

    public virtual void Update()
    {
        Debug.Log($"I Updated {stateName}");
    }

    public virtual void Exit()
    {
        Debug.Log($"I Exited {stateName}");
    }
}
