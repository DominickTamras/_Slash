using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class Feel : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float delay = 1.0f;
    float delta = 0f;

    SpriteRenderer spriteR;
    public float destroyTime = 0.1f;
    public Color color;
    public Material material = null;
    private GameObject player;
    public bool pingPong;
    public float pong;
    public Transform pingerponger;
    public float bottomFloor = 1.5f;

    void Start()
    {
        
    }

    private void Update()
    {
       if(pingPong)
        {
            PingPong(pingerponger, 2);
        }
       
    }

    void CreateGhost()
    {
      
        
    }

    void PingPong(Transform obj, float speed)
    {
        Vector3 pos = obj.position;

        float length = 1.0f;
        pos.y = Mathf.PingPong(Time.time, length) + bottomFloor;
        obj.position = pos;
    }


}
