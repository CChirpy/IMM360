using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxVertical : MonoBehaviour
{
    public float speed;

    private float startY;
    private float deltaY; 

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        deltaY = -playerMove.deltaY * speed + startY;
        transform.position = new Vector3(transform.position.x, deltaY, transform.position.z);
    }
}
