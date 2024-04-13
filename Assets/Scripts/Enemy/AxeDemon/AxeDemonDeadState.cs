using System.Collections;
using UnityEngine;

public class AxeDemonDeadState : EnemyState
{
    private Enemy_AxeDemon enemy;
    public AxeDemonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_AxeDemon _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
