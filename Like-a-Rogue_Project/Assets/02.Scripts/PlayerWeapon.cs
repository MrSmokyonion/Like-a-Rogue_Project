using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Vector3 followPos;
    public Transform parent;

    [SerializeField] private WeaponSpawner _weaponSpawner; //웨폰 스탯 받아오기
    [SerializeField] private WeaponStat _weaponStat; //웨폰 스탯 받아오기
    public WeaponStat WeaponStat
    {
        set { _weaponStat = value; }
    }

    [SerializeField]
    private PlayerWeapon _playerweapon;
    public PlayerWeapon playerweapon // 자기 자신 오브젝트를 할당
    {
        set { _playerweapon = value; }
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
    
    public GameObject[] playerWeaponPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        WeaponSpawner spawner = GameObject.Find("WeaponSpawner").GetComponent<WeaponSpawner>();
        this.transform.localPosition = new Vector3(1.2f, -0.85f, 0);
        
        /*
        
        //Instantiate(playerWeaponPrefab[spawner.r], transform.position, transform.rotation);

        GameObject.Find("WeaponSpawner").GetComponent<WeaponType>();
        */
        Instantiate(playerWeaponPrefab[0]).GetComponent<global::PlayerWeapon>();
        
        //playerWeaponPrefab[spawner.r] = Instantiate(playerWeaponPrefab[spawner.r], transform.position, transform.rotation);
        //Instantiate(playerWeaponPrefab[spawner.r]).GetComponent<global::PlayerWeapon>();


    }


    // Update is called once per frame
    void Update()
    {

    }
    
}

