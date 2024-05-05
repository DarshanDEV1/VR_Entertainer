using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Keyboard_Key : MonoBehaviourPunCallbacks
{
    private Vector3 m_Position;
    private Vector3 m_Scale;

    private void Start()
    {
        m_Position = transform.position;
        m_Scale = transform.localScale;
    }

    public void OnPointerEnter()
    {
        NetworkCallbacks.DebugLog("Key Pointer Enter...", "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
        //Vector3 _pos = m_Position + new Vector3(0f, 0.1f, 0f);
        Vector3 _scl = m_Scale + new Vector3(0.2f, 0.2f, 0.2f);
        //transform.position = _pos;
        transform.localScale = _scl;
    }

    public void OnPointerExit()
    {
        NetworkCallbacks.DebugLog("Key Pointer Exit...", "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
        //transform.position = m_Position;
        transform.localScale = m_Scale;
    }
}