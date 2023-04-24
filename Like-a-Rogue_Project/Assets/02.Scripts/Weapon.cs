using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform target;

    public Vector2 direction;

    private float velocity;

    private float accelaration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player").transform; // 플레이어의 현재 위치
        // direction = (target.position - transform.position).normalized;  //플레이어와의 거리를 단위벡터화
        float distance = Vector2.Distance(target.position, transform.position); // 플레이어와의 거리

        if (distance <= 5.0f) // 거리가 5.0이하일 때 상호작용 메시지
        {
            
        }
       
        
        
        /* 플레이어 위치 추적
        accelaration = 0.1f;
        velocity = (velocity + accelaration * Time.deltaTime);
        float distance = Vector3.Distance(target.position, transform.position);
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

    
}
