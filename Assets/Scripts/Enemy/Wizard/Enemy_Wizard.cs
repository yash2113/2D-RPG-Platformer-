using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wizard : Enemy
{
    #region State

    public WizardIdleState idleState {  get; private set; }
    public WizardMoveState moveState { get; private set; }
    public WizardBattleState battleState { get; private set; }

    public WizardAttackState1 attackState1 { get; private set; }
    public WizardAttackState1 attackState2 { get; private set; }
    public WizardStunnedState stunnedState { get; private set;}
    public WizardDeadState deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new WizardIdleState(this, stateMachine, "Idle", this);
        moveState = new WizardMoveState(this, stateMachine, "Move", this);
        battleState = new WizardBattleState(this, stateMachine, "Move", this);
        attackState1 = new WizardAttackState1(this, stateMachine, "Attack1", this);
        attackState2 = new WizardAttackState1(this, stateMachine, "Attack2", this);
        stunnedState = new WizardStunnedState(this, stateMachine, "Stunned", this);
        deadState = new WizardDeadState(this, stateMachine, "Idle", this);
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
