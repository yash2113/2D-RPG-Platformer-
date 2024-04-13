using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ShardSoul : Enemy
{
    #region States
    public ShardSoulIdleState idleState { get; private set; }
    public ShardSoulMoveState moveState { get; private set; }
    public ShardSoulBattleState battleState { get; private set; }
    public ShardSoulAttackState attackState { get; private set; }
    public ShardSoulStunnedState stunnedState { get; private set; }
    public ShardSoulDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new ShardSoulIdleState(this, stateMachine, "Idle", this);
        moveState = new ShardSoulMoveState(this, stateMachine, "Move", this);
        battleState = new ShardSoulBattleState(this, stateMachine, "Move", this);
        attackState = new ShardSoulAttackState(this, stateMachine, "Attack", this);
        stunnedState = new ShardSoulStunnedState(this, stateMachine, "Stunned", this);
        deadState = new ShardSoulDeadState(this, stateMachine, "Dead", this);
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
