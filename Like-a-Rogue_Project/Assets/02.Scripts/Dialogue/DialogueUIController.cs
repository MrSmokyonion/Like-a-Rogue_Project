using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueUIController : MonoBehaviour, IPointerClickHandler
{
    public GameObject m_UI_dialogueContainer;
    public Text m_UI_text_name;
    public Text m_UI_text_str;


    public void Do_TurnOnDialogue()
    {
        m_UI_dialogueContainer.SetActive(true);
    }
    public void Do_TurnOffDialogue()
    {
        m_UI_dialogueContainer.SetActive(false);
    }
    
    public void Do_PrintScript(ScriptData _script)
    {
        m_UI_text_name.text = _script.name;
        
        //m_UI_text_str.text = _script.str;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_script.str));
    }

    IEnumerator TypeSentence(string _sentence)
    {
        m_UI_text_str.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            m_UI_text_str.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DialogueManager._instance.Do_NextDialogue();
    }
}