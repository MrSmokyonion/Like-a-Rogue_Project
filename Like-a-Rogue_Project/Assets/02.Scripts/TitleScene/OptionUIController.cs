using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionUIController : MonoBehaviour
{
    public UnityEvent m_Event_OnOptionBack;

    public void OpenOption()
    {
        gameObject.SetActive(true);
    }
    
    public void Btn_Back()
    {
        m_Event_OnOptionBack.Invoke();
        gameObject.SetActive(false);
    }
}
