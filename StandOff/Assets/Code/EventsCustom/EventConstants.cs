using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains string representations of each event name.  
/// This class was constructed in order to collect all 
/// events into the same place.  
/// 
/// A future optimization to this may be setting the value
/// of each event name via passing in the events to the inspector, as it
/// would save manual inputting of names.  However, this way also 
/// forces us to confirm spelling + purpose with each event name!
/// </summary>
public class EventConstants : MonoBehaviour
{
    #region Lobby Event Constants
    /// <summary>
    /// Raised by <see cref="NetworkRoomPlayerExt"/> when a player joins a lobby.
    /// Used to tell <see cref = "LobbyList"/> to update playerlist
    /// </summary>
    public const string LOBBYEVENT_PLAYER_JOINED_EVENT = "LobbyPlayerJoinedEvent";
    #endregion
}
