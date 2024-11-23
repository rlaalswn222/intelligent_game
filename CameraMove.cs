using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset; // 상대적 player와의 거리 벡터 계속 고정 시킴

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        //이렇게 Offset을 더해줘야지 player와 같은 position이 아니라 좀 거리가 있게 나온다 
        transform.position = playerTransform.position + Offset;
    }
}
