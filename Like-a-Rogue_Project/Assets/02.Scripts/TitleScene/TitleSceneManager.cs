using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [Header("Variable")]
    public string m_StageSceneName;

    [Header("Start Animation Ref")] 
    public DOTweenAnimation m_TitleDOTween;
    public DOTweenAnimation m_ButtonDOTween;
    public DOTweenAnimation m_FadeDOTween;
    public DOTweenAnimation m_cameraDOTween;
    
    //Singleton
    public static TitleSceneManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    public void StartGame()
    {
        //m_fadeInOut.Do_FadeIn();
        m_TitleDOTween.DORestartById("Out");
        m_ButtonDOTween.DORestartById("Out");
        m_FadeDOTween.gameObject.SetActive(true);
        m_cameraDOTween.DORestartById("Out");
        StartCoroutine(LoadSceneAsync());
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(2.0f);
        
        Scene targetScene = SceneManager.GetSceneByName(m_StageSceneName);

        if (!(targetScene.isLoaded))
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(m_StageSceneName, LoadSceneMode.Single);
            
            while (!(ao.isDone))
            {
                yield return null;
            }
        }
    }
}
