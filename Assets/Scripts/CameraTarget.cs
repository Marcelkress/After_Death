using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Vector3 offset;
    private Vector3 followVelocity = Vector3.zero;
    private float yValue;


    public void Start()
    {
        yValue = transform.position.y;
    }

    private void LateUpdate()
    {
        Vector3 nextTarget = ((target1.position + target2.position) / 2f) + offset;

        nextTarget.y = yValue;


        Vector3 nextposition = Vector3.SmoothDamp(transform.position, nextTarget, ref followVelocity, 0.2f);

        transform.position = nextposition;       
    }
}
