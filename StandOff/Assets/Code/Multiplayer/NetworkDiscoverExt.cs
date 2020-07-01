using UnityEngine;
using Mirror.Discovery;
using System;
using System.Net;

/// <summary>
/// Class to override NetworkDiscovery Functions
/// </summary>
public class NetworkDiscoverExt : NetworkDiscovery
{
    /// <summary>
    /// Overider of bas implementation
    /// to allow extedned server details to be passed via a server request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    protected override ServerResponse ProcessRequest(ServerRequest request, IPEndPoint endpoint)
    {
        //Nab a refrence to the NetworkRoomManagerExt
        NetworkRoomManagerExt netManager = transport.gameObject.GetComponent<NetworkRoomManagerExt>();

        try
        {
            // this is an example reply message,  return your own
            // to include whatever is relevant for your game
            return new ServerResponse
            {
                serverId = ServerId,
                uri = transport.ServerUri(),
                name = netManager.GetServerName(),
                maxPlayers = netManager.GetMaxPlayers(),
                currentPlayers = netManager.GetCurrentPlayers()
            };
        }
        catch (NotImplementedException)
        {
            Debug.LogError($"Transport {transport} does not support network discovery");
            throw;
        }
    }
}
