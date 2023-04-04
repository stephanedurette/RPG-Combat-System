using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] internal float walkSpeed = 10f;
    [SerializeField] private Transform weaponParent;
    [SerializeField] internal float invincibleTimeInSeconds = 2f;
    [SerializeField] private GameObject startingWeaponPrefab;
    [SerializeField] private Hurtbox hurtbox;

    internal Vector2 lastMoveDirection = Vector2.zero;

    internal Rigidbody2D rigidBody;
    internal Animator animator;
    private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;

    internal StateMachine stateMachine;

    internal ResponsiveState responsiveState;
    private PlayerKnockbackState playerKnockbackState;
    internal InvincibleState invincibleState;

    private Weapon currentWeapon;

    internal void SetInvincibleEffect(bool on)
    {
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

        stateMachine = new StateMachine(responsiveState);
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
        currentWeapon = Instantiate(weapon, weaponParent.transform.position, Quaternion.identity, weaponParent).GetComponent<Weapon>();
    }

    internal void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnHealthEmpty += OnHealthEmpty;
        hurtbox.OnHurtboxHit += Hurtbox_OnHurtboxHit;
    }

    private void Hurtbox_OnHurtboxHit(object sender, Hurtbox.OnHurtboxHitEventArgs e)
    {
        health.ChangeHealth(-e.hitData.damage);

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
        hurtbox.OnHurtboxHit -= Hurtbox_OnHurtboxHit;
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
