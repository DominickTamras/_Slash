using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Conditionals
{
    public GameObject count;
    public GameObject obstacleDestroy;
}

public class EoL : MonoBehaviour
{

    bool condition;

    [Header("End of Level conditions")]
    bool enemyDead;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
