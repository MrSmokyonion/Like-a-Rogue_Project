using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HitUIContainer : MonoBehaviour
{
    private DOTweenAnimation m_dotAnim_this;
    private DOTweenAnimation m_dotAnim_mainCam;
    private Image m_image;
    
    // Start is called before the first frame update
    void Start()
    {
        m_dotAnim_this = GetComponent<DOTweenAnimation>();
        m_dotAnim_mainCam = Camera.main.GetComponent<DOTweenAnimation>();
        m_image = GetComponent<Image>();
    }

    public void Do_HitAnim()
    {
        m_image.color = new Color(1f, 1f, 1f, 120f / 255f);
        m_dotAnim_this.DORestartById("Hit");
        m_dotAnim_mainCam.DORestartById("Shake");
    }
}
