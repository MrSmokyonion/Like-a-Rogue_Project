using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlackBarUI : MonoBehaviour
{
    public DOTweenAnimation m_TopBar;
    public DOTweenAnimation m_DownBar;

    public void Do_BlackBar_On()
    {
        m_TopBar.gameObject.SetActive(true);
        m_DownBar.gameObject.SetActive(true);
        m_TopBar.DORestartById("On");
        m_DownBar.DORestartById("On");
    }

    public void Do_BlackBar_Off()
    {
        m_TopBar.DORestartById("Off");
        m_DownBar.DORestartById("Off");
    }
}
