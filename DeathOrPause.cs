using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOrPause : MonoBehaviour
{

    public bool isDeath;

    public bool isPause;

    public GameObject deathScreen;

    public GameObject pauseScreen;
    

   
    void Update()
    {
        if(isDeath)
        {
            if(deathScreen != null)
            
                deathScreen.SetActive(true);
        }

        if(isPause)
        {
            if (pauseScreen != null)

                pauseScreen.SetActive(true);
        }
        
    }
}
