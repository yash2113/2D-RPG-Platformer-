using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloomclaspBattleState : EnemyState
{
    private Enemy_Gloomclasp enemy;
    private Transform player;
    private int moveDir;
    private Animator anim;
    public GloomclaspBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Gloomclasp _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        anim = enemy.GetComponentInChildren<Animator>();

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {

                    float randomNumber = ChooseRandomValue();

                    Debug.Log("Attack Number is " + randomNumber);

                    anim.SetFloat("AttackNumber", randomNumber);

                    stateMachine.ChangeState(enemy.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }


        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        Debug.Log("Attack Cooldown");
        return false;
    }

    private float ChooseRandomValue()
    {
        float randomFloat = Random.Range(0f, 1.5f);
        if (randomFloat < 0.5f)
        {
            return 0f;
        }
        else if (randomFloat < 1f)
        {
            return 0.5f;
        }
        else
        {
            return 1f;
        }
    }
}
