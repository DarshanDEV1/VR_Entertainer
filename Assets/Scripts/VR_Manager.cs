using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.Cardboard;
using JetBrains.Annotations;

public class VR_Manager : MonoBehaviour
{
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Update()
    {
        Api.MinTriggerHeldPressedTime = 1f;
        if (Api.IsTriggerHeldPressed)
        {
            Api.Recenter();
        }

        if(Api.IsCloseButtonPressed)
        {
            Application.Quit();
        }
    }
}
