using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private float terminalVelocity, groundedRayLength, groundHeight;

    private RaycastHit groundHit;
    private Vector3 actingForce;
    private bool grounded;

    private void Start()
    {
        groundHit = new RaycastHit();
    }

    private void FixedUpdate()
    {
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

        //if grounded, have acting force set to 0
        if (grounded)
        {
            actingForce.y = 0;
        }

        //if not grounded, have gravity effect acting force
        else if (actingForce.y > -terminalVelocity)
        {
            actingForce += gravity * Time.deltaTime;
        }

        //apply 
        rb.MovePosition(rb.position + actingForce);
        if (grounded)
        {
            transform.position = new Vector3(transform.position.x, groundHit.point.y + groundHeight, transform.position.z);
        }

        //nullify rigidbody force
        rb.velocity = Vector3.zero;
    }
}
