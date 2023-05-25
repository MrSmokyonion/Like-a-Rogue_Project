using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiniDialogueController : MonoBehaviour
{
    [Header("UI")] 
    public Text m_UI_text_size;
    public Text m_UI_text_visible;

    private GameObject m_childCanvas;

    private void Start()
    {
        m_childCanvas = transform.GetChild(0).gameObject;
    }

    public void Do_ShowText(DialogueScripts _scripts)
    {
        m_childCanvas.SetActive(true);
        int index = Random.Range(0, _scripts.Scripts.Count);
        m_UI_text_size.text = _scripts.Scripts[index].str;
        StartCoroutine(TypeSentence(_scripts.Scripts[index].str));
    }

    public void Do_HideText()
    {
        m_childCanvas.SetActive(false);
        StopAllCoroutines();
    }
    
    IEnumerator TypeSentence(string _sentence)
    {
        m_UI_text_visible.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            m_UI_text_visible.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
