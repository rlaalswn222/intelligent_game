using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mid_rou : MonoBehaviour
{
    float rotSpeed = 0; //처음 회전 속도

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 10;

        }

        if (Input.GetMouseButtonDown(1))
        {
            rotSpeed -= 1;
            if (rotSpeed < 0)
            {
                rotSpeed = 0;
            }
        }

        transform.Rotate(0, 0, this.rotSpeed);
    }
}
