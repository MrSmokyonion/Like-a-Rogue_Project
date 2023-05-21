using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    [SerializeField] private float m_IntroSpeed = 0.1f;
    [SerializeField] private Sprite[] m_spriteArr;
    private SpriteRenderer m_spriteRenederer;

    private void Start()
    {
        m_spriteRenederer = GetComponent<SpriteRenderer>();
        StartCoroutine(OnIntroStart());
    }

    private IEnumerator OnIntroStart()
    {
        while (true)
        {
            foreach (Sprite _sprite in m_spriteArr)
            {
                m_spriteRenederer.sprite = _sprite;
                yield return new WaitForSeconds(m_IntroSpeed);
            }
        }
    }
}
