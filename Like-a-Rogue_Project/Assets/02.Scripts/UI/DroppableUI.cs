using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IDropHandler
{
    private DraggableUI m_currentDroppedObject;
    [HideInInspector] public Image m_imageRenderer;
    public Sprite m_normalSword;

    private void Start()
    {
        m_imageRenderer = GetComponent<Image>();
        
        m_imageRenderer.sprite = m_normalSword;
        m_currentDroppedObject = InventoryManager._instance.GetInvSlotWeaponImageObj(0).GetComponent<DraggableUI>();
        m_currentDroppedObject.m_isEquit = true;
        GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Drop!");
            GetComponent<CanvasGroup>().alpha = 1f;
            GameObject dropped = eventData.pointerDrag;

            if (m_currentDroppedObject != null)
            {
                m_currentDroppedObject.m_isEquit = false;
            }
            
            DraggableUI temp = dropped.GetComponent<DraggableUI>();
            temp.m_isEquit = true;
            m_imageRenderer.sprite = temp.m_ImageRenderer.sprite;
            m_currentDroppedObject = temp;
        }
    }
}
