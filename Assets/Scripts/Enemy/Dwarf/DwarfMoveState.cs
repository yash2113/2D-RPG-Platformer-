using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfMoveState : DwarfGroundedState
{
    public DwarfMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Dwarf _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(95, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(95);
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
