using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuControl : MonoBehaviour
{
    /* 
        Simple UI Setup
        https://www.youtube.com/watch?v=VHFJgQraVUs

        How can I get a UI Canvas to hide/appear on 'esc' button press?
        http://answers.unity.com/answers/850313/view.html

        https://docs.unity3d.com/ScriptReference/TextMesh-text.html
    */

    public static int points;

    public GameObject menu;
    public GameObject scoreboard;
    private bool isShowing = true;

    void Start()
    {
        points = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
            // Debug.Log($"Menu is showing: {isShowing}");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && isShowing)
        {
            isShowing = false;
            menu.SetActive(isShowing);
            // Debug.Log($"Menu is showing: {isShowing}");
        }

        scoreboard.GetComponent<TextMesh>().text = $"Score: {points}";
    }
}
