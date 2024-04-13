using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfIdleState : SkullWolfGroundedState
{
    public SkullWolfIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_SkullWolf _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(68, enemy.transform);

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
