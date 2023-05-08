using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDialogueController : MonoBehaviour
{
    public DialogueScripts scripts;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
