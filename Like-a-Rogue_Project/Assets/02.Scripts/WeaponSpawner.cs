using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    Sword,
    GreatSword,
    Spear,
    Axe,
    ThrowingAxe,
    BattleAxe,
    Sickle,
    Bow,
    Crossbow,
    MagicStick,
    MagicBall,
    MagicBook
}

public class WeaponSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private List<WeaponStat> _weaponStats;
    //[SerializeField] private GameObject[] weaponPrefab = new GameObject[5];
    public GameObject[] weaponPrefab; // 무기 프리팹

    public GameObject[] playerWeaponPrefab; // 플레이어 무기 프리팹
    //private List<GameObject> PrefabList = new List<GameObject>();   //무기 프리팹 리스트
    //[SerializeField] private GameObject weaponPrefab; // 웨폰 프리팹을 무기 번호에 맞게 교체해주자

    public SpriteRenderer spriteRenderer;

    public int r; // 무기 랜덤스폰
    
    public Transform hand;
    
    
    void Start()
    {
        int r = Random.Range(1, _weaponStats.Count); // 무기 랜덤 출력
        
        hand = GameObject.Find("hand").transform; // 플레이어 손 위치 받아오기
        
        //Instantiate(weaponPrefab[0], hand.transform.position, transform.rotation); // 기본 검 장착
        
        /*
        for (int i = 0; i < _weaponStats.Count; i++)
        {
            var weapon = SpawnWeapon((WeaponType)i);
            weapon.WatchWeaponInfo();
        }
        */
        
        var weapon = SpawnWeapon((WeaponType)r); 
        //weapon.WatchWeaponInfo();
        
        
        //spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public Weapon SpawnWeapon(WeaponType type)
    {
        //var newWeapon = Instantiate(weaponPrefab[5]).GetComponent<global::Weapon>();
        var newWeapon = Instantiate(weaponPrefab[r]).GetComponent<global::Weapon>(); //암시적 타입 지역변수 var
        newWeapon.WeaponStat = _weaponStats[(int)type];
        return newWeapon;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}