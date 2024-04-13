using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cacodaemon : Enemy
{
    #region States
    public CacodaemonIdleState idleState { get; private set; }
    public CacodaemonMoveState moveState { get; private set; }
    public CacodaemonBattleState battleState { get; private set; }
    public CacodaemonAttackState attackState { get; private set; }
    public CacodaemonStunnedState stunnedState { get; private set; }
    public CacodaemonDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new CacodaemonIdleState(this, stateMachine, "Idle", this);
        moveState = new CacodaemonMoveState(this, stateMachine, "Move", this);
        battleState = new CacodaemonBattleState(this, stateMachine, "Move", this);
        attackState = new CacodaemonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new CacodaemonStunnedState(this, stateMachine, "Stunned", this);
        deadState = new CacodaemonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        isEnemyFlying = true;
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
