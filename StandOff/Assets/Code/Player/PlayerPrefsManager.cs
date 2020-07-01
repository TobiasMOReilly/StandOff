using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class manages the setting and loading of Player Prefrences
/// </summary>
public class PlayerPrefsManager : PlayerPrefs
{
    #region Player Prefs Constant keys

    public const string PPREF_PLAYER_NAME = "PlayerName";
    public const string PPREF_VOLUME_MUSIC = "VolumeMusic";
    public const string PPREF_VOLUME_SFX = "VolumeSFX";

    #endregion

    #region Pref Setters / Getters

    public static void SetPref(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public static void SetPref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public static void SetPref(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static string GetName()
    {
        string name = string.Copy(PlayerPrefs.GetString(PPREF_PLAYER_NAME));

        return name;
    }
    #endregion
}
