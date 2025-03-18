using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class RoomListing : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        text.text = roomInfo.Name + " | Max Players:" + roomInfo.MaxPlayers;
    }

    public void OnClick_Button()
    {
        Debug.Log("Trying to join the room");
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
