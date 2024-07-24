using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToPhoton : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        print("connected");
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause Cause)
    {
        print("Disconnected from server: " + Cause.ToString());
    }
}
