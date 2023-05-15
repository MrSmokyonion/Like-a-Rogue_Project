using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCarrier : MonoBehaviour
{
    [Header("Scripts")] public DialogueScripts scripts;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            MiniDialogueController._instance.Do_ShowText(scripts);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            MiniDialogueController._instance.Do_HideText();
        }
    }
}
