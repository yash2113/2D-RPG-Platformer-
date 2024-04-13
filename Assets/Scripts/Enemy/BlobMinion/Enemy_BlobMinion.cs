using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlobMinion : Enemy
{
    #region States
    public BlobMinionIdleState idleState { get; private set; }
    public BlobMinionMoveState moveState { get; private set; }
    public BlobMinionBattleState battleState { get; private set; }
    public BlobMinionAttackState attackState { get; private set; }
    public BlobMinionStunnedState stunnedState { get; private set; }
    public BlobMinionDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new BlobMinionIdleState(this, stateMachine, "Idle", this);
        moveState = new BlobMinionMoveState(this, stateMachine, "Move", this);
        battleState = new BlobMinionBattleState(this, stateMachine, "Move", this);
        attackState = new BlobMinionAttackState(this, stateMachine, "Attack", this);
        stunnedState = new BlobMinionStunnedState(this, stateMachine, "Stunned", this);
        deadState = new BlobMinionDeadState(this, stateMachine, "Idle", this);
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
