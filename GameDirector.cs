using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.FilePathAttribute;

public class GameDirector : MonoBehaviour
{
    GameObject car;
    GameObject flag;
    GameObject distance;
    GameObject carY;
    GameObject flagY;
    GameObject distanceY;
    GameObject flagRotations;
   
    void Start()
    {
        this.car = GameObject.Find("car");
        this.flag = GameObject.Find("flag");
        this.flagRotations = GameObject.Find("FlagRotations");
        this.distance = GameObject.Find("Distance");
        this.carY = GameObject.Find("carY");
        this.flagY = GameObject.Find("flagY");
        this.distanceY = GameObject.Find("DistanceY");
    }

    void Update()
  
    {
        float length_y = this.carY.transform.position.y - this.flagY.transform.position.y;
        float length_x = this.flag.transform.position.x - this.car.transform.position.x;
        int rotation = this.flag.GetComponent<FlagController>().numOfRotations;

        this.flagRotations.GetComponent<TextMeshProUGUI>().text = "깃발 회전 횟수 " + rotation.ToString() + "회";
        this.distanceY.GetComponent<TextMeshProUGUI>().text = "Green Car 목표 지점까지 " + length_y.ToString("F2") + "m";
        this.distance.GetComponent<TextMeshProUGUI>().text = "Red Car 목표 지점까지 " + length_x.ToString("F2") + "m";
    }
}
