using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private float terminalVelocity, groundedRayLength, jumpBaseHeight, airResistance, speed, moveBlend;

    private RaycastHit groundHit;
    private Vector3 actingForce, airResistanceV, movementForce, movementForceGoal;
    private bool grounded;

    private void Start()
    {
        groundHit = new RaycastHit();
        ApplyForce(new Vector3(0, 2, 0), true, true);
    }

    private void FixedUpdate()
    {
        //handle movement force
        movementForce = Vector3.Lerp(movementForce, movementForceGoal, moveBlend * Time.deltaTime);

        //determine if grounded
        Physics.Raycast(transform.position, -Vector3.up, out groundHit, groundedRayLength);
        if (groundHit.collider != null )
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        //if grounded, have acting force y set to 0
        if (grounded)
        {
            actingForce.y = 0;
        }
        //if not grounded, have gravity effect acting force
        else if (actingForce.y > -terminalVelocity)
        {
            actingForce += gravity * Time.deltaTime;
        }

        //apply air resistance
        airResistanceV = new Vector3(-actingForce.x * airResistance, 0, -actingForce.z * airResistance);
        if (grounded)
        {
            actingForce += airResistanceV * 10 * Time.deltaTime;
        }
        else
        {
            actingForce += airResistanceV * Time.deltaTime;
        }

        //cancel out acting force if too low
        if (actingForce.magnitude <= 0.5f && grounded)
        {
            actingForce = Vector3.zero;
        }

        //apply all forces
        rb.MovePosition(rb.position + (actingForce + movementForce * speed) * Time.deltaTime);

        //nullify rigidbody force
        rb.velocity = Vector3.zero;
    }

    public void ApplyForce(Vector3 force, bool overRide, bool jump)
    {
        if (jump && grounded)
        {
            transform.position = new Vector3(transform.position.x, groundHit.point.y + jumpBaseHeight, transform.position.z);
        }

        if ((jump && grounded) || (!jump))
        {
            if (overRide)
            {
                actingForce = force;
            }
            else
            {
                actingForce += force;
            }
        }
    }

    public void ChangeMovement(Vector3 direction)
    {
        movementForceGoal = transform.forward * direction.z + transform.right * direction.x;
    }
}
