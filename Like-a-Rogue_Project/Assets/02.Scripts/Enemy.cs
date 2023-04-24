using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// ctrl+R+R로 변수 일괄 변경가능
public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;

    private Rigidbody2D rigid;

    private int nextMove; //적이 이동하는 방향 랜덤 값
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        
        Invoke("Move", 3); // 이동 방향 변경
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y); //기본 이동

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.5f, rigid.position.y); //현재 벡터에서 nextMove*0.5f만큼 앞에 ray를 표시
        
        Debug.DrawRay(frontVec, Vector3.down, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec , Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null) // ray가 아무것도 닿지 않았을 때
        {
            nextMove *= -1; //방향 바꿔주기 
            CancelInvoke(); //인보크를 빠져나오는 함수
            Invoke("Move", 3);
        }
  
    }

    void Move()
    {
        nextMove = Random.Range(-1, 2);
        
        Invoke("Move", 3);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
