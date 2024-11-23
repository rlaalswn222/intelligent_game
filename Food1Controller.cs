using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food1Controller : MonoBehaviour
{
    GameObject JellyGreen;
    GameObject JellyPink;
    GameObject JellyBread;

    void Start()
    {
        this.JellyGreen = GameObject.Find("JellyGreen");
        this.JellyPink = GameObject.Find("JellyPink");
        this.JellyBread = GameObject.Find("JellyBread");
    }

    void Update()
    {
        transform.Translate(0, -0.01f, 0);

        if (transform.position.y < -8.0f)
        {
            Destroy(gameObject);
        }

        //좌표 불러오기
        Vector2 f1 = transform.position;
        Vector2 jg = this.JellyGreen.transform.position;
        Vector2 jp = this.JellyPink.transform.position;
        Vector2 jb = this.JellyBread.transform.position;


        //거리 측정
        Vector2 dirg = f1 - jg;
        Vector2 dirp = f1 - jp;
        Vector2 dirb = f1 - jb;
        
        //dg=green과 거리 dp=pink와 거리 db=bread와 거리
        float dg = dirg.magnitude;
        float dp = dirp.magnitude;
        float db = dirb.magnitude;

        //각 오브젝트들 반경
        float fr = 0.5f;
        float gr = 1.0f;
        float pr = 1.0f;
        float br = 1.0f;
      
        //충돌 판정
        if (dg < fr + gr) //초록 젤리와 food1 
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreaseGreenF1Hp();
            Destroy(gameObject);


        }
        if (dp < fr + pr) //핑크 젤리와 food1
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreasePinkF1Hp();
            Destroy(gameObject);
        }
        if (db < fr + br) //빵 젤리와 food1
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreaseBreadF1Hp();
            Destroy(gameObject);
        }

    }
}
