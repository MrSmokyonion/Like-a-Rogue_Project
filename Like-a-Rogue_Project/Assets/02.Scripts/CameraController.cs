using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //플레이어 위치
    public float cameraSpeed = 5.0f; //카메라 이동속도

    public Vector2 center; //카메라 중간위치
    public Vector2 size; //카메라 크기

    private float height; //세로 넓이

    private float width;  //가로 넓이
    

    void Start()
    {
        height = Camera.main.orthographicSize;  //세로길이의 절반크기
        width = height * Screen.width / Screen.height; //가로길이의 절반크기 구하기

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center,size);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() //update뒤에 실행되는 함수
    {
        
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime *cameraSpeed); // 선형보간으로 플레이어 추적

        float lx = size.x * 0.5f - width; // 플레이어의 현재 x값에서 맵 가로길이를 뺸 값 (width값이 가로길이의 절반이므로 플레이어 x값에 0.5를 곱함)
        // lx + center.x == 최댓값, -lx + center.x == 최솟값
        float clampX = Mathf.Clamp(transform.position.x, -lx +center.x, lx + center.x); //value값이 min과 max사이면 value, min보다 작으면 min, max보다 크면 max를 반환
        
        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly +center.y, ly + center.y);
        
        transform.position = new Vector3(clampX, clampY, -10f); // clamp를 적용한 값 적용 + 카메라 z값 고정
        
    
        if (Input.GetKey(KeyCode.W) && Player.rigid.velocity.y == 0) // W(위) 누를 때 카메라 조정
        {
            transform.Translate(Vector3.up * 3f);
        }
        else if (Input.GetKey(KeyCode.S) && Player.rigid.velocity.y == 0) // S(아래) 누를 때 카메라 조정
        {
            transform.Translate(Vector3.down * 3f);
        }
    }
}
