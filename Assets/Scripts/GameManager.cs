using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public ExampleStreaming m_steaming;
    public Transform m_firePoint;
    public Transform m_goalPoint;

    public float powerMultiplier = 1;
    public float m_yPowerMultiplier;
    public float m_zPowerMultiplier;

    [SerializeField]
    bool m_isVoiceShoot;
    public bool IsVoiceShoot { get { return m_isVoiceShoot; } set { m_isVoiceShoot = value; } }


    [SerializeField] GameObject m_basketballPrefab;
    [SerializeField] AudioSource m_audioSource;
    public AudioSource AudioSource { get { return m_audioSource; } set { m_audioSource = value; } }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public delegate void SpeechCommandHandler();
    public SpeechCommandHandler SwitchShootModeEvent;
    public SpeechCommandHandler VoiceShootEvent;

    public delegate void TouchInputHandler();
    public TouchInputHandler SwapShootEvent;

    public delegate void GameManagerEventHandler();
    public GameManagerEventHandler ScoredEvent;
    public GameManagerEventHandler MissedEvent;
    public GameManagerEventHandler FoundTargetEvent;
    public GameManagerEventHandler LostTargetEvent;


    public void SpeechCommandAction(string Commands)
    {
        Commands = Commands.Trim();
        switch (Commands)
        {
            case "switch mode":
                CallSwitchShootModeEvent();
                print("switch mode");
                break;
            case "shoot":
                if (IsVoiceShoot == true)
                    CallVoiceShootEvent();
                print("voiceShoot");
                break;

        }
    }

    public void AddForceToBall(Vector3 _forceDir)
    {
        GameObject newBasketball = Instantiate(m_basketballPrefab, m_firePoint.position, Quaternion.identity);

        Rigidbody ballRb = newBasketball.GetComponent<Rigidbody>();
        ballRb.AddForce(_forceDir);
        ballRb.AddTorque(transform.right * 10f);
    }


    #region GameManagerEvents
    public void CallScoredEvent()
    {
        if (ScoredEvent != null)
        {
            ScoredEvent();
        }
    }

    public void CallMissedEvent()
    {
        if (MissedEvent != null)
        {
            MissedEvent();
        }
    }

    public void CallFoundTargetEvent()
    {
        if (FoundTargetEvent != null)
        {
            FoundTargetEvent();
        }
    }

    public void CallLostTargetEvent()
    {
        if (LostTargetEvent != null)
        {
            LostTargetEvent();
        }
    }
    #endregion

    #region VoiceCommandEvent
    public void CallSwitchShootModeEvent()
    {
        if (SwitchShootModeEvent != null)
        {
            SwitchShootModeEvent();
        }
    }

    public void CallVoiceShootEvent()
    {
        if (VoiceShootEvent != null)
        {
            VoiceShootEvent();
        }
    }
    #endregion

}