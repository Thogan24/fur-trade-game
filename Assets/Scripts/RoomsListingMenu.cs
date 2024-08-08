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
    public bool sameNameOnce = false;
    public bool sameNameTwice = false;


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
                /*foreach (RoomInfo info2 in roomList)
                {
                    Debug.Log(roomList.Count);
                    Debug.Log(roomListingList.Count);
                    Debug.Log(info2.Name + " " + info.Name);
                    if(info2.Name == info.Name)
                    {
                        if(sameNameOnce == true)
                        {
                            Debug.Log("same name twice");
                            sameNameTwice = true;
                        }
                        sameNameOnce = true;
                        
                    }
                } */
                foreach(RoomListing roomListing in roomListingList)
                {
                    if (roomListing.RoomInfo.Name == info.Name)
                    {
                        sameNameOnce = true;
                    }
                }
                Debug.Log(info.Name);
                Debug.Log(roomList.Count);
                Debug.Log(roomListingList.Count);
                if (!sameNameOnce)
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
                sameNameOnce = false;

                
            }
                
            
            
        }
    }
}
