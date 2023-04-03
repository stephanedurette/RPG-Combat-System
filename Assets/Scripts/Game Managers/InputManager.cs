using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Inputs inputs;

    public static event EventHandler<EventArgs> OnAttackPressed;
    public static event EventHandler<EventArgs> OnShootPressed;
    public static event EventHandler<EventArgs> OnShieldPressed;

    private static Vector2 moveVector;
    public static Vector2 MoveVector => moveVector;

    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Player.Enable();

        inputs.Player.Attack.performed += Attack_performed;
        inputs.Player.Shoot.performed += Shoot_performed;
        inputs.Player.Shield.performed += Shield_performed;
    }

    private void Shield_performed(InputAction.CallbackContext obj)
    {
        OnShieldPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        OnShootPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        OnAttackPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        inputs.Player.Attack.performed -= Attack_performed;
        inputs.Player.Shoot.performed -= Shoot_performed;
        inputs.Player.Shield.performed -= Shield_performed;
    }

    private void Update()
    {
        moveVector = inputs.Player.Move.ReadValue<Vector2>();
    }
}
