using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private DraggableUI m_currentDroppedObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop!");

        if (eventData.pointerDrag != null)
        {
            Debug.Log("DROP!!!!");
            GetComponent<CanvasGroup>().alpha = 1f;
            GameObject dropped = eventData.pointerDrag;

            if (m_currentDroppedObject != null)
            {
                m_currentDroppedObject.m_isEquit = false;
            }
            
            DraggableUI temp = dropped.GetComponent<DraggableUI>();
            temp.m_isEquit = true;
            m_currentDroppedObject = temp;
        }
    }
}
