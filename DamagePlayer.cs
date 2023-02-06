using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public LayerMask damagedLayer;
    public Vector2 damageBoxSize;
    public bool isActiveObstacle;
    public bool isActiveAttack;
    public AudioSource death;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isActiveObstacle = Physics2D.OverlapBox(transform.position, damageBoxSize, 0, damagedLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            death.Play();

            other.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
      
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, damageBoxSize);
    }
}
