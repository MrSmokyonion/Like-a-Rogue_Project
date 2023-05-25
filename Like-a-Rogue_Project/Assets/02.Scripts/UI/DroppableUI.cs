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

    private void Start()
    {
        m_imageRenderer = GetComponent<Image>();
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
