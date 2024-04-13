using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloomclaspAttackState : EnemyState
{
    private Enemy_Gloomclasp enemy;
    private Animator anim;

    public GloomclaspAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Gloomclasp _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(GenerateRandomFrom(69,70), enemy.transform);
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
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    private int GenerateRandomFrom(int x,int y)
    {
        int randomGenerate = Random.Range(0, 2);

        if(randomGenerate == 0)
        {
            return x;
        }
        else
        {
            return y;
        }
    }

}
