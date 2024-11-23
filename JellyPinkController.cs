using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPinkController : MonoBehaviour
{
    float rotSpeed = 0; // 회전 속도

    void Start()
    {
        
    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            transform.Translate(-1, 0, 0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            transform.Translate(1, 0, 0);
        }

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            transform.Translate(0, 1, 0);
        }
        else if (wheelInput < 0)
        {
            transform.Translate(0, -1, 0);
        }
    }

    //분홍 젤리가 이겼을 때 회
    public void RotatePinkJelly()
    {
        this.rotSpeed = 20;
        transform.Rotate(0, 0, this.rotSpeed);
    }
}
