using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    /// <summary>
    /// 오브젝트 종류 3가지
    /// 1.다운점프 안되는 오브젝트  
    /// 2.다운점프 되는 오브젝트   //DownPlatform
    /// 3.아예 통과 안되는 오브젝트 
    ///  == 플랫폼 종류 3가지 
    /// </summary>
    private float platform;
    
    int _playerLayer, _groundLayer, _downLayer;

    public Transform head;
    // Start is called before the first frame update
    void Start()
    {
        _playerLayer = LayerMask.NameToLayer("Player"); //7
        _groundLayer = LayerMask.NameToLayer("Platform"); //6
        _downLayer = LayerMask.NameToLayer("DownPlatform"); // 11
    }

    // Update is called once per frame
    void Update()
    {
        if (head.transform.position.y > this.transform.position.y)
        {
            if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Jump")) // Jump키(스페이스바)와 S(아래)를 y축 속도가 0일 때 누르면 다운점프
            {
                gameObject.layer = 11;  
            }     
        }
        else
        {
            gameObject.layer = 6;
        }
        Physics2D.IgnoreLayerCollision(_playerLayer, _downLayer, true);
    }
}