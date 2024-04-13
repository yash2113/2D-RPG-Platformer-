using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gladiator : Enemy
{
    #region States
    public GladiatorIdleState idleState { get; private set; }
    public GladiatorMoveState moveState { get; private set; }
    public GladiatorBattleState battleState { get; private set; }
    public GladiatorAttackState attackState { get; private set; }
    public GladiatorStunnedState stunnedState { get; private set; }
    public GladiatorDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new GladiatorIdleState(this, stateMachine, "Idle", this);
        moveState = new GladiatorMoveState(this, stateMachine, "Move", this);
        battleState = new GladiatorBattleState(this, stateMachine, "Move", this);
        attackState = new GladiatorAttackState(this, stateMachine, "Attack", this);
        stunnedState = new GladiatorStunnedState(this, stateMachine, "Stunned", this);
        deadState = new GladiatorDeadState(this, stateMachine, "Dead", this);
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
