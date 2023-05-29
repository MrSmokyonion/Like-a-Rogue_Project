using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public int hp = 5; // 피격 5회 당할 시 게임 오버 + 맵 클리어 시 1회복 + 보스 클리어 시  3회복
    public float jump;
    private float speed = 8; // 기본속도
    public static Rigidbody2D rigid;

    int _playerLayer, _groundLayer, _damagedLayer, _enemyLayer;

    private bool downJump = false;
    // Start is called before the first frame update
    
    private bool isDash;
    private float dashSpeed = 50; //대쉬 속도 
    public float dashTime; // 대쉬를 하고있는 시간
    public float defaultTime;
    private float defaultSpeed; // 현재 속도
    
    private SpriteRenderer renderer;

    private Animator anim;

    public Transform hand; //플레이어 손 위치 (무기 위치)
    public Transform root; //플레이어 발 위치

    public GameObject[] weapons;
    public HitUIContainer m_hitUI;

    //private int isAttackNum;
    private bool isAttack = false;
    private float attack1Time;
    private float attack1Speed = 0.333f;

    private float attack2Time;
    private float attack2Speed = 0.5f;
    // private bool weaponSet = false; //무기 장착여부

    [SerializeField] private Transform weaponPos;

    /// <summary>
    /// 대시 구현
    /// 1. 대시는 A 또는 D(좌우이동)를 빠르게 2번 (0.8초?)눌렀을 때 발동 
    /// 2. 대시 중에는 무적판정
    /// 3. 대시 간의 쿨타임
    /// </summary>
    /// 
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        _playerLayer = LayerMask.NameToLayer("Player"); //7
        _groundLayer = LayerMask.NameToLayer("Platform"); //6
        _damagedLayer = LayerMask.NameToLayer("PlayerDamaged"); //8
        _enemyLayer = LayerMask.NameToLayer("Enemy"); //9

        defaultSpeed = speed;

        anim = GetComponent<Animator>();
    }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()   //연속성 스크립트
    {
        /*
        Move();
        // 급정지
        Stop();
        //점프
        Jump();
        */
    }

    // Update is called once per frame
    void Update()  //단발성
    {
        Move();
        // 급정지
        Stop();
        //점프
        Jump();
        //대쉬
        Dash();
        //공격
        StartCoroutine(Attack());

    }

    void Move()
    {
        //hand = GameObject.Find("hand").transform;
        hand = GameObject.Find("_hand").transform;

        // 이동 속도
        float h = Input.GetAxis("Horizontal");  // x축(horizontal)에 가상의 입력값을 받아줄 변수 h

        if (rigid.velocity.x < 0)
        {
            renderer.flipX = true;
            weaponPos.localPosition = new Vector3(MathF.Abs(weaponPos.localPosition.x)*-1, weaponPos.localPosition.y, weaponPos.localPosition.z); 
            weaponPos.localRotation = new Quaternion(0, 180f, 0, 0);
            //hand.transform.localPosition = new Vector3(1.2f *(-1), -0.85f, 0);
            //hand.transform.localPosition = new Vector3(1.8f *(-1), -0.35f, 0);
            //hand.transform.localPosition = new Vector3(1.5f, 0.5f, 0);
        }
        else if (rigid.velocity.x > 0)
        {
            renderer.flipX = false;
            weaponPos.localPosition = new Vector3(MathF.Abs(weaponPos.localPosition.x), weaponPos.localPosition.y, weaponPos.localPosition.z);
            weaponPos.localRotation = new Quaternion(0, 0, 0, 0);
            //hand.transform.localPosition = new Vector3(1.2f, -0.85f, 0);
            //hand.transform.localPosition = new Vector3(1.8f, -0.35f, 0);
            //hand.transform.localPosition = new Vector3(3.7f * (-1), 0.5f, 0);
        }
        
        
        //GetAxis는 -1.0f~1.0f사이의 값을 부드럽게 받아옴, GetAxisRaw는 -1,0,1의 값을 받으므로 스킬구현에 좋음

        rigid.velocity = new Vector2(h * defaultSpeed, rigid.velocity.y); // 기본 이동

        if (rigid.velocity.x != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
            anim.SetBool("isWalk", false);
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
        if (Input.GetButtonDown("Jump"))
            anim.SetBool("isJump", true);
        
        if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Jump") && rigid.velocity.y == 0 ) // Jump키(스페이스바)와 S(아래)를 y축 속도가 0일 때 누르면 다운점프
        {
            downJump = true;
        }
        else if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0 ) //Jump키(스페이스바)를 y축 속도가 0일 때 누르면 점프
        {
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
 
        //점프상태에서 블록 collider 무시
        if (rigid.velocity.y > 0) //플레이어가 점프 중일 시
        {
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, true); 
            Physics2D.IgnoreLayerCollision(_damagedLayer, _groundLayer, true); 
        }

        else
        {
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false); //충돌
            Physics2D.IgnoreLayerCollision(_damagedLayer, _groundLayer, false); //충돌
        }

        
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down*1.5f, new Color(0,1,0 ));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            
            if (rayHit.collider == null)
            {
                if (rayHit.distance < 0.5f)
                {
                    //Debug.Log("test");
                    anim.SetBool("isJump", false); 
                    
                }

            }
        }
        
    }
    void Dash()
    {
        if (Input.GetMouseButtonDown(1)) //마우스 오른쪽 버튼 -> 대쉬
            isDash = true;

        if (dashTime <= 0)
        {
            Physics2D.IgnoreLayerCollision(_playerLayer, _enemyLayer, false);
            defaultSpeed = speed;
            if (isDash)
                dashTime = defaultTime;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(_playerLayer, _enemyLayer, true);
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
        }
        isDash = false;
    }
    
    
    IEnumerator Attack()
    {
        /*
        if (Input.GetMouseButtonDown(0) &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_attack_1") && isAttack == false) //마우스 왼쪽 버튼  -> 일반 공격
            {
                isAttack = true;
                anim.SetTrigger("isAttack_1");
                //attack1Time = attack1Speed; // attack1의 실행시간 -> 디폴트 시간(0.333초)
                //attack1Time -= Time.deltaTime;
                
                //if (attack1Time <= 0)
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    attack2Time = attack2Speed; // attack1이 끝난 후 0.5초 안에 클릭해야 attack2실행   <디폴트 시간 (0.5초)>
                    attack2Time -= Time.deltaTime;
                    if (Input.GetMouseButtonDown(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_attack_2") && isAttack == true &&
                        attack2Time > 0)
                    {
                        //if (Input.GetMouseButtonDown(0))
                        anim.SetTrigger("isAttack_2");
                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                            isAttack = false;
                    }
                }
                //isAttack = false;
            }

        if (Input.GetKey(KeyCode.X)) // X키 -> 특수공격
        {

        }

        yield break;
        */
        
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼  -> 일반 공격
        {
            isAttack = true;
            anim.SetTrigger("isAttack_2");
            //attack1Time = attack1Speed; // attack1의 실행시간 -> 디폴트 시간(0.333초)
            //attack1Time -= Time.deltaTime;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isAttack = false;
            }
            /*
            if (attack1Time >= 0)
            {
                isAttack = true;
                anim.SetTrigger("isAttack_1");
            }
            else
                isAttack = false;
                */
        }
        yield break;
    }

    void Swap()
    {


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnDamaged(); //피격판정
        }
        if (collision.gameObject.CompareTag("Weapon_Item")) //줍기 전 무기
        {
            //상호작용 여부 UI
            //무기 태그를 weapon_item 에서 weapon(착용한 무기)으로 변경
        }
    }

    void OnDamaged()
    {
        m_hitUI.Do_HitAnim();
        
        hp = hp - 1;
        

        if(hp <= 0)
        {
            gameObject.layer = 8;
            StartCoroutine(Death());
        }
        else
        {
            gameObject.layer = 8; //플레이어 피격 시 무적(몹 충돌 무시)
            renderer.color = new Color(1, 1, 1, 0.4f);

            anim.SetTrigger("isHit");
            Invoke("OffDamaged", 2);
        }
    }

    void OffDamaged()
    {
        gameObject.layer = 7; //무적 해제
        renderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator Death()
    {
        anim.SetTrigger("isDeath");

        speed = 0; //움직임 멈춤
        yield return new WaitForSeconds(0.5f);
        
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Time.timeScale = 0;
        }
        
    }
}
