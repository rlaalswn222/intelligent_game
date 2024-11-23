using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    public GameObject CapsulePrefab;
    float span = 3.0f;
    float delta = 0;


    void Start()
    {

    }

    void Update()
    {
        // 시간 간격을 누적하여 span이상일 때마다 캡슐 생성
        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;

            // 캡슐 생성
            GameObject go = Instantiate(CapsulePrefab) as GameObject;

            // 랜덤한 위치 지정
            float px = Random.Range(-1, 2);
            float pz = Random.Range(-8, 11);
            go.transform.position = new Vector3(px, 0, pz);
        }
    }
}
