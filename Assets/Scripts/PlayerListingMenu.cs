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
    int changeInYFromListings = 55;

    private void Awake()
    {
        Debug.Log("Have awaken.");
        GetCurrentRoomPlayers();
    }

    private void GetCurrentRoomPlayers()
    {
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log("This many players (count me)");
            AddPlayerListing(playerInfo.Value);
        }

    }

    private void AddPlayerListing(Player player)
    {
        Debug.Log("Adding player listing");
        PlayerListing listing = Instantiate(playerListing, content); //new Vector3((float)727.2, 367 - PhotonNetwork.CountOfPlayers * 2, 0), Quaternion.identity
        listing.transform.position = listing.transform.position + new Vector3(0, 0 - (changeInYFromListings * PhotonNetwork.CurrentRoom.Players.Count), 0);
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
