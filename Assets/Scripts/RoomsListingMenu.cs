using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private RoomListing roomListing;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room listings updated");
        foreach (RoomInfo info in roomList)
        {
            Debug.Log("Instantiating...");
            RoomListing listing = Instantiate(roomListing, content);
            if (listing != null)
            {
                listing.SetRoomInfo(info);
            }
        }
    }
}
