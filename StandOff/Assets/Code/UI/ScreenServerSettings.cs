using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ScreenServerSettings : MonoBehaviour
{
    #region Public Vars
    public NetworkRoomManagerExt netManager;
    public NetworkDiscoverExt netDiscover;

    public TMP_InputField ServerNameInput;
    public TMP_InputField MaxPlayerInput;
    #endregion

    #region Private Vars
    private bool ServerNameSet;     //unused
    private bool MaxPlayersSet;     //unused

    private Color WarningRedTextColor = new Color(1,0,0,0.5f);
    private Color WarningRedBackground;
    #endregion

    #region Unity API
    private void OnEnable()
    {
        ResetServerChecks();
    }

    private void OnDisable()
    {
        ResetServerChecks();
    }

    #endregion

    #region Server Sceen Functions

    public void SetServerName()
    {
        string name = ServerNameInput.text;

        netManager.SetServerName(name);
    }

    public void SetMaxPlayers()
    {
        int amount = 0;

        if(Int32.TryParse( MaxPlayerInput.text, out amount))
        {
            netManager.SetMaxPlayer(amount);
        }
        
    }

    public void Launch()
    {
        if (!ServerNameCheck() || !ServerMaxPlayerCheck())
        {
            ServerMaxPlayerCheck();
            return;
        }
        else
        {
            CheckForManagers();
            netManager.StartHost();
            netDiscover.AdvertiseServer();
        }
    }
    #endregion

    #region Utils
    /// <summary>
    /// Used to check that the serve name has been set properly
    /// </summary>
    /// <returns></returns>
    private bool ServerNameCheck()
    {
        //check Server name is set
        if (ServerNameInput.text == null || ServerNameInput.text == "")
        {
            //if not change default text to notify player
            TMP_Text textObject = ServerNameInput.placeholder.GetComponent<TMP_Text>();
            SetWarningText(textObject, WarningRedTextColor, Color.blue, "ENTER SERVER NAME");
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool ServerMaxPlayerCheck()
    {
        int currentSetting;

        Int32.TryParse(MaxPlayerInput.text, out currentSetting);

        if(currentSetting >= netManager.GetMinimumPlayers())
        {
            return true;
        }
        else
        {
            TMP_Text textObject = MaxPlayerInput.placeholder.GetComponent<TMP_Text>();

            SetWarningText(textObject, WarningRedTextColor, Color.blue, "ENTER MAX PLAYERS");
            return false;
        }
    }

    private void SetWarningText(TMP_Text textObj, Color textColor, Color BackgroundColor, string message)
    {
        textObj.text = message;
        textObj.color = textColor;
    }

    /// <summary>
    /// Allows managers to be reassigned after a server is launched / quit
    /// </summary>
    private void CheckForManagers()
    {
        if(netManager == null || netDiscover == null)
        {
            GameObject NetManOb;
            NetManOb = GameObject.FindGameObjectWithTag("NetworkManager");

            if (netManager == null)
            {
                netManager = NetManOb.GetComponent<NetworkRoomManagerExt>();
            }

            if (netDiscover == null)
            {
                netDiscover = NetManOb.GetComponent<NetworkDiscoverExt>();
            }
        }

    }

    private void ResetServerChecks()
    {
        ServerNameSet = false;
        MaxPlayersSet = false;
    }
    #endregion
}
