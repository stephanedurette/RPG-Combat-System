 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IDamageTaker
{
    [SerializeField] internal float walkSpeed = 10f;
    [SerializeField] private Transform weaponParent;
    [SerializeField] internal float invincibleTimeInSeconds = 2f;
    [SerializeField] private String normalLayerName, invincibleLayerName;
    [SerializeField] private GameObject startingWeaponPrefab;

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
        this.gameObject.layer = LayerMask.NameToLayer(on ? invincibleLayerName : normalLayerName);
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
    }

    internal void OnAttackPressed(object sender, EventArgs e)
    {
        Attack();
    }

    internal void OnDisable()
    {
        health.OnHealthEmpty -= OnHealthEmpty;
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

    public void TakeDamage(GameObject source, HitData hitData)
    {
        health.ChangeHealth(-hitData.damage);

        rigidBody.velocity = (transform.position - source.transform.position).normalized * hitData.knockBackVelocity;
        playerKnockbackState.Setup(hitData.knockBackTime);
        stateMachine.SetState(playerKnockbackState);
    }
}
