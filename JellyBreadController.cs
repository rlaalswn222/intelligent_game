using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBreadController : MonoBehaviour
{
    public float rotSpeed = 0; // 회전 속도

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(0, 1, 0);
        }
    }

    //잼 젤리가 이겼을 때 회
    public void RotateBreadJelly()
    {
        this.rotSpeed = 20;
        transform.Rotate(0, 0, this.rotSpeed);
    }
}
