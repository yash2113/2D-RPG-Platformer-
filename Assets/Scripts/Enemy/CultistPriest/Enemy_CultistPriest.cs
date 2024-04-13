using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CultistPriest : Enemy
{
    #region States
    public CultistPriestIdleState idleState { get; private set; }
    public CultistPriestMoveState moveState { get; private set; }
    public CultistPriestBattleState battleState { get; private set; }
    public CultistPriestAttackState attackState { get; private set; }
    public CultistPriestStunnedState stunnedState { get; private set; }
    public CultistPriestDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new CultistPriestIdleState(this, stateMachine, "Idle", this);
        moveState = new CultistPriestMoveState(this, stateMachine, "Move", this);
        battleState = new CultistPriestBattleState(this, stateMachine, "Move", this);
        attackState = new CultistPriestAttackState(this, stateMachine, "Attack", this);
        stunnedState = new CultistPriestStunnedState(this, stateMachine, "Stunned", this);
        deadState = new CultistPriestDeadState(this, stateMachine, "Idle", this);
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
