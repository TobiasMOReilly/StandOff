using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class enables the setting / loading of the players name
/// via the character customization screen.
/// </summary>
public class PlayerNameInput : MonoBehaviour
{
    #region Variables

    [Header("Input Fields")]
    [Tooltip("Link to the player name input field")]
    [SerializeField]
    private TMP_InputField NameInput;

    #endregion

    #region Unity API
    
    void Start()
    {
        LoadPlayerName();
    }

    #endregion

    #region Load / Set Functions
    /// <summary>
    /// Loads the players name from Player Prefrences.
    /// Note : Use PlayerPrefsManger to set / retrieve data
    /// </summary>
    private void LoadPlayerName()
    {
        if (PlayerPrefsManager.GetName() != null)
        {
            NameInput.text = PlayerPrefsManager.GetName();
        }
        else
        {
            Debug.Log("NAME LOADER - NO NAME FOUND");
        }
    }

    /// <summary>
    /// Sets the players name via the PlayerPrefsManager
    /// </summary>
    public void SetPlayerName()
    {
        PlayerPrefsManager.SetPref(PlayerPrefsManager.PPREF_PLAYER_NAME, NameInput.text);
    }

    #endregion

}
