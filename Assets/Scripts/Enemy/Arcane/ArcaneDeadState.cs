using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneDeadState : EnemyState
{
    private Enemy_Arcane enemy;

    public ArcaneDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Arcane _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.SetBool(enemy.lastAnimBoolName, true);
        enemy.anim.speed = 0;
        enemy.cd.enabled = false;

        stateTimer = 0.1f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 10);
        }
    }
}
