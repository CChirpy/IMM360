using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Building a 2D Parallax Scrolling Background in Unity:
    https://youtu.be/9bhkH7mtFNE

    How to make a texture tile and not stretch: 
    https://answers.unity.com/questions/361573/how-to-make-a-texture-tile-and-not-stretch.html

    Why Logging doesn't use string interpolation:
    https://stackoverflow.com/a/52201456
*/

public class parallaxHorizontal : MonoBehaviour
{
    public float speed;
    private float deltaX;

    void Update()
    {
        // float inputX = playerMove.inputX;
        // deltaX += speed * inputX * Time.deltaTime;
        // Debug.Log($"deltaX {deltaX}, inputX {inputX}, deltaTime {Time.deltaTime}");

        deltaX = speed * playerMove.deltaX;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(deltaX, 0);
    }
}
