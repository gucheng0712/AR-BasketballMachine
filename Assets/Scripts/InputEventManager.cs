using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputEventManager : MonoBehaviour
{

    [SerializeField] Slider yPowerSlider;
    [SerializeField] Slider zPowerSlider;
    [SerializeField] AudioClip swapShootSound;


    float touchTimeStart;
    float touchtimeFinish;
    float timeInterval;
    public bool enableMicrophone;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 dir;

    bool m_canShoot;


    void Start()
    {
        GameManager.Instance.m_yPowerMultiplier = yPowerSlider.value;
        GameManager.Instance.m_zPowerMultiplier = zPowerSlider.value;

        GameManager.Instance.SwapShootEvent += Shoot;
        GameManager.Instance.SwapShootEvent += PlaySwapShootSound;
    }

    void OnDisable()
    {
        GameManager.Instance.SwapShootEvent -= Shoot;
        GameManager.Instance.SwapShootEvent -= PlaySwapShootSound;
    }

    private void Update()
    {
        if (GameManager.Instance.IsVoiceShoot == false)
            SwipInput();
    }




    void SwipInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (Input.mousePosition.x > Screen.width * 0.25f && Input.mousePosition.x < Screen.width * 0.75f)
            {
                m_canShoot = true;
            }

            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && m_canShoot)
        {
            m_canShoot = false;
            touchtimeFinish = Time.time;
            timeInterval = touchtimeFinish - touchTimeStart;
            endPos = Input.mousePosition;
            dir = endPos - startPos;
            GameManager.Instance.SwapShootEvent();
        }

    }

    void Shoot()
    {
        timeInterval = Mathf.Clamp(timeInterval, 0.5f, 1);

        float dirForce = dir.y / timeInterval;

        dirForce = Mathf.Clamp(dirForce, 0, 1000f);
        float mapedForce = MathFunc.Remap(dirForce, 0f, 1000f, 1.5f, 8 * GameManager.Instance.powerMultiplier);

        //float clampedForce = Mathf.Clamp(dirForce, 3.0f, 8.0f);

        Vector3 forceDir = (Camera.main.transform.forward * 2f * GameManager.Instance.m_zPowerMultiplier + Vector3.up * 4f * GameManager.Instance.m_yPowerMultiplier) * mapedForce;
        GameManager.Instance.AddForceToBall(forceDir);
    }

    void PlaySwapShootSound()
    {
        GameManager.Instance.AudioSource.PlayOneShot(swapShootSound);
    }


    public void OnYPowerSliderValueChange()
    {
        GameManager.Instance.m_yPowerMultiplier = yPowerSlider.value;
    }

    public void OnZPowerSliderValueChange()
    {
        GameManager.Instance.m_zPowerMultiplier = zPowerSlider.value;
    }

    public void OnMicrophoneTogglePressed()
    {
        enableMicrophone = !enableMicrophone;
        if (enableMicrophone)
        {
            GameManager.Instance.m_steaming.MicrophoneStartListen();
        }
        else
        {
            GameManager.Instance.m_steaming.MicrophoneStopListen();
        }
    }

}
