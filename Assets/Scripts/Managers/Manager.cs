// Manager.cs
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviourPunCallbacks
{
    [Header("Multiplayer Manager References")]
    [SerializeField] NetworkManager m_NetworkManager;

    [SerializeField] GameObject m_Player_Prefab;
    [SerializeField] GameObject m_DefaultCamera;
    [SerializeField] GameObject m_NameObject;
    [SerializeField] GameObject m_KeyboardObject;
    [SerializeField] GameObject m_JoinCreateRoomObject;
    [SerializeField] Transform m_HostSpawnPosition;
    [SerializeField] Transform[] m_Player_Instantiate_LOC;
    [SerializeField] VR_Button m_VR_Button;

    [SerializeField] internal Transform m_Cam_Ref;

    private List<GameObject> m_PlayerButtonList = new List<GameObject>();
    private bool m_IsRoomHost = false;

    private string m_Player_Name;

    private void Awake()
    {
        m_Player_Name = PlayerPrefs.GetString("Name", "User");
        Time.timeScale = 1;
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        NetworkCallbacks.ConnectServer();
        m_NetworkManager.ConnectServer += JoinLobby;
        m_NetworkManager.CreateRoom += SetRoomHost;
        m_NetworkManager.JoinRoom += JoinActivity;
        m_NetworkManager.PlayerEntered += DisableCameras;
        m_NetworkManager.PlayerEntered += OnPlayerEnter;

        Disable();
    }

    private void Update()
    {

        Google.XR.Cardboard.Api.MinTriggerHeldPressedTime = 2f;
        if (Google.XR.Cardboard.Api.IsTriggerHeldPressed)
        {
            Google.XR.Cardboard.Api.Recenter();
        }

        if(Google.XR.Cardboard.Api.IsCloseButtonPressed)
        {
            Application.Quit();
        }
    }

    private void JoinLobby()
    {
        NetworkCallbacks.JoinLobby();
    }

    private void JoinActivity()
    {
        m_Player_Name = PlayerPrefs.GetString("Name");
        InstantiatePlayer();
    }

    private void SetRoomHost()
    {
        m_IsRoomHost = true;
    }

    private void OnPlayerEnter(Photon.Realtime.Player _Player)
    {
        NetworkCallbacks.DebugLog(string.Concat(_Player.NickName,
            " Has Joined The Room..."), "green",
            NetworkCallbacks.DebugFont(FontStyle.bold));

        Disable();
    }

    private void InstantiatePlayer()
    {
        NetworkCallbacks.DebugLogRich("INSTANTIATED PLAYER",
            "blue",
            NetworkCallbacks.DebugFont(FontStyle.bold),
            NetworkCallbacks.DebugFont(FontStyle.italic));
        m_DefaultCamera.SetActive(false);
        m_NameObject.SetActive(false);
        //m_KeyboardObject.SetActive(false);
        m_JoinCreateRoomObject.SetActive(false);

        Disable();

        if (m_IsRoomHost)
        {
            var host_player = PhotonNetwork.Instantiate(m_Player_Prefab.name, m_HostSpawnPosition.position, Quaternion.identity);
            host_player.transform.GetChild(0).gameObject.SetActive(true);
            //host_player.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.NickName;
        }
        else
        {
            var network_player = PhotonNetwork.Instantiate(m_Player_Prefab.name, m_Player_Instantiate_LOC[PhotonNetwork.LocalPlayer.ActorNumber - 1].position, Quaternion.identity);
            network_player.transform.GetChild(0).gameObject.SetActive(true);
            //network_player.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.NickName;
        }
    }

    private void DisableCameras(Photon.Realtime.Player _Player)
    {
        Disable();
    }

    private void Disable()
    {
        foreach (GameObject _pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            NetworkCallbacks.DebugLog(_pl.GetComponent<PhotonView>().Owner.NickName, "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
            NetworkCallbacks.DebugLog(_pl.transform.GetChild(0).gameObject.name, "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
            if (!_pl.GetComponent<PhotonView>().IsMine)
            {
                _pl.transform.GetChild(0).gameObject.SetActive(false);
                NetworkCallbacks.DebugLog(_pl.GetComponent<PhotonView>().Owner.NickName, "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
                NetworkCallbacks.DebugLog(_pl.transform.GetChild(0).gameObject.name, "cyan", NetworkCallbacks.DebugFont(FontStyle.bold));
            }
        }
    }

    [PunRPC]
    private void UpdateNicknameText(string newNickname)
    {
        PhotonNetwork.NickName = newNickname;
        NetworkCallbacks.DebugLog(string.Concat("VR_Button", newNickname), "green", NetworkCallbacks.DebugFont(FontStyle.bold));
    }
}