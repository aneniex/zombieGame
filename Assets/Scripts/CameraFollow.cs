using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 offset;
    public float chaseSpeed = 5;

    private void LateUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, chaseSpeed * Time.deltaTime);
        }
    }
}
