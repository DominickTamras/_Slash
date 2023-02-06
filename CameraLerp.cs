using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    [Header("Camera Stuff")]
    public float smoothSpeed;
    public Transform player;
    public Vector3 offSet;
    private bool isRight;
    private bool isLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            isLeft = true;
            isRight = false;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            isLeft = false;
            isRight = true;
        }
    }
    private void FixedUpdate()
    {

        Vector3 desiredPos = player.position + offSet;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
        if (isLeft)
        {
            offSet.x =- 7;
        }

        if(isRight)
        {
            offSet.x =+ 6;
        }
   

    }


}
