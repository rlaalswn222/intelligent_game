using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food2Generator : MonoBehaviour
{
    public GameObject Food2Prefab;
    float span = 1.5f;
    float delta = 0;

    void Start()
    {

    }

   
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(Food2Prefab) as GameObject;
            float px = Random.Range(-6, 7);

            go.transform.position = new Vector3(px, 6, 0);

        }
    }
}
