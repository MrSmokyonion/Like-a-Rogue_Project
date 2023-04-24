using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool input_Inventory { get; private set; }
    
    public void OnInventory(InputAction.CallbackContext _value)
    {
        switch (_value.phase)
        {
            case InputActionPhase.Performed:
                input_Inventory = true;
                break;
            default:
                input_Inventory = false;
                break;
        }
    }
}
