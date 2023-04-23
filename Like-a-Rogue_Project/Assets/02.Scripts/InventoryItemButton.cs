using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool m_isItemExit = false;
    
    //마우스를 버튼 위로 올렸을 때 UI 작업
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_isItemExit)
            return;
        
        Debug.Log("button mouse over");
        //해당 아이템의 세부 내용 UI 뜨게
    }

    //마우스를 버튼에서 밖으로 뺐을 때 UI 작업
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!m_isItemExit)
            return;
        
        Debug.Log("button mouse exit");
        //아이템 세부 UI 없애기
    }
}
