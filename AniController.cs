using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{

    public float jumpPower = 5;
    public float moveSpeed = 5; // 이동 속도 변수 추가
    public int itemCount;
    bool isJump;
    public Animator anim;
    Rigidbody rigidplayer; //변수 선언
    AudioSource eatAudio;

    void Awake()
    {
        eatAudio = GetComponent<AudioSource>();
        isJump = false;
        rigidplayer = GetComponent<Rigidbody>();
        rigidplayer.freezeRotation = true; // 물리 상호작용으로 인해 회전하지 않도록 설정
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigidplayer.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v).normalized;
        Vector3 newPosition = rigidplayer.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        // 플레이어의 위치를 직접 설정
        rigidplayer.MovePosition(newPosition);
    }

    private void Jump()
    {
        isJump = true;
        rigidplayer.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        anim.SetTrigger("jump");
    }

    //무한 점프 금지 함수 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("Landed on Floor");
            isJump = false; //plane에 닿으면 isJump값을 false로
        }
    }

    //충돌하게 되면 실행될 코드
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++; //Player 클래스를 player로 선언해주고 itemCount 변수를 숫자 올려줌
            eatAudio.Play();
            other.gameObject.SetActive(false); //충돌 시 바나나가 없어

        }
    }
}
