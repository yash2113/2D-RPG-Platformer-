using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardSoulMoveState : ShardSoulGroundedState
{
    public ShardSoulMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_ShardSoul _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(86, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(86);
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
