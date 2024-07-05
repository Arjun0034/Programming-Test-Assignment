using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTrigger : MonoBehaviour
{
    // trigger for game over 
    private void OnTriggerEnter(Collider other)
    {
        // if tag is enemy then load scene 1
        // scene 1 is defined in build setting
        if(other.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(1);
        }
    }
}
