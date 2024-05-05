using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviourPun
{
    [SerializeField] GameObject m_Visuals;
    [SerializeField] Camera m_Camera;
    [SerializeField] TMP_Text m_Player_Name;

    private void Start()
    {
        if (photonView.IsMine)
        {
            m_Visuals.SetActive(false);
            NetworkCallbacks.DebugLog(string.Concat(PhotonNetwork.NickName, " : ", "Player Script"), "green", NetworkCallbacks.DebugFont(FontStyle.bold));
            photonView.RPC("SetNickName", RpcTarget.AllBuffered, PhotonNetwork.NickName);
            FindObjectOfType<Manager>().m_Cam_Ref.position = transform.position;
            m_Camera.transform.SetParent(FindObjectOfType<Manager>().m_Cam_Ref);
        }
    }

    private void Update()
    {

        m_Visuals.transform.parent.rotation = Quaternion.Euler(m_Camera.transform.rotation.eulerAngles.x, 
                                                                m_Camera.transform.rotation.eulerAngles.y, 
                                                                0f);
    }

    [PunRPC]
    private void SetNickName(string nickName)
    {
        m_Player_Name.text = nickName;
    }
}