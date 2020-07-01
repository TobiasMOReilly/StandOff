using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// Static class to streamline analytics event calls
/// Use to call an analytics event from elsewhere.
/// 
/// </summary>
public static class AnalyticsManager
{

    #region InGame Events
    #endregion

    #region UI Events
    /// <summary>
    /// Triggerd when the player starts a game
    /// </summary>
    public static void GameStarted()
    {
        Analytics.CustomEvent("GameStarted");
    }

    /// <summary>
    /// Used to track how many times a screen has been viewed
    /// </summary>
    public static void ScreenView(string screenName)
    {
        Analytics.CustomEvent(screenName + " Screen View");
    }

    /// <summary>
    /// Used to log data when the user quits the game
    /// </summary>
    /// <param name="timeToQuit">Time since the game was launched in minutes</param>
    public static void Quit(float timeToQuit)
    {
        Analytics.CustomEvent("TimeToQuit", new Dictionary<string, object>
        {
            { "TimeToQuit", timeToQuit / 60}
        });
    }
    #endregion

    #region Test Methods
    /// <summary>
    /// Call to check if analytics are firering correctly
    /// </summary>
    public static void TestAnalyitc()
    {
        AnalyticsResult ar = Analytics.CustomEvent("MyEvent");

        Debug.Log("Result = " + ar.ToString());
    }
    #endregion

    #region Example Analytics Calls
    //AnalyticsEvent.Custom("secret_found", new Dictionary<string, object>
    //{
    //    { "secret_id", secretID},
    //    { "time_elapsed", Time.timeSinceLevelLoad }
    //});
    #endregion
}
