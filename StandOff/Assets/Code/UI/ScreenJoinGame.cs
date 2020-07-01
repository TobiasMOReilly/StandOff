using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Works with networkDiscovery to list avalible servers
/// on the local network...
/// ------FIX-------
/// Currently hard link between scripts!!!!
/// </summary>
public class ScreenJoinGame : MonoBehaviour
{
    //Link to the network manager
    [SerializeField]
    private NetworkManager NetworkManager;
    //Link to the network discovery script
    [SerializeField]
    private NetworkDiscovery NetworkDiscovery;

    //List of active servers found
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    private Dictionary<long,GameObject> ServerListItems = new Dictionary<long,GameObject>();

    [Header("Server List Game Objects")]
    public GameObject ServerList;

    public GameObject ServerListItem;

    #region Unity API

    /// <summary>
    /// When the Joion Screen is enabled
    /// --Begin searching for servers
    /// </summary>
    private void OnEnable()
    {
        RefreshServerList();
    }
    private void OnDisable()
    {
        ClearServerList();
    }
    #endregion

    #region Mirror API

    /// <summary>
    /// Used by network descovery script to add found server to discovered servers list
    /// </summary>
    /// <param name="info"></param>
    public void OnDiscoveredServer(ServerResponse info)
    {
        //Check if server already found
        if (discoveredServers.ContainsKey(info.serverId))
        {
            //Update Server display
        }
        else
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers.Add(info.serverId, info);
            
            GameObject newServerItem = Instantiate(ServerListItem); //create a new list item
            newServerItem.transform.SetParent(ServerList.transform, false); //set new item as a child of the server list
            ServerListItem data = newServerItem.GetComponent<ServerListItem>();//get access to ServerListItem

            //SET UP ADDITIONAL DATA FROM RESPONSE
            data.InitServerListItem(
                info.serverId,
                info.name,
                discoveredServers[info.serverId].EndPoint.Address.ToString(),
                info.currentPlayers,
                info.maxPlayers
                );

            ServerListItems.Add(info.serverId,newServerItem);
        }

    }

    /// <summary>
    /// Used to connect to a chosen server
    /// </summary>
    /// <param name="info"></param>
    public void Connect(long id)
    {
        //CHECK IF SERVER IS FULL FIRST

        NetworkManager.StartClient(discoveredServers[id].uri);

    }

    #endregion

    public void RefreshServerList()
    {
        CheckForManagers();

        ClearServerList();

        NetworkDiscovery.StartDiscovery();
    }

    private void ClearServerList()
    {
        CheckForManagers();

        NetworkDiscovery.StopDiscovery(); // stop discovery
        discoveredServers.Clear(); // clear list of servers

        //destroy list items
        foreach(KeyValuePair<long,GameObject> ob in ServerListItems)
        {
            Destroy(ob.Value);
        }

        ServerListItems.Clear();
    }

    private void UpdateListItem(ServerResponse info)
    {

    }

    /// <summary>
    /// Allows managers to be reassigned after a server is launched / quit
    /// </summary>
    private void CheckForManagers()
    {
        if (NetworkManager == null || NetworkDiscovery == null)
        {
            GameObject NetManOb;
            NetManOb = GameObject.FindGameObjectWithTag("NetworkManager");

            if (NetworkManager == null)
            {
                NetworkManager = NetManOb.GetComponent<NetworkRoomManagerExt>();
            }

            if (NetworkDiscovery == null)
            {
                NetworkDiscovery = NetManOb.GetComponent<NetworkDiscoverExt>();
            }
        }

    }
}
