using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject JellyGreen;
    GameObject JellyPink;
    GameObject JellyBread;
    GameObject GreenHp;
    GameObject PinkHp;
    GameObject BreadHp;
    GameObject Winner;
    GameObject Rule;
    GameObject Elimination;

    
    AudioSource Finish;

    bool isGreenWin = false;
    bool isPinkWin = false;
    bool isBreadWin = false;
    bool isGreenElimination = false;
    bool isPinkElimination = false;
    bool isBreadElimination = false;

    void Start()
    {
        this.JellyGreen = GameObject.Find("JellyGreen");
        this.JellyPink = GameObject.Find("JellyPink");
        this.JellyBread = GameObject.Find("JellyBread");
        this.GreenHp = GameObject.Find("GreenHp");
        this.PinkHp = GameObject.Find("PinkHp");
        this.BreadHp = GameObject.Find("BreadHp");
        this.Winner = GameObject.Find("Winner");
        this.Rule = GameObject.Find("Rule");
        this.Elimination = GameObject.Find("Elimination");

        Finish = GetComponent<AudioSource>();

    }

    //Green HpGauge
    public void IncreaseGreenF1Hp() //Food1을 먹으면 0.1만큼 상승
    {
        this.GreenHp.GetComponent<Image>().fillAmount += 0.1f;


        if (this.GreenHp.GetComponent<Image>().fillAmount >= 1)
        {
            GreenWin();
        }
    }
    public void IncreaseGreenF2Hp() //Food2을 먹으면 0.15만큼 상승
    {
        this.GreenHp.GetComponent<Image>().fillAmount += 0.15f;

        if (this.GreenHp.GetComponent<Image>().fillAmount >= 1)
        {
            GreenWin();
        }
    }
    

    //Pink HpGauge
    public void IncreasePinkF1Hp() //Food1을 먹으면 0.1만큼 상승
    {
        this.PinkHp.GetComponent<Image>().fillAmount += 0.1f;

        if (this.PinkHp.GetComponent<Image>().fillAmount >= 1)
        {
            PinkWin();
        }
    }
    public void IncreasePinkF2Hp() //Food2을 먹으면 0.15만큼 상승
    {
        this.PinkHp.GetComponent<Image>().fillAmount += 0.15f;

        if (this.PinkHp.GetComponent<Image>().fillAmount >= 1)
        {
            PinkWin();
        }
    }
   

    //Bread HpGauge
    public void IncreaseBreadF1Hp() //Food1을 먹으면 0.1만큼 상승
    {
        this.BreadHp.GetComponent<Image>().fillAmount += 0.1f;

        if (this.BreadHp.GetComponent<Image>().fillAmount >= 1)
        {
            BreadWin();
        }
    }
    public void IncreaseBreadF2Hp() //Food2을 먹으면 0.15만큼 상승
    {
        this.BreadHp.GetComponent<Image>().fillAmount += 0.15f;

        if (this.BreadHp.GetComponent<Image>().fillAmount >= 1)
        {
            BreadWin();
        }
    }

    public void GreenElimination()
    {
        isGreenElimination = true;
        
        this.Elimination.GetComponent<TextMeshProUGUI>().text = "초록젤리 감점( ´△｀)";
        this.GreenHp.GetComponent<Image>().fillAmount -= 0.1f;

    }
    public void PinkElimination()
    {
        isPinkElimination = true;
        this.Elimination.GetComponent<TextMeshProUGUI>().text = "분홍젤리 감점( ´△｀)";
        this.PinkHp.GetComponent<Image>().fillAmount -= 0.1f;
    }
    public void BreadElimination()
    {
        isBreadElimination = true;
        this.Elimination.GetComponent<TextMeshProUGUI>().text = "잼젤리 감점( ´△｀)";
        this.BreadHp.GetComponent<Image>().fillAmount -= 0.1f;
    }


    //Winner Jelly UI 
    void GreenWin()
    {
        isGreenWin = true;
        // 승자 UI에 표시
        this.Winner.GetComponent<TextMeshProUGUI>().text = "초록젤리 승!";

        //게임 종료 후 이긴 젤리 제외 없어짐, 승자 젤리 살짝 rotation
        JellyGreen.GetComponent<JellyController>().RotateGreenJelly();
        
        this.JellyPink.SetActive(false);
        this.JellyBread.SetActive(false);

        if (Finish != null)
        {
            Finish.Play();
        }
    }

   

    void PinkWin()
    {
        isPinkWin = true;
        // 승자 UI에 표시
        this.Winner.GetComponent<TextMeshProUGUI>().text = "분홍젤리 승!";

        //게임 종료 후 이긴 젤리 제외 없어짐, 승자 젤리 살짝 rotation
        JellyPink.GetComponent<JellyPinkController>().RotatePinkJelly();

        
        this.JellyGreen.SetActive(false);
        this.JellyBread.SetActive(false);

        if (Finish != null)
        {
            Finish.Play();
        }
    }
  

    void BreadWin()
    {
        isBreadWin = true;
        // 승자 UI에 표시
        this.Winner.GetComponent<TextMeshProUGUI>().text = "잼젤리 승!";

        //게임 종료 후 이긴 젤리 제외 없어짐, 승자 젤리 살짝 rotation
        JellyBread.GetComponent<JellyBreadController>().RotateBreadJelly();
        this.JellyGreen.SetActive(false);
        this.JellyPink.SetActive(false);

        if (Finish != null)
        {
            Finish.Play();
        }
    }
 

    void Update()
    {
        //룰 설명 창 
        this.Rule.GetComponent<TextMeshProUGUI>().text = "게이지를 먼저 채운 사람이 승자\n초록 젤리: 방향키 조작\n핑크 젤리: 마우스 조작, 스트롤로 상하 이동 \n잼 젤리: WASD키 조작";

    }
}
