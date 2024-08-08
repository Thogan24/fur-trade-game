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
    private RoomsCanvases roomsCanvases;
    public int changeInYFromListings = 52;

    public void FirstInitilize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }

    public override void OnJoinedRoom()
    {
        roomsCanvases.CurrentRoomCanvas.Show();
    }
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
                RoomListing listing = Instantiate(roomListing, content); // new Vector3(0, 0 - (changeInYFromListings * roomListingList.Count), 0), Quaternion.identity,
                listing.transform.position = listing.transform.position + new Vector3(0, 0 - (changeInYFromListings * roomListingList.Count), 0);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    roomListingList.Add(listing);
                }
            }
                
            
            
        }
    }
}
