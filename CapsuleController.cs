using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    GameObject player;

    float rotSpeed = 3;
    float fallSpeed = 0.3f;

    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Rotate(0, 0, this.rotSpeed);

        Vector3 moveDirection = Vector3.down; // 아래 방향으로 이동하는 벡터 할당
        transform.Translate(moveDirection * fallSpeed * Time.deltaTime, Space.World);

    }
}
