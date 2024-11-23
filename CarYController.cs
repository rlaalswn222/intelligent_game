using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarYController : MonoBehaviour
{
    float speed = 0;
    Vector2 startPos;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Vector2 endPos = Input.mousePosition;
            float swipeLength = (endPos.y - this.startPos.y);

            this.speed = swipeLength / 1000.0f;
        }

        transform.Translate(-this.speed, 0, 0);
        this.speed *= 0.98f;
    }
}
