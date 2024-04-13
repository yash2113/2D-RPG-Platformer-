using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin : Enemy
{
    #region States
    public GoblinIdleState idleState { get; private set; }
    public GoblinMoveState moveState { get; private set; }
    public GoblinBattleState battleState { get; private set; }
    public GoblinAttackState attackState { get; private set; }
    public GoblinStunnedState stunnedState { get; private set; }
    public GoblinDeadState deadState { get; private set; }
    #endregion

    public int currencyStole = 20;
    protected override void Awake()
    {
        base.Awake();

        idleState = new GoblinIdleState(this, stateMachine, "Idle", this);
        moveState = new GoblinMoveState(this, stateMachine, "Move", this);
        battleState = new GoblinBattleState(this, stateMachine, "Move", this);
        attackState = new GoblinAttackState(this, stateMachine, "Attack", this);
        stunnedState = new GoblinStunnedState(this, stateMachine, "Stunned", this);
        deadState = new GoblinDeadState(this, stateMachine, "Idle", this);
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
