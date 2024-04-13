using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMinionMoveState : BlobMinionGroundedState
{
    public BlobMinionMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BlobMinion _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(83, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(83);
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            Debug.Log("State Change Move -> Idle");
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
