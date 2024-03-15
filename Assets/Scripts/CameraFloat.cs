using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFloat : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float floatBlend;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, floatBlend * Time.deltaTime);
        transform.rotation = target.rotation;
    }
}
