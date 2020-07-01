using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// MARKED FOR DELET
/// </summary>
public class LobbyUI : MonoBehaviour
{
    [Scene]
    public string MainMenu;

    public void Back()
    {
        ////IF PLAYER IS HOST
        //if (this.hasAuthority && this.isClient)
        //{
        //    Debug.Log("---------------LEADER LEAVE");
        //    Mirror.NetworkRoomManager.singleton.StopHost();
        //}
        ////IF CLIENT ONLY
        //else if (this.hasAuthority && this.isClientOnly)
        //{
        //    Debug.Log("---------------CLIENT LEAVE");
        //    Mirror.NetworkRoomManager.singleton.StopClient();

        //}   
        

        //SceneManager.LoadScene(MainMenu);
    }
}
