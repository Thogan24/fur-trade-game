using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    public void Start()
    {
    }

    private RoomsCanvases roomsCanvases;
    public void FirstInitilize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }


    public TextMeshProUGUI roomName;
    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        if (SceneManager.GetActiveScene().name == "RoomSelect")
        {
            AudioSource buttonSounds = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
            buttonSounds.Play();
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        //Disconnection stuff
        options.CleanupCacheOnLeave = false;
        options.PlayerTtl = -1;
        options.EmptyRoomTtl = 300000;

        PhotonNetwork.KeepAliveInBackground = 60.0f * 1000.0f;


        if (roomName != null)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);

        }
    }
    
    public override void OnCreatedRoom()
    {
        Debug.Log("Created new room, " + this + ", " + roomName.text);
        Debug.Log(PhotonNetwork.CountOfRooms);
        roomsCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room " + message + " " + this);
    }


}
