using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameWeaponHpUIController : MonoBehaviour
{
    [Header("UIs")]
    public Image m_UI_MainWeapon;
    public Image m_UI_SubWeapon;
    [SerializeField] public Image[] m_UI_hpContainer;
    [SerializeField] public Image[] m_UI_hpContainer2;

    [Header("Sprite Resources")]
    public Sprite m_sprite_fullHeart;
    public Sprite m_sprite_emptyHeart;

    public Player m_playerRef;

    private void Start()
    {
        m_playerRef = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    private void Update()
    {
        if (m_playerRef != null)
        {
            Do_UpdateHpStatus(m_playerRef.hp);
            if (m_playerRef.hp <= 0)
            {
                GameController._instance.GameOver();
            }
        }
    }

    public void Do_SwapWeaponUI()
    {
        var mainSprite = m_UI_MainWeapon.sprite;
        var subSprite = m_UI_SubWeapon.sprite;
        m_UI_MainWeapon.sprite = subSprite;
        m_UI_SubWeapon.sprite = mainSprite;
    }

    public void Do_UpdateHpStatus(int _hp)
    {
        for (int i = 0; i < m_UI_hpContainer.Length; i++)
        {
            int temp = _hp - (i);
            if (temp <= 0)
            {
                m_UI_hpContainer[i].sprite = m_sprite_emptyHeart;
            }
            else if (temp >= 1)
            {
                m_UI_hpContainer[i].sprite = m_sprite_fullHeart;
            }
        }
        
        for (int i = 0; i < m_UI_hpContainer2.Length; i++)
        {
            int temp = _hp - (i);
            if (temp <= 0)
            {
                m_UI_hpContainer2[i].sprite = m_sprite_emptyHeart;
            }
            else if (temp >= 1)
            {
                m_UI_hpContainer2[i].sprite = m_sprite_fullHeart;
            }
        }
    }
}
