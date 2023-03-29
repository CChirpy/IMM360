using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void OnGUI()
    {
        // This is just the rough size of this font.
        int size = 12;

        // Always displays "*" on screen to act as crosshair.
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))                                            	// Responds to the left (first) mouse button.
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);	// The middle of the screen is half its width and height.
            Ray ray = cam.ScreenPointToRay(point);                                  	// Create the ray at the point by using ScreenPointToRay().
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))                                      	// The raycast fills a referenced variable with information.
            {
                GameObject hitObject = hit.transform.gameObject;						// Retrieve the object the ray hit.
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)														// Check for the ReactiveTarget component on the object.
                {
					// Sends message to target object.
                    target.ReactToHit();												// Call a method of the target instead of just emitting the debug message.
                }
            }
        }
    }
}