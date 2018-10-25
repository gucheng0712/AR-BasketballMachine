using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoredEventManager : MonoBehaviour
{
    [SerializeField] Text m_scoredCountText;
    [SerializeField] ParticleSystem m_scoredParticle;

    [SerializeField] AudioClip m_scoredAudio;
    public int scoredCount;


    private void Start()
    {
        GameManager.Instance.ScoredEvent += IncreaseScoredCount;
        GameManager.Instance.ScoredEvent += PlayScoredEffect;
        GameManager.Instance.ScoredEvent += PlayScoredSound;
    }

    private void OnDisable()
    {
        GameManager.Instance.ScoredEvent -= IncreaseScoredCount;
        GameManager.Instance.ScoredEvent -= PlayScoredEffect;
        GameManager.Instance.ScoredEvent -= PlayScoredSound;
    }

    void IncreaseScoredCount()
    {
        scoredCount++;
        m_scoredCountText.text = scoredCount.ToString();
    }

    void PlayScoredEffect()
    {
        m_scoredParticle.Play();
    }

    void PlayScoredSound()
    {
        GameManager.Instance.AudioSource.PlayOneShot(m_scoredAudio);
    }




}
