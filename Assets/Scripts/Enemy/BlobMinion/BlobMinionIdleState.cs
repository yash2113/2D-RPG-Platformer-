using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMinionIdleState : BlobMinionGroundedState
{
    public BlobMinionIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BlobMinion _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(82, enemy.transform);

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
            Debug.Log("State Change Idle -> Move");
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
