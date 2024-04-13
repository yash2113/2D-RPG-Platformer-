using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrIdleState : EnemyState
{
    private Enemy_Necro enemy;
    private Transform player;

    public NecrIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necro _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Vector2.Distance(player.position, enemy.transform.position) > 10)
        {
            enemy.bossFightBegun = true;
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Necro is entering from Idle -> teleport ");
            stateMachine.ChangeState(enemy.teleportState);
        }

        if(stateTimer < 0 && enemy.bossFightBegun)
        {
            stateMachine.ChangeState(enemy.battleState); 
        }

    }


}
