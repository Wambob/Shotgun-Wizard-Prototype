using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMovements : MonoBehaviour
{
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private Transform shotgun;
    [SerializeField] private float walkingRange, walkingSpeed, fallingRange, fallingSpeed, disparity;

    private Vector3 shotgunOrigin, shotgunPos;
    private Vector2 horizontalMove;

    private void Start()
    {
        shotgunOrigin = shotgun.localPosition;
    }

    private void Update()
    {
        horizontalMove = new Vector2(playerPhysics.actingForce.x, playerPhysics.actingForce.z);

        //apply horizontal forces
        shotgunPos.y = (walkingRange) * ((horizontalMove.magnitude + playerPhysics.movementForceGoal.magnitude) / playerPhysics.speed) * Mathf.Sin(Time.time * walkingSpeed * ((horizontalMove.magnitude + playerPhysics.movementForceGoal.magnitude) / playerPhysics.speed));

        //apply veritcal forces
        if (playerPhysics.actingForce.y <= 0)
        {
            shotgunPos.x = (fallingRange) * (-playerPhysics.actingForce.y / playerPhysics.terminalVelocity) * Mathf.Sin(Time.time * fallingSpeed * (-playerPhysics.actingForce.y / playerPhysics.terminalVelocity) * disparity);
        }

        //apply changes
        shotgun.localPosition = shotgunOrigin + shotgunPos;
    }
}
