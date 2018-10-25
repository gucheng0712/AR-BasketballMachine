using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MyTrackableEventHandler : DefaultTrackableEventHandler
{
    TrackableBehaviour m_trackableBehaviour;


    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTrackingFound()
    {
        GameManager.Instance.CallFoundTargetEvent();
        base.OnTrackingFound();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
    }

}
