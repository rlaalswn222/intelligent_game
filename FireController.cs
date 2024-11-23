using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    GameObject JellyGreen;
    GameObject JellyPink;
    GameObject JellyBread;

    float rotSpeed = -1;

    void Start()
    {
        this.JellyGreen = GameObject.Find("JellyGreen");
        this.JellyPink = GameObject.Find("JellyPink");
        this.JellyBread = GameObject.Find("JellyBread");
    }

    void Update()
    {
        transform.Rotate(0, 0, this.rotSpeed);

        Vector3 moveDirection = Vector3.down;
        transform.Translate(moveDirection * 0.01f, Space.World);

        if (transform.position.y < -9.0f) //화면 밖으로 나가면 제거 
        {
            Destroy(gameObject);
        }

       

        Vector2 f = transform.position;
        Vector2 jg = this.JellyGreen.transform.position;
        Vector2 jp = this.JellyPink.transform.position;
        Vector2 jb = this.JellyBread.transform.position;

        Vector2 dirg = f - jg;
        Vector2 dirp = f - jp;
        Vector2 dirb = f - jb; //젤리와 불의 거리

        float dg = dirg.magnitude;
        float dp = dirp.magnitude;
        float db = dirb.magnitude;

        float fr = 1.0f;
        float gr = 1.0f;
        float pr = 1.0f;
        float br = 1.0f;

        
        if(dg < fr + gr)
        {
            Destroy(gameObject);
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().GreenElimination();
            
        }

        if (dp < fr + pr)
        {
            Destroy(gameObject);
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().PinkElimination();
            
        }

        if (db < fr + br)
        {
            Destroy(gameObject);
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().BreadElimination(); // 탈락 메시지 호출
            
        }
    }
}
