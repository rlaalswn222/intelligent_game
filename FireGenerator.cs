using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGenerator : MonoBehaviour
{
    public GameObject FirePrefab;
    float span = 2.3f;
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
            GameObject go = Instantiate(FirePrefab) as GameObject;
            float px = Random.Range(-6, 7);
            float py = Random.Range(2, 7);

            go.transform.position = new Vector3(px, py, 0);

        }
        
    }
}
