using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SwingSkeleton : Enemy
{

    #region States

    public SwingSkeletonIdleState idleState {  get; private set; }
    public SwingSkeletonMoveState moveState { get; private set; }
    public SwingSkeletonBattleState battleState { get; private set; }
    public SwingSkeletonAttackState attackState { get; private set; }
    public SwingSkeletonStunnedState stunnedState { get; private set;}
    public SwingSkeletonDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SwingSkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SwingSkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SwingSkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SwingSkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SwingSkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SwingSkeletonDeadState(this, stateMachine, "Dead", this);
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
