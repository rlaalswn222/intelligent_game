using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlagController : MonoBehaviour
{
    bool isRotating = false;
    public int numOfRotations = 0;
    float totalRotationAngle = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isRotating = !isRotating;
        }
        if (isRotating)
        {
            transform.Rotate(0, 10, 0);

            // 회전 각도 누적
            totalRotationAngle += 10;

            // 회전 횟수 계산
            numOfRotations = (int)(totalRotationAngle / 360);

            //Debug.Log("총 " + numOfRotations + " 바퀴 돌았습니다.");
        }

    }
}