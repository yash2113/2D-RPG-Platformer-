using UnityEngine;

public class NecroBattleState : EnemyState
{
    private Enemy_Necro enemy;
    private Transform player;
    private int moveDir;

    public NecroBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Necro _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        if(enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if(enemy.IsPlayerDetected().distance < enemy.attackDistance )
            {
                if(enemy.CanDoSpellCast())
                {
                    Debug.Log("Necro is entering from Battle  -> spell Cast ");
                    if(enemy.CanCastSkull())
                    {
                        stateMachine.ChangeState(enemy.skullCastState);
                    }
                    else
                    {
                        stateMachine.ChangeState(enemy.staffShineState);
                    }
                }
                else
                {
                    Debug.Log("Necro is entering from Battle -> Idle ");
                    stateMachine.ChangeState(enemy.idleState);
                }
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

        if(enemy.IsPlayerDetected().distance > enemy.attackDistance )
        {
            enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    


}
