using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;

    public int m_equitMainWeaponIndex = 0;
    public int m_equitSubWeaponIndex = 1;
    public List<WeaponStat> m_weaponItems;

    [SerializeField] private List<InventoryItemButton> m_InventorySlots;

    private InGameWeaponHpUIController m_ingameUIController;
    private EquitWeaponUIController m_equitWeaponUIController;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_ingameUIController = FindObjectOfType<InGameWeaponHpUIController>();
        m_equitWeaponUIController = FindObjectOfType<EquitWeaponUIController>();

        FreshInventorySlots();
    }

    public void FreshInventorySlots()
    {
        int i = 0;
        for (; i < m_weaponItems.Count && i < m_InventorySlots.Count; i++)
        {
            m_InventorySlots[i].AddItemToSlot(m_weaponItems[i]);
        }

        for (; i < m_InventorySlots.Count; i++)
        {
            m_InventorySlots[i].RemoveItemFromSlot();
        }
    }

    public void AddItem(WeaponStat _weapon)
    {
        if (m_weaponItems.Count < m_InventorySlots.Count)
        {
            m_weaponItems.Add(_weapon);
            FreshInventorySlots();
        }
        else
        {
            Debug.Log("Inventory is FULL!");
        }
    }

    public void RemoveItem(WeaponStat _weapon)
    {
        m_weaponItems.Remove(_weapon);
        FreshInventorySlots();
    }

    public void RemoveItemByNumber(int _i)
    {
        if (m_weaponItems.Count > 0)
        {
            m_weaponItems.RemoveAt(_i);
            FreshInventorySlots();
        }
        else
        {
            Debug.Log("Inventory is Empty!");
        }
    }
    
    public void SwapWeaponMainSub()
    {
        //데이터 상의 스왑
        (m_equitMainWeaponIndex, m_equitSubWeaponIndex) = (m_equitSubWeaponIndex, m_equitMainWeaponIndex);
        
        //UI 상의 스왑
        m_ingameUIController.Do_SwapWeaponUI();
        m_equitWeaponUIController.Do_SwapWeaponMainSub();
        
        //캐릭터 애니메이션 상의 스왑
    }
    
    //Equit main 
    //Equit sub
}

//TODO :
//- 캐릭터 애니메이션 스왑 연동