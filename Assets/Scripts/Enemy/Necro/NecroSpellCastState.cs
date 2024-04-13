using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroSpellCastState : EnemyState
{
    private Enemy_Necro enemy;
    private float spellCooldown;
    private float spellTimer;

    public NecroSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necro _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();


    }

}
