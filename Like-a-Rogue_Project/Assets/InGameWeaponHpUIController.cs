using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameWeaponHpUIController : MonoBehaviour
{
    [Header("UIs")]
    public Image m_UI_MainWeapon;
    public Image m_UI_SubWeapon;
    [SerializeField] public Image[] m_UI_hpContainer;

    [Header("Sprite Resources")]
    public Sprite m_sprite_fullHeart;
    public Sprite m_sprite_halfHeart;
    public Sprite m_sprite_emptyHeart;

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
            int temp = _hp - (i*2);
            if (temp <= 0)
            {
                m_UI_hpContainer[i].sprite = m_sprite_emptyHeart;
            }
            else if (temp < 2)
            {
                m_UI_hpContainer[i].sprite = m_sprite_halfHeart;
            }
            else if (temp >= 2)
            {
                m_UI_hpContainer[i].sprite = m_sprite_fullHeart;
            }
        }
    }
}
