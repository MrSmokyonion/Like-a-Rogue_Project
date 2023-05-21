using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform m_itemDetailUI;
    
    [SerializeField] private bool m_isItemExist = false;

    [SerializeField] private Sprite m_weaponIcon_none;
    [SerializeField] private Sprite m_weaponIcon_weapon;

    private WeaponStat m_weapon;
    private Image m_ButtonIcon;
    private Image m_WeaponImage;

    private void OnValidate()
    {
        m_ButtonIcon = GetComponent<Image>();
        m_WeaponImage = transform.GetChild(0).GetComponent<Image>();
    }

    //마우스를 버튼 위로 올렸을 때 UI 작업
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_isItemExist) return;  //아이템 존재하는지 체크

        //해당 아이템의 세부 내용 UI 뜨게
        if (Input.GetMouseButton(0))
        {
            return;
        }
        RectTransform rt = GetComponent<RectTransform>();
        m_itemDetailUI.gameObject.SetActive(true);
        //m_itemDetailUI.position = new Vector3(rt.position.x, rt.position.y, m_itemDetailUI.position.z);
        //m_itemDetailUI.anchoredPosition += new Vector2(rt.rect.width / 2*-1, 0);
        Debug.Log("button mouse over : ");
    }

    //마우스를 버튼에서 밖으로 뺐을 때 UI 작업
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!m_isItemExist)
            return;
        
        //아이템 세부 UI 없애기

        m_itemDetailUI.gameObject.SetActive(false);
        Debug.Log("button mouse exit");
    }

    public void AddItemToSlot(WeaponStat _weapon)
    {
        m_weapon = _weapon;
        m_isItemExist = true;
        //m_ButtonIcon.sprite = m_weaponIcon_weapon;
        m_WeaponImage.gameObject.SetActive(true);
    }
    
    public void RemoveItemFromSlot()
    {
        m_weapon = null;
        m_isItemExist = false;
        //m_ButtonIcon.sprite = m_weaponIcon_none;
        m_WeaponImage.gameObject.SetActive(false);
    }

    public void SetActiveItemDetailUI(bool _b = false)
    {
        m_itemDetailUI.gameObject.SetActive(_b);
    }
}
