using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FireWorm : Enemy
{
    [Header("Archer Specific Info")]
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private int fireBallDamage;
    
    /*public Vector2 jumpVelocity;
    public float jumpCooldown;
    public float safeDistance; // How close player should be to trigger jump on battle state
    [HideInInspector] public float lastTimeJumped;*/

    [Header("Additional Behind Collision Check")]
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehindCheckSize;

    #region States

    public FireWormIdleState idleState {  get; private set; }
    public FireWormMoveState moveState { get; private set; }
    public FireWormBattleState battleState { get; private set; }
    public FireWormAttackState attackState { get; private set; }
    public FireWormDeadState deadState { get; private set; }
    public FireWormStunnedState stunnedState { get; private set;}
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new FireWormIdleState(this, stateMachine, "Idle", this);
        moveState = new FireWormMoveState(this, stateMachine, "Move", this);
        battleState = new FireWormBattleState(this, stateMachine, "Idle", this);
        attackState = new FireWormAttackState(this, stateMachine, "Attack", this);
        deadState = new FireWormDeadState(this, stateMachine, "Move", this);
        stunnedState = new FireWormStunnedState(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newFireVall = Instantiate(fireBallPrefab, attackCheck.position, Quaternion.identity);
        newFireVall.GetComponent<FireBall_Controller>().SetupFireBall(fireBallSpeed * facingDir, stats);
    }

    public bool GroundBehind()
    {
        return Physics2D.BoxCast(groundBehindCheck.position, groundBehindCheckSize, 0, Vector2.zero, 0, whatIsGround);
    }

    public bool WallBehind()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDir, wallCheckDistance + 2, whatIsGround);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireCube(groundBehindCheck.position, groundBehindCheckSize);
    }
}
