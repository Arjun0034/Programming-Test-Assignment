using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //// Speed for player movement 
    public float moveSpeed = 5f; 

    // boolian to check player is moving
    private bool isMoving = false;

    // Target position for movement
    private Vector3 targetPosition; 

    public void MoveTo(Vector3 destination)
    {
        // if player is still at one place
        if (!isMoving)
        {
            // then move player to the selected tile
            StartCoroutine(MoveToTile(destination));
        }
    }

    IEnumerator MoveToTile(Vector3 destination)
    {
        // setting player movement to true
        isMoving = true;

        // setting target position = destination
        targetPosition = destination;

        //looping
        //between player positiona and clicked tile
        // loop stop when player within the 0.1f unit from the target position
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            //moving player to the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // setting target position into player's current position
        transform.position = targetPosition;

        //and moving = false
        //to stop player from moving
        isMoving = false;
    }
}