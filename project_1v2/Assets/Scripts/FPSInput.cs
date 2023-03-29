using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]	// Ensures that other components needed by the script are also attached.
[AddComponentMenu("Control Script/FPS Input")]	// Script will be added to the component menu in Unity’s editor.

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float sprintSpeedMultiplier = 2.0f;
    public float crouchSpeedMultiplier = 2.0f;
    public float zoomMultiplier = 2.0f;

    private CharacterController charController; // Variable for referencing the CharacterController.
    private float baseSpeed;
    private float baseHeight;
    private float crouchHeight;
    private float FOV;
    private float zoomFOV;

    void Start()
    {
        charController = GetComponent<CharacterController>();	// Access other components attached to the same object.
        baseSpeed = speed;
        baseHeight = 1.0f;
        crouchHeight = baseHeight / 2;
        FOV = 60.0f;
        zoomFOV = FOV / zoomMultiplier;
    }

    void Update()
    {
        // commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection
        // transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);

        // Basic WASD-style movement control
        float deltaX = Input.GetAxis("Horizontal") * speed;		// “Horizontal” and “Vertical” are indirect names for keyboard mappings.
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);		// Limit diagonal movement to the same speed as movement along an axis.
        movement.y = gravity;
        movement *= Time.deltaTime;								// Framerate independent movement
        movement = transform.TransformDirection(movement);		// Transform the movement vector from local to global coordinates
        charController.Move(movement);                          // Tell the CharacterController to move by that vector.

        // https://docs.unity3d.com/ScriptReference/KeyCode.html
        // https://docs.unity3d.com/ScriptReference/Input.GetKeyUp.html
        // Sprint: Increase speed on shift press.
        if (Input.GetKeyDown(KeyCode.LeftShift) == true)
        {
            speed = baseSpeed * sprintSpeedMultiplier;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) == true)
        {
            speed = baseSpeed;
        }

        // https://docs.unity3d.com/ScriptReference/Transform.html
        // http://answers.unity.com/answers/656143/view.html
        // Crouch: Decrease PosY and ScaleY and speed on ctrl press.
        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            speed = baseSpeed / crouchSpeedMultiplier;
            transform.localPosition = new Vector3(transform.position.x, crouchHeight, transform.position.z);
            transform.localScale = new Vector3(1, crouchHeight, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) == true)
        {
            speed = baseSpeed;
            transform.localPosition = new Vector3(transform.position.x, baseHeight, transform.position.z);
            transform.localScale = new Vector3(1, baseHeight, 1);
        }

        // https://docs.unity3d.com/ScriptReference/Camera-fieldOfView.html
        // Zoom: Lower field of view  on right click.
        if (Input.GetKeyDown(KeyCode.Mouse1) == true)
        {
            Camera.main.fieldOfView = zoomFOV;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) == true)
        {
            Camera.main.fieldOfView = FOV;
        }
    }
}