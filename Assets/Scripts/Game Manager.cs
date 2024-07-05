using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //method for Try again button
    // to start game again
    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
}
