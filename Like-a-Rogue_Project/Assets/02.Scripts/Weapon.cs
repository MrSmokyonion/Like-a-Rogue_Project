using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponStat _weaponStat;  //웨폰 스탯 받아오기
    public  WeaponStat WeaponStat
    {
        set { _weaponStat = value; }
    }

    [SerializeField]
    private Weapon _weapon;
    public Weapon weapon // 자기 자신 오브젝트를 할당
    {
        set { _weapon = value; }
    }
    public Player player;

    /*
    public void WatchWeaponInfo()
    {
        Debug.Log("무기 공격력 :: " + _weaponStat.m_attackDamage);
        Debug.Log("무기 딜레이 :: " + _weaponStat.m_attackDelay);
        Debug.Log("무기 특수공격 공격력 :: " + _weaponStat.m_specialAttackDamage);
        Debug.Log("무기 특수공격 딜레이 :: " + _weaponStat.m_specialAttackDelay);
    }
    */
    
    public Transform target; // 플레이어 위치 받아오기

    public Vector2 direction;

    private bool isSetting; //무기 장착여부
    public Transform hand;
    private int _weaponLayer, _weaponItemLayer; // 12, 13
    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj = Resources.Load<GameObject>("Weapon/N_01_sword_normal");
        //Instantiate(obj);
        _weaponItemLayer = LayerMask.NameToLayer("Weapon_Item");
        _weaponLayer = LayerMask.NameToLayer("Weapon");

    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("hand").transform;
        target = GameObject.Find("Player").transform; // 플레이어의 현재 위치
        
        disCheck();
    }

    void disCheck()
    {
        /*
        // direction = (target.position - transform.position).normalized;  //플레이어와의 거리를 단위벡터화
        float distance = Vector2.Distance(target.position, transform.position); // 플레이어와의 거리

        if (distance <= 5.0f) // 거리가 5.0이하일 때 상호작용 메시지
        {
                Debug.Log("dsaf");
        }
        */
    }
    private void Interaction() //상호작용 시   아이템웨폰 프리팹 파괴 -> 플레이어 웨폰 프리팹 껴주기(웨폰_스왑)
    {
        isSetting = true; 
        //transform.position = hand.transform.position;
        //무기 태그를 weapon_item 에서 weapon(착용한 무기)으로 변경
        //상호작용 시 무기 착용(플레이어 손 위치로 무기 이동 + 플립)
        
        
    }

    void Weapon_swap()  //1.무기 장착해주기 2.원래 끼고있던 무기 서브무기로 변경 3.서브무기에 있던 무기는 인벤토리로
    {
        
        
        
        
    }
    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))  //gameObject.tag 로 쓰는거 보다 compareTag가 효율적임 1.동적할당 없이 tag비교 가능 2.실제로 존재하는 태그인지 확인하는 동작도 포함함
            {
                //상호작용 여부 UI
                if (Input.GetKey(KeyCode.F)) //상호작용 F키
                {
                    Interaction();
                    //gameObject.layer = 12;   //레이어 바꾸기 x -> 아이템웨폰이랑 플레이어웨폰 프리팹을 따로 만들어서 레이어 분리
                }
            }
        }  
}

