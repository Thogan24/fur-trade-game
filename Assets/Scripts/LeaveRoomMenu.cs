using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomsCanvases roomsCanvases;
    public void OnClick_LeaveRoom()
    {
        if (SceneManager.GetActiveScene().name == "RoomSelect")
        {
            AudioSource buttonSounds = GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<AudioSource>();
            buttonSounds.Play();
        }
        GameObject playerListings = GameObject.FindGameObjectWithTag("PlayerListings");
        playerListings.GetComponent<PlayerListingMenu>().playerCount = 0;
        PhotonNetwork.LeaveRoom(true);        
        roomsCanvases.CurrentRoomCanvas.Hide();

    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }
}
