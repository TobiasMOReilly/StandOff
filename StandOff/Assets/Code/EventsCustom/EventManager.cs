using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages and contains all events utilized throughout the course of the game.
/// 
/// Utilizes a hashmap from eventName --> GameEvent for O(1) lookup after population.
/// 
/// To add a new event, create the GameEvent in the events folder,
/// add it to this scriptable object by increasing the list size, 
/// and then add its name as a constant in <see cref="EventConstants"/>.
/// 
/// To raise a gameevent, simply call RaiseGameEvent, overloaded to account for
/// the object layer.
/// </summary>
[CreateAssetMenu(fileName = "EventManager", menuName = "System/EventManager")]
public class EventManager : ScriptableObject
{
    [SerializeField]
    [Header("Game Events")]
    [Tooltip("Add every GameEvent to this list!")]
    private List<GameEvent> gameEvents;

    /// <summary>
    /// Dictionary mapping name to Event for O(1) lookup.
    /// </summary>
    private Dictionary<string, GameEvent> eventMap;

    /// <summary>
    /// Raises the passed in event, after confirming if it is 
    /// valid.
    /// </summary>
    /// <param name="eventName"></param>
    public void RaiseGameEvent(string eventName)
    {
        // Raise the game event if not null; else, debug.
        GameEvent gameEvent = this.GetGameEvent(eventName);
        if (gameEvent == null)
        {
            Debug.Log("ERROR: Invalid game event name: " + eventName);
        }
        else
        {
            this.GetGameEvent(eventName).Raise();
        }
    }

    /// <summary>
    /// Overridden raiser -- allows for callers to take advantage 
    /// of layer systems for more specific sub-calls.
    /// </summary>
    /// <param name="eventName"></param>
    public void RaiseGameEvent(string eventName, int layer)
    {
        // Raise the game event if not null; else, debug.
        GameEvent gameEvent = this.GetGameEvent(eventName);
        if (gameEvent == null)
        {
            Debug.Log("ERROR: Invalid game event name: " + eventName);
        }
        else
        {
            this.GetGameEvent(eventName).Raise(layer);
        }
    }

    /// <summary>
    /// Gets a GameEvent from the dictionary of possible events.
    /// If there is no such event by name (to lower), this returns null.
    /// </summary>
    /// <param name="eventName">The string name of the event.</param>
    /// <returns>The game event associated with the passed in string, if any.</returns>
    private GameEvent GetGameEvent(string eventName)
    {
        if (eventMap.TryGetValue(eventName.ToLower(), out GameEvent gameEvent))
        {
            return gameEvent;
        }
        return null;
    }

    /// <summary>
    /// Maps each game event's name to the game event itself, after
    /// instantializing the eventMap.
    /// </summary>
    private void PopulateGameEventMap()
    {
        eventMap = new Dictionary<string, GameEvent>();
        foreach (GameEvent gameEvent in this.gameEvents)
        {
            eventMap.Add(gameEvent.name.ToLower(), gameEvent);
        }
    }

    /// <summary>
    /// When this script is first called, it will populate our hashmap!
    /// </summary>
    private void OnEnable()
    {
        this.PopulateGameEventMap();
    }
}
