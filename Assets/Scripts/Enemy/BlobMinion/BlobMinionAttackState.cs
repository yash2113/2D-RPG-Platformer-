using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMinionAttackState : EnemyState
{
    private Enemy_BlobMinion enemy;
    public BlobMinionAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BlobMinion _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(80, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            Debug.Log("State Change Attack -> Battle");
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
