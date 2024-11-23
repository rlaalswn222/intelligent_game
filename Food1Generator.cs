using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food1Generator : MonoBehaviour
{
    public GameObject Food1Prefab;
    float span = 1.0f;
    float delta = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(Food1Prefab) as GameObject;
            float px = Random.Range(-6, 7);
            
            go.transform.position = new Vector3(px, 7, 0);

        }
    }
}
