using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_rou : MonoBehaviour
{
    float rotSpeed = 0;
    //회전속도

    bool isRotating = false;
    //회전 여부

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 10;
        }

        transform.Rotate(0, 0, this.rotSpeed);

        this.rotSpeed *= 0.99f;

        if (rotSpeed < 0.01f)
        {
            isRotating = false;
            Debug.Log(GetTopItem());
        }
    }
    string GetTopItem()
    {  
        float angle = transform.rotation.eulerAngles.z;

        if (angle >=330 && angle < 30)
            return "운수나쁨 ";
        else if (angle >= 30 && angle < 90)
            return "운수대통 ";
        else if (angle >= 90 && angle < 150)
            return "운수매우나쁨 ";
        else if (angle >= 150 && angle < 210)
            return "운수보통";
        else if (angle >= 210 && angle < 270)
            return "운수조심 ";
        else if (angle >= 270 && angle < 330)
            return "운수좋음 ";
        return "알 수 없음";
    }
}