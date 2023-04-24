using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public int hp = 5; // 피격 5회 당할 시 게임 오버 + 맵 클리어 시 1회복 + 보스 클리어 시  3회복
    public float jump;
    private float speed = 8; // 기본속도
    public static Rigidbody2D rigid;

    int _playerLayer, _groundLayer;

    private bool downJump = false;
    // Start is called before the first frame update
    
    private bool isDash;
    private float dashSpeed = 30; //대쉬 속도 
    public float dashTime; // 대쉬를 하고있는 시간
    public float defaultTime;
    private float defaultSpeed; // 현재 속도

    private bool isAttack;

    private SpriteRenderer renderer;
    
    /// <summary>
    /// 대시 구현
    /// 1. 대시는 A 또는 D(좌우이동)를 빠르게 2번 (0.8초?)눌렀을 때 발동 
    /// 2. 대시 중에는 무적판정
    /// 3. 대시 간의 쿨타임
    /// </summary>
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        _playerLayer = LayerMask.NameToLayer("Player");
        _groundLayer = LayerMask.NameToLayer("Platform");

        defaultSpeed = speed;
    }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()   //연속성 스크립트
    {
        // 이동 속도
        float h = Input.GetAxis("Horizontal");  // x축(horizontal)에 가상의 입력값을 받아줄 변수 h
        //GetAxis는 -1.0f~1.0f사이의 값을 부드럽게 받아옴, GetAxisRaw는 -1,0,1의 값을 받으므로 스킬구현에 좋음

        rigid.velocity = new Vector2(h * defaultSpeed, rigid.velocity.y); // 기본 이동
        

        
    }

    // Update is called once per frame
    void Update()  //단발성
    {
        // 급정지
        Stop();
        //점프
        Jump();
        //대쉬
        Dash();
        //공격
        Attack();
        
    }

    
    void Stop()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); //A,D에서 손을 뗄 떼 속력 감소시켜주기
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Jump") && rigid.velocity.y == 0) // Jump키(스페이스바)와 S(아래)를 y축 속도가 0일 때 누르면 다운점프
            downJump = true;
        else if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0) //Jump키(스페이스바)를 y축 속도가 0일 때 누르면 점프
                rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        
        
        //점프상태에서 블록 collider 무시
        if (rigid.velocity.y > 0 || downJump == true)
            Physics2D.IgnoreLayerCollision(_playerLayer,_groundLayer, true);
        else 
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false); //충돌
        
    }
    void Dash()
    {
        if (Input.GetMouseButtonDown(1)) //마우스 오른쪽 버튼 -> 대쉬
            isDash = true;

        if (dashTime <= 0)
        {
            gameObject.layer = 7; //무적 x
            defaultSpeed = speed;
            if (isDash)
                dashTime = defaultTime;
        }
        else
        {
            gameObject.layer = 8; // 무적 (피격 x)
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
        }
        isDash = false;
    }
    void Attack()
    {
        if (Input.GetMouseButton(0)) //마우스 왼쪽 버튼  -> 일반 공격
        {
            isAttack = true;
        }
 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(); //피격판정
        }
        if (collision.gameObject.tag == "Weapon")
        {
            
        }
    }

    void OnDamaged()
    {
        hp = hp - 1;
        gameObject.layer = 8; //플레이어 피격 시 무적(몹 충돌 무시)
        renderer.color = new Color(1, 1, 1, 0.4f);
        
        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 7; //무적 해제
        renderer.color = new Color(1, 1, 1, 1);
    }
}
