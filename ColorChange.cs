using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]    
public class BlackWhite
{
    public bool isBlack;
    public bool isWhite;
    public SpriteRenderer currentRend;
    public Sprite black;
    public Sprite white;
    public Transform obj;
    public Vector2 boxSize;
    public bool playerIsIn;

}

public class ColorChange : MonoBehaviour
{
    [SerializeField]
    public List<BlackWhite> switcher = new List<BlackWhite>();

    private bool wasPressed;
    private float delay;
    public GameObject player;
    public GameObject arrow;
    public AudioSource spaceBarPlay;




    public LayerMask playerLayer;

    private void Start()
    {
      
      
    }
    void Update()
    {

        foreach (BlackWhite currentPlayer in switcher)
        {
           if(currentPlayer.playerIsIn && currentPlayer.isBlack)
            {
                if(player != null)
                player.GetComponent<SpriteRenderer>().color = Color.white;
                arrow.GetComponent<SpriteRenderer>().color = Color.black;


            }
           if(currentPlayer.playerIsIn && currentPlayer.isWhite)
            {
                    if (player != null)
                player.GetComponent<SpriteRenderer>().color = Color.black;
                arrow.GetComponent<SpriteRenderer>().color = Color.white;


            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && wasPressed == false)
        {
            delay = 0;
            wasPressed = true;
            spaceBarPlay.Play();
            foreach (BlackWhite currentObj in switcher)
            {
               
                if (currentObj.isBlack)
                {   
                    currentObj.isWhite = true;
                    currentObj.isBlack = false;
                    currentObj.currentRend.sprite = currentObj.white;

                    currentObj.obj.DOPunchScale(new Vector3(1,1,1), 0.2f, 1, 0);
                    
                   

                }

               else if(currentObj.isWhite)
                {
                    currentObj.isWhite = false;
                    currentObj.isBlack = true;
                    currentObj.currentRend.sprite = currentObj.black;

                    currentObj.obj.DOPunchScale(new Vector3(1, 1, 1), 0.2f, 1, 0);



                }
               

            }

        }
    }

    private void FixedUpdate()
    {

        foreach (BlackWhite currentLayer in switcher) // Fix! Not being initialized correctly. 
        {

            currentLayer.boxSize = currentLayer.obj.localScale * 2;

            currentLayer.playerIsIn = Physics2D.OverlapBox(currentLayer.obj.position, currentLayer.boxSize, 0, playerLayer);
        }
        if (delay <= 1 && wasPressed == true)
        {
            delay += Time.deltaTime;
            
        }

        else
        {
            wasPressed = false;
           
        }

    }

    private void OnDrawGizmosSelected()
    {
      foreach(BlackWhite test in switcher)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(test.obj.position, test.boxSize);
        }
            
        
    }
}
