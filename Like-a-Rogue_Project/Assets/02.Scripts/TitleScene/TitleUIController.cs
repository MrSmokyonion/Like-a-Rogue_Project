using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    [Header("Controller")]
    public OptionUIController m_optionUIController;
    //페이드인,아웃 애니메이션

    public void Btn_Start()
    {
        TitleSceneManager._instance.StartGame();
    }
    
    public void Btn_Option()
    {
        m_optionUIController.OpenOption();
    }
    
    public void Btn_Exit()
    {
        TitleSceneManager._instance.ExitGame();
    }
}
