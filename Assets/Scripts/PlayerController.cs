using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private PlayerSpells playerSpells;
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float jumpForce, dashForce, vSensitivity, hSensitivity, lookLimit, lookBlend, dashCost;

    private Player player;
    private InputAction move, jump, look;
    private InputAction[] actions;

    private Vector3 movement, dashVector;
    private float lookVal;
    private bool dashed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        player = new Player();
        actions = new InputAction[3];

        move = player.Wizard.Move;
        jump = player.Wizard.Jump;
        look = player.Wizard.Look;

        actions[0] = move;
        actions[1] = jump;
        actions[2] = look;

        jump.performed += OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (playerPhysics.grounded)
        {
            playerPhysics.ApplyForce(new Vector3(0, jumpForce, 0), false, true);
        }
        else if (!dashed)
        {
            if (playerSpells.ManaSpend(dashCost))
            {
                if (movement == Vector3.zero)
                {
                    dashVector = transform.forward;
                }
                else
                {
                    dashVector = (transform.forward * movement.z + transform.right * movement.x).normalized;
                }

                playerPhysics.ApplyForce(dashVector * dashForce, false, false);
            }
        }
    }

    private void OnEnable()
    {
        foreach (InputAction action in actions)
        {
            action.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (InputAction action in actions)
        {
            action.Disable();
        }
    }

    private void Update()
    {
        //send movement to player physics (making sure magnitude <= 1)
        movement = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        playerPhysics.ChangeMovement(movement);

        //rotate camera and player
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + look.ReadValue<Vector2>().x * hSensitivity, 0);

        lookVal = Mathf.Clamp(lookVal - look.ReadValue<Vector2>().y * vSensitivity, -lookLimit, lookLimit);
        camAnchor.localEulerAngles = new Vector3(lookVal, 0, 0);

        //dashed reset
        if (playerPhysics.grounded && dashed)
        {
            dashed = false;
        }
    }
}
