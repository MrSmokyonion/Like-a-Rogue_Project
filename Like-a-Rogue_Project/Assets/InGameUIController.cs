using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private GameObject m_PlayerStatusUI;
    [SerializeField] private GameObject m_ResumeUI;

    private bool m_isInventoryPopUp = false;

    private void Update()
    {
        PopUpInventory();
    }

    private void PopUpInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DOTweenAnimation dot1 = m_InventoryUI.GetComponent<DOTweenAnimation>();
            DOTweenAnimation dot2 = m_PlayerStatusUI.GetComponent<DOTweenAnimation>();
            DOTweenAnimation dot3 = m_ResumeUI.GetComponent<DOTweenAnimation>();
            if (m_isInventoryPopUp)
            {
                dot1.DORestartById("POPDOWN");
                dot2.DORestartById("POPDOWN");
                dot3.DORestartById("POPDOWN");
                m_isInventoryPopUp = false;
            }
            else
            {
                dot1.DORestartById("POPUP");
                dot2.DORestartById("POPUP");
                dot3.DORestartById("POPUP");
                m_isInventoryPopUp = true;
            }
        }
    }
}