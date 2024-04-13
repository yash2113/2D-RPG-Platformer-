using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dwarf : Enemy
{
    #region States
    public DwarfIdleState idleState { get; private set; }
    public DwarfMoveState moveState { get; private set; }
    public DwarfBattleState battleState { get; private set; }
    public DwarfAttackState attackState { get; private set; }
    public DwarfStunnedState stunnedState { get; private set; }
    public DwarfDeadState deadState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new DwarfIdleState(this, stateMachine, "Idle", this);
        moveState = new DwarfMoveState(this, stateMachine, "Move", this);
        battleState = new DwarfBattleState(this, stateMachine, "Move", this);
        attackState = new DwarfAttackState(this, stateMachine, "Attack", this);
        stunnedState = new DwarfStunnedState(this, stateMachine, "Stunned", this);
        deadState = new DwarfDeadState(this, stateMachine, "Idle", this);
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
