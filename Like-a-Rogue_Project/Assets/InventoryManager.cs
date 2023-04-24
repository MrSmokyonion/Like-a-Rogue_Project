using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;

    public List<WeaponStat> m_weaponItems;

    [SerializeField] private List<InventoryItemButton> m_InventorySlots;

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
}