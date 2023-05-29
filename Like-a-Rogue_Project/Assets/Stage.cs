using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    //포탈 예시 - 만들어서 적을 모두 죽였을때 포탈 생성할 위치에 미리 배치하고 기본 비활성화
    public GameObject portalObject;
    public bool isSceneLoadPortal = false;
    public GameObject m_fade;
    [SerializeField] public GameObject[] m_Enemys;
    public int enemyCount;
    public Transform warpPosition;
    public CameraController m_camController;

    public string m_StageSceneStr;
    private bool portalActivated = false;

    private void OnValidate()
    {
        enemyCount = m_Enemys.Length;
    }

    private void Update()
    {
        

        // Enemy 오브젝트가 없고 Portal이 아직 활성화되지 않았을 때 Portal을 활성화
        if (enemyCount == 0 && !portalActivated)
        {
            portalObject.SetActive(true);
            portalActivated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player가 Portal에 충돌했을 때 이동
        if (other.CompareTag("Player"))
        {
            if (isSceneLoadPortal)
            {
                if(m_fade != null)
                    m_fade.SetActive(true);
                StartCoroutine(LoadSceneAsync());
                return;
            }
            
            //vertor3의 수치(x, y, z)값을 변경하여 순간이동 - 맵을 한 Scene에 여러개 만들어서 좌표값 따서 넣기 밑의 좌표는 예시 좌표
            other.transform.position = warpPosition.position;
            m_camController.center.x = 51.8f;
        }
    }
    
    IEnumerator LoadSceneAsync()
    {
        PlayerPrefs.SetString("LoadScene", m_StageSceneStr);
        yield return new WaitForSeconds(2.0f);
        
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
