using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private PlayerListing playerListing;

    private List<PlayerListing> playerList = new List<PlayerListing>();

    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    private void GetCurrentRoomPlayers()
    {
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }

    }

    private void AddPlayerListing(Player player)
    {
        PlayerListing listing = Instantiate(playerListing, new Vector3((float)727.2, 367 - PhotonNetwork.CountOfPlayers * 2, 0), Quaternion.identity, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            playerList.Add(listing);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("PLAYER ENTERED!!!");
        AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        int index = playerList.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(playerList[index].gameObject);
            playerList.RemoveAt(index);
        }

    }
    
}
