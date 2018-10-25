using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechCmdEventManager : MonoBehaviour
{

    [SerializeField] AudioClip m_switchModeAudio;
    [SerializeField] AudioClip m_voiceShootAudio;

    void Start()
    {
        Debug.Log("RegisterSpeechCmdEvent");
        GameManager.Instance.SwitchShootModeEvent += SwitchShootMode;
        GameManager.Instance.SwitchShootModeEvent += PlaySwitchModeSound;
        GameManager.Instance.VoiceShootEvent += ShootByVoice;
        GameManager.Instance.VoiceShootEvent += ShootSoundEffect;
    }

    void OnDisable()
    {
        GameManager.Instance.SwitchShootModeEvent -= SwitchShootMode;
        GameManager.Instance.SwitchShootModeEvent -= PlaySwitchModeSound;
        GameManager.Instance.VoiceShootEvent -= ShootByVoice;
        GameManager.Instance.VoiceShootEvent -= ShootSoundEffect;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.IsVoiceShoot == true)
        {
            GameManager.Instance.CallVoiceShootEvent();
        }
    }


    void SwitchShootMode()
    {
        GameManager.Instance.IsVoiceShoot = !GameManager.Instance.IsVoiceShoot;
        if (GameManager.Instance.IsVoiceShoot)
        {
            GameManager.Instance.m_steaming.ResultsField.text = "Say \"Shoot\" to shoot a ball.\n Say \"Switch mode\" to switch to swap shoot mode";
        }
        else
        {
            GameManager.Instance.m_steaming.ResultsField.text = "Swap Screen to \"Shoot\" a ball.\n Say \"Switch mode\" to switch to swap shoot mode";
        }
    }

    void PlaySwitchModeSound()
    {
        GameManager.Instance.AudioSource.PlayOneShot(m_switchModeAudio);
    }


    void ShootByVoice()
    {
        float dist = (GameManager.Instance.m_goalPoint.position - GameManager.Instance.m_firePoint.position).magnitude;
        Vector3 forceDir = (Camera.main.transform.forward * GameManager.Instance.m_zPowerMultiplier * 10f + Vector3.up * 20f * GameManager.Instance.m_yPowerMultiplier) * dist;
        GameManager.Instance.AddForceToBall(forceDir);
    }

    public void ShootSoundEffect()
    {
        GameManager.Instance.AudioSource.PlayOneShot(m_voiceShootAudio);
    }




}
