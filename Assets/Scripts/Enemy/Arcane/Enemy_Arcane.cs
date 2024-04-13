using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Arcane : Enemy
{
    [Header("Archer Specific Info")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private int arrowDamage;

    public Vector2 jumpVelocity;
    public float jumpCooldown;
    public float safeDistance; // How close player should be to trigger jump on battle state
    [HideInInspector] public float lastTimeJumped;

    [Header("Additional Behind Collision Check")]
    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehindCheckSize;

    #region States

    public ArcaneIdleState idleState {  get; private set; }
    public ArcaneMoveState moveState { get; private set; }
    public ArcaneBattleState battleState { get; private set; }
    public ArcaneAttackState attackState { get; private set; }
    public ArcaneDeadState deadState { get; private set; }
    public ArcaneStunnedState stunnedState { get; private set;}
    public ArcaneRollState rollState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new ArcaneIdleState(this, stateMachine, "Idle", this);
        moveState = new ArcaneMoveState(this, stateMachine, "Move", this);
        battleState = new ArcaneBattleState(this, stateMachine, "Idle", this);
        attackState = new ArcaneAttackState(this, stateMachine, "Attack", this);
        deadState = new ArcaneDeadState(this, stateMachine, "Dead", this);
        stunnedState = new ArcaneStunnedState(this, stateMachine, "Stunned", this);
        rollState = new ArcaneRollState(this, stateMachine, "Roll", this);
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
        GameObject newArrow = Instantiate(arrowPrefab, attackCheck.position, Quaternion.identity);
        newArrow.GetComponent<Arrow_Controller>().SetupArrow(arrowSpeed * facingDir, stats);
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
