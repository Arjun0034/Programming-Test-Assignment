using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // moving speed for enemy
    public float moveSpeed = 3f;

    // boolian to check enemy is mobing!
    private bool isMoving = false;

    // Reference to player's transform
    private Transform playerTransform; 

    private void Start()
    {
        // Find player by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // if player tag is found 
        if (player != null)
        {
            //then playertranfrom equal to players position
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        // if player's position is not empty 
        if (playerTransform != null && !isMoving)
        {
            //start coroutine to move to the player
            StartCoroutine(MoveToPlayer());
        }
    }

    private IEnumerator MoveToPlayer()
    {
        //setting movement to true
        isMoving = true;

        //target postion = player's position
        Vector3 targetPosition = new Vector3(playerTransform.position.x, 1, playerTransform.position.z); 

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            //move enemt to the player position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // set enemy position to the player position
        transform.position = targetPosition;

        // setting ismoving to false if enemy reaces players position
        isMoving = false;

        // Delaying the next movement to avoid constant starting and stopping
        yield return new WaitForSeconds(0.1f);
    }

}
