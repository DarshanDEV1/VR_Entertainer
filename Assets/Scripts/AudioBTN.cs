// VR_Button.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class AudioBTN : MonoBehaviour
{
    [SerializeField] Audio_Enum m_Audio;

    public void OnPointerEnter()
    {
        NetworkCallbacks.DebugLog("Pointer Enter...", "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
    }

    public void OnPointerExit()
    {
        NetworkCallbacks.DebugLog("Pointer Exit...", "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
    }

    public void OnPointerClick()
    {
        NetworkCallbacks.DebugLog("Pointer Clicked...", "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));

        if (m_Audio == Audio_Enum.Next)
        {
            FindObjectOfType<AudioManager>().Next();
        }
        else
        {
            FindObjectOfType<AudioManager>().Previous();
        }
    }

    public enum Audio_Enum
    {
        Prev,
        Next
    }
}