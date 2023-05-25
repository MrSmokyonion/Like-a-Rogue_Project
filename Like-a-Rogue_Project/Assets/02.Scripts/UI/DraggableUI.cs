using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform m_canvas;
    
    private Transform m_previousParent;
    private RectTransform m_rectTr;
    private CanvasGroup m_canvasGroup;
    [HideInInspector] public bool m_isEquit = false;
    [HideInInspector] public Image m_ImageRenderer;

    void Start()
    {
        //m_canvas = FindObjectOfType<Canvas>().transform;
        m_rectTr = GetComponent<RectTransform>();
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_ImageRenderer = GetComponent<Image>();
    }

    private void Update()
    {
        if (m_isEquit == false)
        {
            m_canvasGroup.alpha = 1f;
        }
        else if (m_isEquit == true)
        {
            m_canvasGroup.alpha = 0.6f;
        }
    }

    //현재 오브젝트 드래그 시작시 1번 호출됨.
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.GetComponent<InventoryItemButton>().SetActiveItemDetailUI(false);
        
        m_previousParent = transform.parent;

        transform.SetParent(m_canvas);
        transform.SetAsLastSibling();

        m_canvasGroup.alpha = 0.6f;
        m_canvasGroup.blocksRaycasts = false;
        GetComponent<Image>().raycastTarget = false;
    }

    //현재 오브젝트 드래그 하는 내내 프레임마다 호출됨.
    public void OnDrag(PointerEventData eventData)
    {
        m_rectTr.anchoredPosition = new Vector2(eventData.position.x - 1920/2, eventData.position.y - 1080/2);
    }

    //현재 오브젝트 드래그 종료할때 1번 호출됨.
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == m_canvas)
        {
            transform.SetParent(m_previousParent);
            m_rectTr.position = m_previousParent.GetComponent<RectTransform>().position;
        }
        
        m_canvasGroup.alpha = 1f;
        m_canvasGroup.blocksRaycasts = true;
        GetComponent<Image>().raycastTarget = true;
    }
}
