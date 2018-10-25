using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool m_isPassHoop;

    void Start()
    {
        Destroy(gameObject, 10f);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "HoopTop")
        {
            m_isPassHoop = true;
        }

        if (m_isPassHoop && col.gameObject.tag == "HoopBottom")
        {
            m_isPassHoop = false;
            GameManager.Instance.CallScoredEvent();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Board")
        {

        }
    }
}
