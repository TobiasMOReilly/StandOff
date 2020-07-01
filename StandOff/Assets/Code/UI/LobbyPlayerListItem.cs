using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyPlayerListItem : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text ReadyState;

    public void SetName(string name)
    {
        Name.text = name;
    }

    public void SetReadyState(bool state)
    {
        if(state)
        {
            ReadyState.text = "Ready";
            ReadyState.color = Color.green;
        }
        else
        {
            ReadyState.text = "Waiting";
            ReadyState.color = Color.red;
        }
    }
}
