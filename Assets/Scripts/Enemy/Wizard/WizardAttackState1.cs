using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttackState1 : EnemyState
{
    private Enemy_Wizard enemy;
    
    public WizardAttackState1(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wizard _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
        
    }

    public override void Enter()
    {
        base.Enter();

        int randomAttackAudio = GenerateRandomNumber(12, 47);
        Debug.Log("random attack is " + randomAttackAudio);

        AudioManager.instance.PlaySFX(randomAttackAudio, enemy.transform);
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

    private int GenerateRandomNumber(int x, int y)
    {
        int randomHelper = Random.Range(0, 2);

        if(randomHelper == 0)
        {
            return x;
        }
        else
        {
            return y;
        }

    }
}
