using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class ServerListItem : MonoBehaviour
{
    private string serverName;
    private string serverIP;
    private int maxPlayers;
    private int currentPlayers;
    private long serverID;

    public TMP_Text ServerName;
    public TMP_Text ServerIP;
    public TMP_Text PlayerCount;

    public void InitServerListItem(long id, string name, string ip, int currentPlayers, int maxPlayer)
    {
        this.serverID = id;
        this.serverName = name;
        this.serverIP = ip;
        this.currentPlayers = currentPlayers;
        this.maxPlayers = maxPlayer;

        PopulateListItem();
    }

    public void UpdateCurrentPlayers(int amount)
    {
        currentPlayers = amount;
        UpdatePlayerCount();
    }

    public long GetServerID()
    {
        long id = serverID;
        return id;
    }

    public void Connect()
    {
        ScreenJoinGame reff = GetComponentInParent<ScreenJoinGame>();
        reff.Connect(serverID);
    }
    private void PopulateListItem()
    {
        ServerName.text = serverName;
        ServerIP.text = serverIP;
        UpdatePlayerCount();
    }

    private void UpdatePlayerCount()
    {
        PlayerCount.text = currentPlayers + " / " + maxPlayers;
    }
}
