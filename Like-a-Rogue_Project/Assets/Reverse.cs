using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    public GameObject normalBackground;
    public GameObject reverseBackground;
    [SerializeField] private int reverseCount = 1;


    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // Ctrl 키가 입력되었을 때 동작
            if (normalBackground.activeSelf)
            {
                normalBackground.SetActive(false);
                reverseBackground.SetActive(true);
            }
            else if (!normalBackground.activeSelf)
            {
                normalBackground.SetActive(true);
                reverseBackground.SetActive(false);
            }

            reverseCount = 0;
        }
        
        if (reverseCount == 0)
        {
            // reverseCount 값이 0일때 동작 X
            return;
        }
        //카운트 회복은 포탈쪽에서 처리하는게 좋을거라고 생각 - 포탈쪽에서 적의 수를 확인하기 때문
    }
}
