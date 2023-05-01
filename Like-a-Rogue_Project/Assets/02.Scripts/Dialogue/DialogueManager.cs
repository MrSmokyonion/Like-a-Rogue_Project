using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance;
    
    public DialogueScripts m_DialogueScripts;
    public DialogueUIController m_DialogueUI;
    public BlackBarUI m_BlackUI;

    private int m_index = 0;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        
        _instance = this;
    }

    public void Do_StartDialogue()
    {
        m_index = 0;
        
        m_BlackUI.Do_BlackBar_On();
        m_DialogueUI.Do_TurnOnDialogue();
        Do_NextDialogue();
    }

    public void Do_NextDialogue()
    {
        if (m_index >= m_DialogueScripts.Scripts.Count)
        {
            Do_EndDialogue();
            return;
        }
        
        m_DialogueUI.Do_PrintScript(m_DialogueScripts.Scripts[m_index]);
        m_index++;
    }

    public void Do_EndDialogue()
    {
        m_DialogueUI.Do_TurnOffDialogue();
        m_BlackUI.Do_BlackBar_Off();
    }
}
