using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
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
