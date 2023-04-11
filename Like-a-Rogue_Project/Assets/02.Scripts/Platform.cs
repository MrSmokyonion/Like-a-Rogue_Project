using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform root; // 플레이어 발 위치 (0, -0.75f, 0)  머리위치 (0, 0.75f, 0)

    int _playerLayer, _groundLayer, _downLayer;
    // Start is called before the first frame update
    void Start()
    {
        _playerLayer = LayerMask.NameToLayer("Player");
        _groundLayer = LayerMask.NameToLayer("Platform"); // 6
        _downLayer = LayerMask.NameToLayer("DownPlatform"); // 14
    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false); // Platform 레이어에선 충돌
        Physics2D.IgnoreLayerCollision(_playerLayer, _downLayer, true); // downPlatform 레이어에선 충돌 무시

        if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Jump")) // Jump키(스페이스바)와 S(아래)를 y축 속도가 0일 때 누르면 다운점프
        {
            gameObject.layer = 14;
        }
        else if (transform.position.y > root.transform.position.y)
            gameObject.layer = 6;
    }
}
