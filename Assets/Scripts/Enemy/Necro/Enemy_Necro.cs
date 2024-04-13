using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Necro : Enemy
{
    #region States

    public NecrIdleState idleState {  get; private set; }
    public NecroBattleState battleState { get; private set; }
    public NecroAttackState attackState { get; private set; }
    public NecroDeadState deadState { get; private set; }
    public NecroTeleportState teleportState { get; private set; }
    public NecroSpellCastState staffShineState { get; private set; }
    public NecroSpellCastState skullCastState { get; private set; }
    #endregion

    public bool bossFightBegun;

    [Header("Skull Cast Details")]
    [SerializeField] private GameObject skullPrefab;
    public int amountOfSkullSpell;
    public float skullSpellCooldown;
    public float distanceToCastSkull;

    [Header("Staff Cast Details")]
    [SerializeField] private GameObject bombPrefab;
    public int amountOfStaffSpell;
    public float staffSpellCooldown;

    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport Details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;

    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        idleState = new NecrIdleState(this, stateMachine, "Idle", this);
        battleState = new NecroBattleState(this, stateMachine, "Move", this);
        attackState = new NecroAttackState(this, stateMachine, "Attack", this);
        deadState = new NecroDeadState(this, stateMachine, "Idle", this);
        teleportState = new NecroTeleportState(this, stateMachine, "Teleport", this);
        staffShineState = new NecroSpellCastState(this, stateMachine, "StaffShine", this);
        skullCastState = new NecroSpellCastState(this, stateMachine, "SkullCast", this);
    }



    public bool CanDoSpellCast()
    {
        if(Time.time >= lastTimeCast + spellStateCooldown )
        {
            return true;
        }

        Debug.Log("Necro Spell Cast Cooldown");
        return false;
    }

    public bool CanCastSkull()
    {
        if(IsPlayerDetected().distance <= distanceToCastSkull)
        {
            return true;
        }
        else
            return false;
    }

}
