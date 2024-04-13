using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroAttackState : EnemyState
{
    private Enemy_Necro enemy;

    public NecroAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necro _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    
}
