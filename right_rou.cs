using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class right_rou : MonoBehaviour
{

    float rotSpeed = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) //왼쪽 버튼 누르는 중에
            this.rotSpeed = 10;
        else
            this.rotSpeed *= 0.99f;


        transform.Rotate(0, 0, this.rotSpeed);
    }
}
