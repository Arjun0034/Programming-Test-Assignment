using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 tilePosition;


    //setting tile position 
    public void SetPosition(int x, int y)       
    {
        tilePosition = new Vector2(x, y);
    }
}
