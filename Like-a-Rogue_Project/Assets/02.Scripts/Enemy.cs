using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;

// ctrl+R+R로 변수 일괄 변경가능
public class Enemy : MonoBehaviour
{
    public float speed;
    public int hp;

    private Rigidbody2D rigid;

    private int nextMove; //적이 이동하는 방향 랜덤 값

    private Animator anim;

    public Transform player; // 목표대상의 좌표
    
    private float velocity;

    private float accelaration; // 몬스터 위치추적에 쓰일 가속화 변수

    // public Player target; // 플레이어를 타겟으로

    public GameObject[] monsterType; // 몬스터 종류
    // Start is called before the first frame update

    private SpriteRenderer render;

    private int platformLayer;

    private bool isDeath = false;

    private bool isAttack = false;
    private float attackTime;
    private float defaultTime = 0.967f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("Move", 3); // 이동 방향 변경

        render = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() //움직임 제어
    {
        //Move();
        Attack();
        //Hit();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // rigid.velocity = Vector2.zero;

       
    }

    void Move()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y); //기본 이동


        if (rigid.velocity.x < 0)
        {
            render.flipX = false;
        }
        else if (rigid.velocity.x > 0)
            render.flipX = true;


        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 1.1f, rigid.position.y); //현재 벡터에서 nextMove*0.5f만큼 앞에 ray를 표시


        platformLayer = LayerMask.NameToLayer("Platform"); //6
        
        Debug.DrawRay(frontVec, Vector3.down * 1.5f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));  // 에너미 콜리더의 offset이 0.03이상이어야 애니메이션 실행 (왠지 모름)
        if (rayHit.collider == null) // ray가 아무것도 닿지 않았을 때 -> 플랫폼 콜리더에 닿지 않았을 때
        {
            //Debug.Log(rayHit.collider);
            nextMove *= -1; //방향 바꿔주기 
            CancelInvoke(); //인보크를 빠져나오는 함수
            Invoke("MoveDir", 3);
        }


        /* 플레이어 위치 추적
         
         target = GameObject.Find("Player").transform; // 플레이어의 현재 위치
         direction = (target.position - transform.position).normalized;  //플레이어와의 거리를 단위벡터화
        
        accelaration = 0.1f;
        velocity = (velocity + accelaration * Time.deltaTime);
        float distance = Vector2.Distance(target.position, transform.position); // 플레이어와의 거리
        if (distance <= 5.0f)
        {
            this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                transform.position.y + (direction.y * velocity), transform.position.z);
        }
        else
        {
            velocity = 0;
        }
        */
    }
    void MoveDir() //이동방향 결정
    {
        nextMove = Random.Range(-1, 2);

        if (nextMove == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        Invoke("MoveDir", 3);
    }

    void Attack()
    {
        isAttack = true;
        attackTime -= Time.deltaTime; //공격 쿨타임
        if (attackTime <= 0)
        {
            player = GameObject.Find("Player").transform; // 플레이어의 현재 위치
            // direction = (target.position - transform.position).normalized;  //플레이어와의 거리를 단위벡터화
            float distance = Vector2.Distance(player.position, transform.position); // 플레이어와의 거리

            
            if (distance <= 2.0f && !isDeath) // 거리가 2이하일 때 공격
            {
                anim.SetTrigger("isAttack");
            }
            if (isAttack)
                attackTime = defaultTime; 
        }
        else
            attackTime -= Time.deltaTime; //공격 쿨타임
    }

    void Hit()
    {
        if (hp > 0)
        {
            anim.SetTrigger("isHit");
            hp = hp - 1; // 추후 무기 데미지만큼 감소하는걸로 변경
        }
        else if (hp <= 0)
        {
            StartCoroutine(Death());
        }
    }
   

    IEnumerator Death()  // 사망처리  -> 사망모션 후 오브젝트 파괴 + 움직임 멈춤 + 피격 X  
    {
        anim.SetTrigger("isDeath");
        isDeath = true;
        yield return new WaitForSeconds(0.5f);
        if (isDeath == true)
        {
            speed = 0; // 움직임 멈춤
                                            //피격 방지
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            anim.ResetTrigger("isHit");
            anim.ResetTrigger("isAttack");
        }
        
        

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        
        //this.gameObject.SetActive(false);
        //Destroy(gameObject);
    }


    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Hit();
        }
    }
    

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("touch");
            Hit();
        }
    }
    */
}
