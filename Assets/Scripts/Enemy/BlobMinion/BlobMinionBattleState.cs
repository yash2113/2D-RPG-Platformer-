using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMinionBattleState : EnemyState
{
    private Enemy_BlobMinion enemy;
    private Transform player;
    private int moveDir;
    public BlobMinionBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BlobMinion _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    
    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(83, enemy.transform);

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
        {
            Debug.Log("State Change Battle -> Move");
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
                    Debug.Log("State Change Battle -> Attack");
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
            {
                Debug.Log("State Change Battle -> Idle");
                stateMachine.ChangeState(enemy.idleState);
            }
        }



        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(83);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

}
