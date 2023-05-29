using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public string m_StageSceneStr;
    public Slider m_sliderUI;

    private void Start()
    {
        m_StageSceneStr = PlayerPrefs.GetString("LoadScene");
        
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(2.0f);
        
        Scene targetScene = SceneManager.GetSceneByName(m_StageSceneStr);

        if (!(targetScene.isLoaded))
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(m_StageSceneStr, LoadSceneMode.Single);
            
            while (!(ao.isDone))
            {
                float progress = Mathf.Clamp01(ao.progress / .9f);
                m_sliderUI.value = progress;
                
                yield return null;
            }
        }
    }
}