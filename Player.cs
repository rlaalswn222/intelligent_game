using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis;
    public float speed;
    public int itemCount;
    public float jumpPower = 5;
    public float moveSpeed = 5; // 이동 속도 변수
    public TextMeshProUGUI gameOverText;
    bool isJump;
    Rigidbody rigidplayer; //변수 선언
    AudioSource eatAudio;


    Animator anim;
    Vector3 moveVec;

    //난이도 조절
    float startTime; // 게임 진행 누적 시간
    public float accelerationRate = 0.3f; // 속도 증가율 변수 추가

    void Awake()
    {
        //그냥 GetComponent는 애니메이터가 자식 오브젝트라서 못 갖고옴
        anim = GetComponentInChildren<Animator>();

        eatAudio = GetComponent<AudioSource>();
        isJump = false;
        rigidplayer = GetComponent<Rigidbody>();
        rigidplayer.freezeRotation = true; // 물리 상호작용으로 인해 회전하지 않도록 설정
    }

    void Start()
    {
        startTime = Time.time; // 게임 시작 시간 기록
        speed = moveSpeed; // 초기 속도로 설정

        // Game Over 텍스트
        gameOverText = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime; // 경과 시간 계산

        // 3초가 지난 후 속도를 점진적으로 증가 난이도 조절 
        if (elapsedTime > 3f)
        {
            speed = moveSpeed + (elapsedTime - 3f) * accelerationRate;
        }

        hAxis = Input.GetAxisRaw("Horizontal"); //InputManager에서 관리

        // 앞으로 이동
        Vector3 forwardMove = transform.forward;

        // 좌우 이동
        Vector3 sideMove = new Vector3(hAxis, 0, 0);

        // 벡터 합침
        moveVec = (forwardMove + sideMove).normalized;

        // 이동
        transform.position += moveVec * speed * Time.deltaTime;

        // 애니메이션 설정
        anim.SetBool("isRun", moveVec != Vector3.zero); // 파라미터 값 설정

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigidplayer.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
        
    }

    //무한 점프 금지 함수 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false; //plane에 닿으면 isJump값을 false로
        }
    }

    //충돌하게 되면 실행될 코드
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item") //바나나 아이템 
        {
            itemCount++; //Player 클래스를 player로 선언해주고 itemCount 변수를 숫자 올려줌
            eatAudio.Play();
            other.gameObject.SetActive(false); //충돌 시 tag가 Item인 바나나와 수박 아이콘이 사라짐
        }
        if (other.tag =="Item2") //수박 아이템
        {
            itemCount += 2;
            eatAudio.Play();
            other.gameObject.SetActive(false);
        }
        if (other.name == "Dive")
        {
            DiveGameOver();
        }
        if (other.tag == "Obstacle")
        {
            CrushGameOver();
        }
        if (other.tag == "FinalLine")
        {
            GameOver();
        }
        
    }
    void CrushGameOver()
    {
        gameOverText.text = "충돌!! Game Over";
        gameOverText.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    void DiveGameOver()
    {
        gameOverText.text = "추락!! Game Over";
        gameOverText.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    void GameOver()
    {
        gameOverText.text = "결승선 통과 점수를 확인해주세요";
        gameOverText.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}

