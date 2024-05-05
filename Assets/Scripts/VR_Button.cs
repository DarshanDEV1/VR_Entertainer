// VR_Button.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class VR_Button : MonoBehaviourPunCallbacks
{
    [SerializeField] Manager manager;
    [SerializeField] TMP_Text nicknameText;

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

        NetworkCallbacks.JoinOrCreateRoom("Room", 12);

        // Update the player's nickname
        string newNickname = FindObjectOfType<VR_Keyboard>().outputText.text;
        UpdateNicknameText(newNickname);
        // Hide the VR_Keyboard
        FindObjectOfType<VR_Keyboard>().gameObject.SetActive(false);
    }

    private void UpdateNicknameText(string newNickname)
    {
        PhotonNetwork.NickName = newNickname;
        NetworkCallbacks.DebugLog(string.Concat("VR_Button", " :  ", newNickname), "green", NetworkCallbacks.DebugFont(FontStyle.bold));
    }
}