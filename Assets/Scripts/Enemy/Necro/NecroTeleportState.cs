using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroTeleportState : EnemyState
{
    private Enemy_Necro enemy;
    public NecroTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necro _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.stats.MakeInvincible(true);
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
        {
            if(enemy.CanDoSpellCast() && enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                Debug.Log("Necro is entering from Teleport -> spell Cast ");
                if(enemy.CanCastSkull() )
                {
                    stateMachine.ChangeState(enemy.skullCastState);
                }
                else
                {
                    stateMachine.ChangeState(enemy.staffShineState);
                }
            }
            else
            {
                Debug.Log("Necro is entering from Teleport -> battle ");
                stateMachine.ChangeState(enemy.battleState);
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.MakeInvincible(false);
    }

}
