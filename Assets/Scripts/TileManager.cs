using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    // tile prefab
    public GameObject tilePrefab;      

    // Size of the grid
    public int gridSize = 10;      

    // text to show tile's info
    public Text tileText;

    // Player prefab
    public GameObject playerPrefab;    

    //enemy prefab
    public GameObject enemyPrefab;

    // Reference to the instantiated player
    private GameObject player;          

    // refrence to instantiated enemy
    private GameObject enemy;

    // Reference to Player script
    private Player playerMovement;      

    private void Start()
    {
        //if tile prefab is empty 
        if (tilePrefab == null)
        {
            //return error
            Debug.LogError("Tile Prefab is not assigned");
            return;
        }

        //if text is not assigned in inspector 
        if (tileText == null)
        {
            //return this error 
            Debug.LogError("Text is not assigned");
            return;
        }

        //if player prefab is not assigned in inspector 
        if (playerPrefab == null)
        {
            //return error message
            Debug.LogError("Player Prefab is not assigned");
            return;
        }

        //if enemy's prefab is not assigned
        if (enemyPrefab == null)
        {
            //then return error
            Debug.LogError("Enemy Prefab is not assigned");
            return;
        }

        //generate 10*10 grid at start of game
        GenerateGrid(); 

        //instantiate player
        SpawnPlayer(); 

        //delaying time for enemy spawn 
        float delay = Random.Range(2f, 3f);
        //spawning enemy twice
        Invoke("SpawnEnemy", delay);
        Invoke("SpawnEnemy", delay);
    }

    // grid generation
    private void GenerateGrid()
    {
        // // Looping through the x and y axis
        for (int x = 0; x < gridSize; x++) 
        {
            for (int y = 0; y < gridSize; y++)
            {
                // tile position
                Vector3 tilePosition = new Vector3(x , 0, y); 

                // Creating a tile
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                
                //geting reference to Tile script
                Tile tileComponent = tile.GetComponent<Tile>();

                //if tilecomponent is not null
                if (tileComponent != null)
                {
                   
                    tileComponent.SetPosition(x, y); 

                    Renderer tileRenderer = tile.GetComponent<Renderer>();
                    if (tileRenderer != null)
                    {
                        if ((x + y) % 2 == 0)
                        {
                            // Gray for even sum
                            tileRenderer.material.color = Color.gray; 
                        }
                        else
                        {
                            // White for odd sum
                            tileRenderer.material.color = Color.white; 
                        }
                    }
                    else
                    {
                        Debug.LogError("Tile Prefab does not have a Renderer component.");
                    }
                }
                else
                {
                    Debug.LogError("Tile Prefab does not have a Tile component.");
                }
            }
        }
    }

    private void SpawnPlayer()
    {
        // player spawn position
        Vector3 playerSpawnPosition = new Vector3((gridSize / 2) , 1, (gridSize / 2));

        //intantiating player prefab
        player = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);

        // Getting reference of the Player script
        playerMovement = player.GetComponent<Player>(); 

        //checking player is empty or not
        if (playerMovement == null)
        {
            Debug.LogError("Player prefab does not have a Player script.");
        }
    }

    
    private void SpawnEnemy()
    {
        // randamizing spawn 
        int randomX = Random.Range(0, gridSize);
        int randomY = Random.Range(0, gridSize);

        //setting enemy position
        Vector3 enemySpawnPosition = new Vector3(randomX, 1, randomY);

        // instantiate enemy prefab
        enemy = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
    }

    private void Update()
    {
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;

            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            // If ray hits something
            if (Physics.Raycast(ray, out hit)) 
            {
                // Get the tile information of clicked tile
                Tile clickedTile = hit.collider.GetComponent<Tile>(); 
                
                //if clicked object is tile
                if (clickedTile != null) 
                {

                    if (player != null && playerMovement != null)
                    {
                        // Move player to the clicked tile
                        Vector3 targetPosition = new Vector3(clickedTile.transform.position.x, 1, clickedTile.transform.position.z);
                        playerMovement.MoveTo(targetPosition);
                    }
                    else
                    {
                        Debug.LogWarning("Player or Player Movement script is not properly instantiated.");
                    }
                }
                else
                {
                    Debug.LogWarning("Raycast hit an object without a Tile component.");
                }
            }
        }
    }
}
