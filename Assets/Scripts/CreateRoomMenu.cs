using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    public void Start()
    {
    }



    public TextMeshProUGUI roomName;
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        if (roomName != null)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);

        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created new room, " + this + ", " + roomName.text);
        Debug.Log(PhotonNetwork.CountOfRooms);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room " + message + " " + this);
    }


}
