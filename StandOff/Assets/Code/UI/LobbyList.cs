using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// DELETE
/// </summary>
public class LobbyList : MonoBehaviour
{

    public NetworkRoomManager networkRoomManager;

    [SerializeField]
    private GameObject PlayerList;

    [SerializeField]
    private GameObject ListItemPrefab;

    public RectTransform LayoutRoot;

    private List<GameObject> listItems = new List<GameObject>();


    //public override void OnRoomClientEnter()
    //{
    //    AddPlayerToList();
    //    base.OnRoomClientEnter();
    //}

    //public void AddPlayerToList()
    //{
    //    GameObject toAdd = Instantiate(ListItemPrefab);

    //    toAdd.SetActive(false);

    //    toAdd.transform.SetParent(PlayerList.transform, false);

    //    //Add the details of the last player to join
    //    NetworkRoomPlayer newplayer = base.roomSlots[roomSlots.Count-1];
    //    string name = newplayer.GetComponent<NetworkRoomPlayerExt>().GetName();
    //    toAdd.GetComponentInChildren<TMP_Text>().text = name;

    //    //Add List item to list
    //    listItems.Add(toAdd);

    //    toAdd.SetActive(true);
    //}

    /// <summary>
    /// BAD CODE dont know what I was thinking ------MARKED FOR DELETE------
    /// </summary>
    public void UpdatePlayerList()
    {
        Debug.Log("UPDATING PLAYER LIST Stage 1 | " + networkRoomManager.roomSlots.Count);


        foreach (NetworkRoomPlayer current in networkRoomManager.roomSlots)
        {
            Debug.Log("UPDATING PLAYER LIST 2");

            GameObject toAdd = Instantiate(ListItemPrefab);

            toAdd.SetActive(false);

            toAdd.transform.SetParent(PlayerList.transform, false);

            toAdd.GetComponentInChildren<TMP_Text>().text = current.gameObject.GetComponent<NetworkRoomPlayerExt>().GetName();

            listItems.Add(toAdd);

            toAdd.SetActive(true);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(LayoutRoot);

    }
}
