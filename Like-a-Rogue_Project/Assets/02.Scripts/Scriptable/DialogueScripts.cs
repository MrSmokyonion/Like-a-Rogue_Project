using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Script_000_temp", menuName = "Scriptable Object Asset/Dialogue Script")]

public class DialogueScripts : ScriptableObject
{
    public List<ScriptData> Scripts;
}

[System.Serializable]
public class ScriptData
{
    public string name;
    [TextArea(3, 10)] public string str;
}