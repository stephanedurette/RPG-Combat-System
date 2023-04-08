using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] internal float walkSpeed = 10f;
    [SerializeField] internal float invincibleTimeInSeconds = 2f;
    [SerializeField] private List<Hurtbox> hurtboxes;
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("Dashing")]
    [SerializeField] internal float dashSpeed = 10f;
    [SerializeField] internal float dashTime = 1f;
    [SerializeField] internal float dashCooldown = 1f;
    [SerializeField] internal AnimationCurve dashSpeedOverTime;
    [Header("Weapons")]
    [SerializeField] private GameObject startingWeaponPrefab;
    [SerializeField] private Transform weaponParent;
    [Header("Shield")]
    [SerializeField] internal float shieldDuration = 1f;
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private Collection shieldCollection;


    internal bool canDash = true;
    internal bool isShielded = false;

    internal Vector2 lastMoveDirection = Vector2.zero;

    internal Rigidbody2D rigidBody;
    internal Animator animator;

    internal StateMachine stateMachine;

    internal ResponsiveState responsiveState;
    private PlayerKnockbackState playerKnockbackState;
    internal InvincibleState invincibleState;
    private DashState dashState;
    private ShieldedState shieldedState;

    private Weapon currentWeapon;

    internal void SetInvincibleEffect(bool on)
    {
        foreach (Hurtbox hurtbox in hurtboxes)
            hurtbox.gameObject.SetActive(!on);
        spriteRenderer.material.SetInt("_IsInvincible", on ? 1 : 0);
    }

    private void Start()
    {
        if (startingWeaponPrefab != null)
        {
            EquipWeapon(startingWeaponPrefab);
        }

        responsiveState = new ResponsiveState(this);
        playerKnockbackState = new PlayerKnockbackState(this);
        invincibleState = new InvincibleState(this);
        dashState = new DashState(this);
        shieldedState = new ShieldedState(this);

        stateMachine = new StateMachine(responsiveState);
    }

    internal void OnDashPressed(object sender, System.EventArgs e)
    {
        if (canDash)
            stateMachine.SetState(dashState);
    }

    internal void OnShieldPressed(object sender, System.EventArgs e)
    {
        if (isShielded || shieldCollection.CurrentValue <= 0) return;

        shieldCollection.ChangeValue(-1);
        stateMachine.SetState(shieldedState);
    }

    internal void SetShield(bool on)
    {
        isShielded = on;
        shieldObject.SetActive(on);
    }

    private void Attack()
    {
        weaponParent.transform.right = lastMoveDirection;
        currentWeapon?.Attack();
    }

    public void EquipWeapon(GameObject weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        } 
        currentWeapon = Instantiate(weapon, weaponParent.transform.position, weaponParent.transform.rotation, weaponParent).GetComponent<Weapon>();
    }

    internal void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health.OnHealthEmpty += OnHealthEmpty;

        foreach (Hurtbox hurtbox in hurtboxes)
            hurtbox.OnHurtboxHit += Hurtbox_OnHurtboxHit;

        PickupManager.OnWeaponPickup += EquipWeapon;
    }

    private void Hurtbox_OnHurtboxHit(object sender, Hurtbox.OnHurtboxHitEventArgs e)
    {
        if (isShielded) return;

        health.ChangeValue(-e.hitData.damage);

        rigidBody.velocity = (transform.position - e.other.transform.position).normalized * e.hitData.knockBackVelocity;
        playerKnockbackState.Setup(e.hitData.knockBackTime);
        stateMachine.SetState(playerKnockbackState);
    }

    internal void OnAttackPressed(object sender, EventArgs e)
    {
        Attack();
    }

    internal void OnDisable()
    {
        health.OnHealthEmpty -= OnHealthEmpty;
        foreach (Hurtbox hurtbox in hurtboxes)
            hurtbox.OnHurtboxHit -= Hurtbox_OnHurtboxHit;
        PickupManager.OnWeaponPickup -= EquipWeapon;
    }


    private void OnHealthEmpty(object sender, EventArgs e)
    {
        FindObjectOfType<GameOverPanel>().TogglePanel(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        stateMachine.OnUpdate();
    }
}
