using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlyingEye : Enemy
{
    #region States

    public FlyingEyeIdleState idleState {  get; private set; }
    public FlyingEyeMoveState moveState { get; private set; }
    public FlyingEyeAttackState attackState { get; private set; }
    public FlyingEyeBattleState battleState { get; private set; }
    public FlyingEyeStunnedState stunnedState { get; private set;}
    public FlyingEyeDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new FlyingEyeIdleState(this, stateMachine, "Idle", this);
        moveState = new FlyingEyeMoveState(this, stateMachine, "Move", this);
        attackState = new FlyingEyeAttackState(this, stateMachine, "Attack", this);
        battleState = new FlyingEyeBattleState(this, stateMachine, "Move", this);
        stunnedState = new FlyingEyeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new FlyingEyeDeadState(this, stateMachine, "Dead", this);
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
