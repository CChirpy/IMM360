using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Number of rows and columns in the grid.
    public const int gridSize = 3;

    // How far apart the markers are.
    public const float offsetX = 2f;
    public const float offsetY = 2f;

    // The original marker object, set in Unity scene.
    [SerializeField] PlaceMarker originalMarker;

    // State of the board is stored in a 2d array.
    public static int[,] boardState;

    // Determines whos turn it is. 
    public static int turn;

    // Tracks number of moves/actions made.
    public static int moveCount;

    void Start()
    {
        /*  -----------------------------
            Initialize markers.
            - Unity in Action Chapter 5
            ----------------------------- */

        // Position of the original marker.
        Vector3 startPos = originalMarker.transform.position;

        // Fill the grid with markers by duplicating the original.
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                // Reference to marker object.
                PlaceMarker clone;

                // Use the original for the first grid space.
                if (i == 0 && j == 0)
                { clone = originalMarker; }

                // Create a new marker for all the other grid spaces.
                else
                { clone = Instantiate(originalMarker) as PlaceMarker; }

                // Position the new marker.
                float posX = (offsetX * j) + startPos.x;
                float posY = -(offsetY * i) + startPos.y;
                clone.transform.position = new Vector3(posX, posY, startPos.z);

                // Memorize where this marker is placed.
                clone.savePlacement(i, j);
            }
        }

        /*  ---------------------------------
            Initialize state of the board.
            0 = empty, 1 = circle, 2 = cross
            --------------------------------- */

        // Initialize 2d array with then number of rows and columns.
        boardState = new int[gridSize, gridSize];

        // Circle starts first.
        turn = 1;

        // No moves made yet.
        moveCount = 0;
    }
}
