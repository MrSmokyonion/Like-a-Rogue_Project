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
    
    int _playerLayer, _groundLayer;

    public Transform root;
    // Start is called before the first frame update
    void Start()
    {
        _playerLayer = LayerMask.NameToLayer("Player");
        _groundLayer = LayerMask.NameToLayer("Platform");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Jump")) // Jump키(스페이스바)와 S(아래)를 y축 속도가 0일 때 누르면 다운점프
        {
            if (root.transform.position.y > this.transform.position.y)
            {
                Physics2D.IgnoreLayerCollision(_playerLayer,_groundLayer, true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false);
            }
        }
}
    }
