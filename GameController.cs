using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Resources;

public class GameController : MonoBehaviour
{
    public GameObject player;  // Reference to the Player GameObject
    private Player playerScript;
    public TextMeshProUGUI resourceText;

    void Start()
    {
        // "Player" 게임 오브젝트 찾기
        this.player = GameObject.Find("Player");

    }
    void Update()
    {
        // Player 스크립트에서 itemCount 값을 가져옴
        int itemCount = this.player.GetComponent<Player>().itemCount;

        // itemCount 값을 텍스트로 표시
        this.resourceText.text = "현재점수: " + itemCount.ToString("F0")+ "점 ";
    }
}
