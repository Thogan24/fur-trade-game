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

    private List<RoomListing> roomListingList = new List<RoomListing>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room listings updated");
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = roomListingList.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(roomListingList[index].gameObject);
                    roomListingList.RemoveAt(index);
                }
            }
            else
            {
                Debug.Log("Instantiating...");
                RoomListing listing = Instantiate(roomListing, content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    roomListingList.Add(listing);
                }
            }
                
            
            
        }
    }
}
