using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Inputs inputs;

    public static event EventHandler<EventArgs> OnAttackPressed;
    public static event EventHandler<EventArgs> OnDashPressed;
    public static event EventHandler<EventArgs> OnShieldPressed;

    private static Vector2 moveVector;
    public static Vector2 MoveVector => moveVector;

    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Player.Enable();

        inputs.Player.Attack.performed += Attack_performed;
        inputs.Player.Dash.performed += Dash_performed;
        inputs.Player.Shield.performed += Shield_performed;
    }

    private void Shield_performed(InputAction.CallbackContext obj)
    {
        OnShieldPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        OnDashPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        OnAttackPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        inputs.Player.Attack.performed -= Attack_performed;
        inputs.Player.Dash.performed -= Dash_performed;
        inputs.Player.Shield.performed -= Shield_performed;
    }

    private void Update()
    {
        moveVector = inputs.Player.Move.ReadValue<Vector2>();
    }
}
