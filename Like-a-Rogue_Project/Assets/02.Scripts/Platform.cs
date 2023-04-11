using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform root; // �÷��̾� �� ��ġ (0, -0.75f, 0)  �Ӹ���ġ (0, 0.75f, 0)

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
        //Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false); // Platform ���̾�� �浹
        Physics2D.IgnoreLayerCollision(_playerLayer, _downLayer, true); // downPlatform ���̾�� �浹 ����

        if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Jump")) // JumpŰ(�����̽���)�� S(�Ʒ�)�� y�� �ӵ��� 0�� �� ������ �ٿ�����
        {
            gameObject.layer = 14;
        }
        else if (transform.position.y > root.transform.position.y)
            gameObject.layer = 6;
    }
}
