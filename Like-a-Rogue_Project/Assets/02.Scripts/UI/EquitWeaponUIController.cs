using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquitWeaponUIController : MonoBehaviour
{
    public DroppableUI m_mainWeaponSlot;
    public DroppableUI m_subWeaponSlot;

    public void Do_SwapWeaponMainSub()
    {
        (m_mainWeaponSlot.m_imageRenderer.sprite, m_subWeaponSlot.m_imageRenderer.sprite) = (
            m_subWeaponSlot.m_imageRenderer.sprite, m_mainWeaponSlot.m_imageRenderer.sprite);
    }
}
