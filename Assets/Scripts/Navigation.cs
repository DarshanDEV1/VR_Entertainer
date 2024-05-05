using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    [SerializeField] Nav_Enum m_Nav_Enum;

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

        Navigation_Method();
    }

    private void Navigation_Method()
    {
        switch(m_Nav_Enum)
        {
            case Nav_Enum.Audio_Player:
                SceneChange(Scene_Enum.VR_Audio_Visualizer);
                break;
            case Nav_Enum.Cinema_Hall:
                SceneChange(Scene_Enum.VR_Cinema_Hall);
                break;
            case Nav_Enum.Meeting_Room:
                SceneChange(Scene_Enum.VR_Room_Meeting);
                break;
            case Nav_Enum.Back:
                SceneChange(Scene_Enum.StartScene);
                break;
            case Nav_Enum.Exit:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    private void SceneChange(Scene_Enum _scene)
    {
        SceneManager.LoadSceneAsync(_scene.ToString());
    }

    public enum Scene_Enum
    {
        StartScene,
        VR_Room_Meeting,
        VR_Cinema_Hall,
        VR_Audio_Visualizer
    }

    public enum Nav_Enum
    {
        Audio_Player,
        Cinema_Hall,
        Meeting_Room,
        Back,
        Exit
    }
}
