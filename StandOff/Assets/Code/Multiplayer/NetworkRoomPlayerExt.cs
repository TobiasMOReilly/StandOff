using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
/// <summary>
/// Extention of NetworkRoomPlayer.cs
/// Allows for additional custom functionality.
/// </summary>

public class NetworkRoomPlayerExt : NetworkRoomPlayer
{

    [Header("UI Elements")]
    public GameObject UIObject;
    public GameObject BeginButton;
    public GameObject ReadyButton;
    public GameObject BackButton;
    public GameObject LobbyUITarget;

    [Header("Player Details")]
    public string PlayerName; //MARKED FOR DELETE - REPLACED BY DISPLAY NAME
    public GameObject PlayerListItemPrefab;
    public bool IsLeader;
    public Guid id;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private NetworkRoomManagerExt roomManager;
    private NetworkRoomManagerExt RoomManager
    {
        get
        {
            if (roomManager != null)
            {
                return roomManager;
            }
            else
            {
                return roomManager = NetworkManager.singleton as NetworkRoomManagerExt;
            }
        }
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplayReady();
    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();


    #region Getters /Setters
    /// <summary>
    /// Allows the player to be set as leader. 
    /// Leader has more privilages
    /// </summary>
    /// <param name="state"></param>
    public void SetIsLeader(bool state)
    {
        IsLeader = state;
    }

    /// <summary>
    /// Used to return the players name to external scripts
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return DisplayName;
    }

    #endregion

    #region Name / Ready Methods
    /// <summary>
    /// Used to update the players listing in lobby
    /// </summary>
    private void UpdateDisplay()
    {
        //RoomManager.UpdateListItem(this);
        RoomManager.RebuildPlayerListName();
    }

    private void UpdateDisplayReady()
    {
        RoomManager.RebuildPlayerListReady();
    }

    /// <summary>
    /// Used to set the begin button visible for the lead player
    /// </summary>
    /// <param name="state"></param>
    [Client]
    private void SetBeginButtonVisable(bool state)
    {
        if (this.isLocalPlayer)
        {
            BeginButton.SetActive(state);
        }
    }
    #endregion

    #region Base OverRides
    public override void OnStartAuthority()
    {
        //Activate UI
        ActivateUI(true);
        Debug.Log("---------------------------------------------------------" + IsReady);
    }

    public override void OnStartClient()
    {
        InitLobbyPlayer();
        base.OnStartClient();
    }
    /// <summary>
    /// Runs logic for when a player leaves the room
    /// </summary>
    public override void OnClientExitRoom()
    {
        EndLobbyPlayer();

        base.OnClientExitRoom();
    }

    public override void OnNetworkDestroy()
    {
        EndLobbyPlayer();

        base.OnNetworkDestroy();
    }
    #endregion

    #region ServerCommands
    /// <summary>
    /// OLD - MARKED FOR DELETE
    /// Tells the server to assign the players display name.
    /// </summary>
    [Command]
    private void CmdSetPlayerName()
    {
        this.PlayerName = PlayerPrefsManager.GetName();
    }
    /// <summary>
    /// Tells the server to assign the players display name.
    /// </summary>
    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        this.DisplayName = displayName;
    }
    [Command]
    private void CmdSetPlayerId()
    {
        this.id = IdGenerator.Generate();
    }
    [Command]
    private void CmdReadyUp()
    {
        this.IsReady = !this.IsReady;
        base.CmdChangeReadyState(IsReady);
    }
    #endregion

    #region UI

    private void ActivateUI(bool state)
    {
        if (state)
        {
            //Get the target perant panal
            GameObject target = GameObject.FindGameObjectWithTag("LobbyUITarget");
            //Set UIObject to be child of target
            UIObject.transform.SetParent(target.transform, false);
        }

        //Set UIObject active
        UIObject.SetActive(state);
    }

    public void UiReadyButton()
    {
        if (this.isLocalPlayer)
        {          
            CmdReadyUp();
        }
    }


    public void UiBackButton()
    {
        if (this.isLocalPlayer && IsLeader)
        {
            Debug.Log("---------------LEADER LEAVE");
            roomManager.StopHost();
        }
        else if (this.isLocalPlayer && !IsLeader)
        {
            Debug.Log("---------------CLIENT LEAVE");
            roomManager.StopClient();
        }
    }
    #endregion

    #region Enter / Exit Logic

    private void InitLobbyPlayer()
    {
        //Set id
        this.id = IdGenerator.Generate();

        //Add player to roommanager list
        RoomManager.AddPlayer(this);

        //Set name
        CmdSetDisplayName(PlayerPrefsManager.GetName());


        SetBeginButtonVisable(IsLeader);   

    }

    private void EndLobbyPlayer()
    {
        RoomManager.RemovePLayer(this);
        ActivateUI(false);
    }

    #endregion
}

