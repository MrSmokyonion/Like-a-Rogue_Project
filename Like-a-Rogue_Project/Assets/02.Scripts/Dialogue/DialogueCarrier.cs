using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueCarrier : MonoBehaviour
{
    [Header("Scripts")] public DialogueScripts scripts;
    private MiniDialogueController m_miniDialogueController;

    private void Start()
    {
        m_miniDialogueController = GetComponent<MiniDialogueController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("dialogue");
        if (col.tag == "Player")
        {
            m_miniDialogueController.Do_ShowText(scripts);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            m_miniDialogueController.Do_HideText();
        }
    }
}
