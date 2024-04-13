using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkullWolf : Enemy
{
    #region States
    public SkullWolfIdleState idleState { get; private set; }
    public SkullWolfMoveState moveState { get; private set; }
    public SkullWolfBattleState battleState { get; private set; }
    public SkullWolfAttackState attackState { get; private set; }
    public SkullWolfStunnedState stunnedState { get; private set; }
    public SkullWolfDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new SkullWolfIdleState(this, stateMachine, "Idle", this);
        moveState = new SkullWolfMoveState(this, stateMachine, "Move", this);
        battleState = new SkullWolfBattleState(this, stateMachine, "Move", this);
        attackState = new SkullWolfAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkullWolfStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SkullWolfDeadState(this, stateMachine, "Dead", this);
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
