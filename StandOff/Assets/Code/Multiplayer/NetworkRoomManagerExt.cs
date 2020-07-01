using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class NetworkRoomManagerExt : NetworkRoomManager
{
    #region Server Detail Variables

    private string ServerName;
    private int MaxPlayers;
    private int CurrentPlayers = 0;
    #endregion

    #region Room Variables
    [Header("Room List Variables")]
    [Tooltip("The game object for the list of players in room")]
    [SerializeField]
    private GameObject PlayerList;

    [Tooltip("The list item prefab")]
    [SerializeField]
    private GameObject ListItemPrefab;

    //List of player details objects
    //private List<NetworkRoomPlayerExt> roomPlayers = new List<NetworkRoomPlayerExt>();
    private Dictionary<Guid, NetworkRoomPlayerExt> roomPlayers1 = new Dictionary<Guid, NetworkRoomPlayerExt>();

    //List of all current player list items
    //private List<GameObject> listItems = new List<GameObject>();
    private Dictionary<Guid, GameObject> listItems1 = new Dictionary<Guid, GameObject>();

    #endregion

    #region Base Overrides
    #region Server

    /// <summary>
    /// Checks to see if currently in the lobby
    /// if so, grabs the PlayerList game object
    /// </summary>
    /// <param name="sceneName"></param>
    public override void OnRoomServerSceneChanged(string sceneName)
    {
        if (sceneName.Equals(base.RoomScene))
        {
            PlayerList = GameObject.FindGameObjectWithTag("RoomPlayerDisplayList");
        }

        base.OnRoomServerSceneChanged(sceneName);
    }

    public override void OnRoomClientDisconnect(NetworkConnection conn)
    {
        UpdateCurrentPlayers();
        base.OnRoomClientDisconnect(conn);
    }

    public override void OnRoomStopClient()
    {
        // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
        // going to the offline scene to avoid collision with the one that lives there.
        if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().name != offlineScene)
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        base.OnRoomStopClient();
    }

    public override void OnRoomStopServer()
    {
        // Demonstrates how to get the Network Manager out of DontDestroyOnLoad when
        // going to the offline scene to avoid collision with the one that lives there.
        if (gameObject.scene.name == "DontDestroyOnLoad" && !string.IsNullOrEmpty(offlineScene) && SceneManager.GetActiveScene().name != offlineScene)
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        base.OnRoomStopServer();
    }
    #endregion

    #region Client
    #endregion

    #endregion

    #region Server Detail Functions

    /// <summary>
    /// Used to set the server name externally
    /// </summary>
    /// <param name="name"></param>
    public void SetServerName(string name)
    {
        //NAME STRING NEED TO BE VALIDATED. Eg. No crazy shit
        ServerName = string.Copy(name);
    }

    /// <summary>
    /// Allows for the server name to be 
    /// refrenced elswhere.
    /// </summary>
    /// <returns></returns>
    public string GetServerName()
    {
        return string.Copy(ServerName);
    }

    /// <summary>
    /// Used to set the max amount of players
    /// for this server
    /// </summary>
    public void SetMaxPlayer(int amount)
    {
        //INSURE PLAYER COUNT IS RESONABLE eg. 6 NOT 1000
        //IF SO INFORM THE USER
        MaxPlayers = amount;
    }

    /// <summary>
    /// Allows the servers max players to be passed out
    /// </summary>
    /// <returns></returns>
    public int GetMaxPlayers()
    {
        return MaxPlayers;
    }

    private void UpdateCurrentPlayers()
    {
        //CurrentPlayers = roomPlayers.Count;
        CurrentPlayers = roomPlayers1.Count;

    }

    public int GetCurrentPlayers()
    {
        return CurrentPlayers;
    }

    #endregion

    #region Game Functions

    #endregion

    #region Lobby Functions
    public void AddPlayer(NetworkRoomPlayerExt player)
    {
        //roomPlayers.Add(player);
        roomPlayers1.Add(player.id, player);

        AddPlayerToDisplayList(player);

        UpdateCurrentPlayers();

        SetSeverLead(player);

        Debug.Log("CURRENT PLAYERS ----------------------- " + CurrentPlayers);
    }

    public void RemovePLayer(NetworkRoomPlayerExt player)
    {
        Debug.Log("----------------------------------Removing player");
        //REMOVE FROM ROOMPLAYERS LIST
        roomPlayers1.Remove(player.id);
        //REMOVE LIST ITEM  
        try
        {
            GameObject toRemove = listItems1[player.id];
            Destroy(toRemove);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }

        listItems1.Remove(player.id);

        
        Debug.Log("----------------------------------Removing player : Count : " + roomPlayers1.Count);
        //update currentPlayers
        UpdateCurrentPlayers();
    }

    /// <summary>
    /// Used to update a list item tied to a particular player.
    /// allows ready state display to be changed
    /// </summary>
    /// <param name="player"></param>
    public void UpdateListItem(NetworkRoomPlayerExt player)
    {
        foreach(KeyValuePair<Guid, NetworkRoomPlayerExt> pair in roomPlayers1)
        {
            Debug.Log(pair.Key + "   :   " + pair.Value.GetName());
        }

        try
        {
            LobbyPlayerListItem toUpdate = listItems1[player.id].GetComponent<LobbyPlayerListItem>();
            //Update ListItem Name
            toUpdate.SetName(player.GetName());
            //Update ListItem readystate
            toUpdate.SetReadyState(player.IsReady);
        }
        catch(KeyNotFoundException e)
        {
            Debug.Log("!!!!!!!!!!!!!!CANT FIND KEY!!!!!!!!!!!!");
        }

        //RebuildPlayerList();
    }

    public void RebuildPlayerListName()
    {
        foreach (KeyValuePair<Guid, GameObject> item in listItems1)
        {
            LobbyPlayerListItem i = item.Value.GetComponent<LobbyPlayerListItem>();

            i.SetName(roomPlayers1[item.Key].GetName());
        }
    }

    public void RebuildPlayerListReady()
    {
        foreach(KeyValuePair<Guid, GameObject> item in listItems1)
        {
            LobbyPlayerListItem i = item.Value.GetComponent<LobbyPlayerListItem>();

            Debug.Log(i.Name.text +  " : " + i.ReadyState.text);

            i.SetReadyState(roomPlayers1[item.Key].IsReady);
        }
    }

    /// <summary>
    /// Allows the last player in the list of room slots
    /// to be added to the displayed player list
    /// </summary>
    private void AddPlayerToDisplayList(NetworkRoomPlayerExt player)
    {

        GameObject toAdd = Instantiate(ListItemPrefab);

        //Add List item to list
        listItems1.Add(player.id, toAdd);

        toAdd.SetActive(false);

        string name = player.GetName();

        toAdd.GetComponentInChildren<TMP_Text>().text = name;

        //Set list item parent
        if (PlayerList == null)
        {
            PlayerList = GameObject.FindGameObjectWithTag("RoomPlayerDisplayList");
        }

        toAdd.transform.SetParent(PlayerList.transform, false);

        toAdd.SetActive(true);
    }

    private void SetSeverLead(NetworkRoomPlayerExt player)
    {
        bool test = CurrentPlayers == 1;

        player.SetIsLeader(test);
  
    }

    #endregion

    #region Getters
    public int GetMinimumPlayers()
    {
        return base.minPlayers;
    }
    #endregion
}

