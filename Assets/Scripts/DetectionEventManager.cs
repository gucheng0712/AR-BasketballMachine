using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEventManager : MonoBehaviour
{
    [SerializeField] ParticleSystem m_appearParticle;
    [SerializeField] ParticleSystem m_disappearParticle;
    private void Start()
    {
        GameManager.Instance.FoundTargetEvent += PlayAppearanceEffect;
    }

    private void OnDisable()
    {
        GameManager.Instance.FoundTargetEvent -= PlayAppearanceEffect;
    }


    void PlayAppearanceEffect()
    {
        m_appearParticle.Play();
    }

    //void PlayDisappearanceEffect()
    //{
    //    m_disappearParticle.Play();
    //}
}
