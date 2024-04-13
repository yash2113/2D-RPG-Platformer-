using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AxeDemon : Enemy
{
    #region States
    public AxeDemonIdleState idleState { get; private set; }
    public AxeDemonMoveState moveState { get; private set; }
    public AxeDemonBattleState battleState { get; private set; }
    public AxeDemonAttackState attackState1 { get; private set; }
    public AxeDemonAttackState attackState2 { get; private set; }
    public AxeDemonStunnedState stunnedState { get; private set; }
    public AxeDemonDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new AxeDemonIdleState(this, stateMachine, "Idle", this);
        moveState = new AxeDemonMoveState(this, stateMachine, "Move", this);
        battleState = new AxeDemonBattleState(this, stateMachine, "Move", this);
        attackState1 = new AxeDemonAttackState(this, stateMachine, "Attack1", this);
        attackState2 = new AxeDemonAttackState(this, stateMachine, "Attack2", this);
        stunnedState = new AxeDemonStunnedState(this, stateMachine, "Stunned", this);
        deadState = new AxeDemonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
