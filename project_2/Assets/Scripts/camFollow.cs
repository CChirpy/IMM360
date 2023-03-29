using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Chapter 6 FollowCam.cs
*/

public class camFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void LateUpdate()
    {
        // Preserve Z position while changing X and Y. 
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y - startY, transform.position.z);

        // Smooth transition from current to target position.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
