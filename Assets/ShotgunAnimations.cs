using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnimations : MonoBehaviour
{
    [SerializeField] private Animator shotgunAnim;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float blendSpeed;

    private Vector2 inputFloat;

    private void Update()
    {
        inputFloat = Vector2.Lerp(inputFloat, playerController.look.ReadValue<Vector2>(), blendSpeed * Time.deltaTime);

        shotgunAnim.SetFloat("LookX", inputFloat.x);
        shotgunAnim.SetFloat("LookY", inputFloat.y);
    }
}
