using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController _instance;
    public GameObject m_gameOverFade;
    public GameObject m_container;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        
        _instance = this;
    }

    public void GameOver()
    {
        m_gameOverFade.SetActive(true);
        Invoke("gameOverUI", 2.5f);
    }

    private void gameOverUI()
    {
        m_container.SetActive(true);
    }

    public void GameOver_Yes()
    {
        PlayerPrefs.SetString("LoadScene", "Tutorial Scene");
        StartCoroutine(LoadSceneAsync());
    }
    
    public void GameOver_No()
    {
        PlayerPrefs.SetString("LoadScene", "Title Scene");
        StartCoroutine(LoadSceneAsync());
    }
    
    IEnumerator LoadSceneAsync()
    {
        Scene targetScene = SceneManager.GetSceneByName("Loading Scene");

        if (!(targetScene.isLoaded))
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync("Loading Scene", LoadSceneMode.Single);
            
            while (!(ao.isDone))
            {   
                yield return null;
            }
        }
    }
}
