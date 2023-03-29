using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMarker : MonoBehaviour
{
    // Array of children in marker object.
    // 1 = circle, 2 = cross
    public Renderer[] markerChildren;

    // The location of this marker.
    public int placementRow, placementCol;

    // Variables from SceneController.
    private int gridSize = SceneController.gridSize;

    void Start()
    {
        /*  ----------------------------------------------
            Hide all markers at the start.
            - https://txcom2003.wordpress.com/2011/07/01/
            ---------------------------------------------- */

        markerChildren = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in markerChildren)
        { child.enabled = false; }
    }

    // Initializes the location of this marker.
    public void savePlacement(int r, int c)
    {
        placementRow = r;
        placementCol = c;
    }

    public void OnMouseDown()
    {
        /*  --------------------------------------------------------------------------
            Place marker when empty area is clicked on.
            - https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html
            -------------------------------------------------------------------------- */

        // Only place marker if this marker is empty.
        if (SceneController.boardState[placementRow, placementCol] == 0)
        {
            // Make marker visible based on who's turn it is.
            markerChildren[SceneController.turn].enabled = true;

            // Count moves made.
            SceneController.moveCount++;

            // Save current board data into array.
            SceneController.boardState[placementRow, placementCol] = SceneController.turn;

            // Check if game over.
            checkGameOver(
                SceneController.boardState, gridSize,
                placementRow, placementCol,
                SceneController.turn, SceneController.moveCount);

            // Make it the other player's turn.
            if (SceneController.turn == 1)
            { SceneController.turn = 2; }
            else
            { SceneController.turn = 1; }
        }
    }

    /*  ----------------------------------------------------
        Check if the marker just placed was a winning move.
        https://stackoverflow.com/questions/1056316/
        ---------------------------------------------------- */

    public void checkGameOver(int[,] board, int size, int row, int col, int turn, int move)
    {
        // Check row.
        for (int i = 0; i < size; i++)
        {
            if (board[row, i] != turn)
            { break; }
            if (i == size - 1)
            { displayWin(turn); }
        }

        // Check column.
        for (int i = 0; i < size; i++)
        {
            if (board[i, col] != turn)
            { break; }
            if (i == size - 1)
            { displayWin(turn); }
        }

        // Check diagonal.
        if (row == col)
        {
            for (int i = 0; i < size; i++)
            {
                if (board[i, i] != turn)
                { break; }
                if (i == size - 1)
                { displayWin(turn); }
            }
        }

        // Check reverse diagonal.
        if (row + col == size - 1)
        {
            for (int i = 0; i < size; i++)
            {
                if (board[i, (size - 1) - i] != turn)
                { break; }
                if (i == size - 1)
                { displayWin(turn); }
            }
        }

        // Check draw.
        if (move == size * size )
        { Debug.Log("Draw!"); }
    }

    public void displayWin(int turn)
    {
        Debug.Log("Player " + turn + " Wins.");
    }
}
