using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gloomclasp : Enemy
{
    #region States
    public GloomclaspIdleState idleState { get; private set; }
    public GloomclaspMoveState moveState { get; private set; }
    public GloomclaspBattleState battleState { get; private set; }
    public GloomclaspAttackState attackState { get; private set; }
    public GloomclaspStunnedState stunnedState { get; private set; }
    public GloomclaspDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new GloomclaspIdleState(this, stateMachine, "Idle", this);
        moveState = new GloomclaspMoveState(this, stateMachine, "Move", this);
        battleState = new GloomclaspBattleState(this, stateMachine, "Move", this);
        attackState = new GloomclaspAttackState(this, stateMachine, "Attack", this);
        stunnedState = new GloomclaspStunnedState(this, stateMachine, "Stunned", this);
        deadState = new GloomclaspDeadState(this, stateMachine, "Idle", this);
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
