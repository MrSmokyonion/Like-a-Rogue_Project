using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float jump;
    public float maxSpeed;  //최대 속도 설정
    public static Rigidbody2D rigid;
    
    int _playerLayer, _groundLayer, _downLayer;

    private bool downJump = false;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        _playerLayer = LayerMask.NameToLayer("Player");
        _groundLayer = LayerMask.NameToLayer("Platform");
        _downLayer = LayerMask.NameToLayer("DownPlatform");

    }

    void Start()
    {
        
    }

    private void FixedUpdate()   //연속성 스크립트
    {
        // 이동 속도
        float h = Input.GetAxisRaw("Horizontal");  // x축(horizontal)에 가상의 입력값을 받아줄 변수 h 
        
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);  // rigidbody의 x축 벡터에 속도값 h 더해주기
        
        if (rigid.velocity.x > maxSpeed)    // 속력이 maxSpeed보다 높아지지 않게 조정 
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) 
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        
    }

    // Update is called once per frame
    void Update()  //단발성
    {
        // 급정지
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); //A,D에서 손을 뗄 떼 속력 감소시켜주기
        }
        //점프
        Jump();

    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Jump") && rigid.velocity.y == 0) // Jump키(스페이스바)와 S(아래)를 y축 속도가 0일 때 누르면 다운점프
            downJump = true;

        else if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0) //Jump키(스페이스바)를 y축 속도가 0일 때 누르면 점프
                rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);

        if(rigid.velocity.y > 0)   
        Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, true); // Platform 레이어에서 충돌무시
        else
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false); // Platform 레이어에서 충돌


    }
    /*
    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Platform"))
        {
            Debug.Log("sdaff");
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false); //충돌
        }
    }
    */
}
