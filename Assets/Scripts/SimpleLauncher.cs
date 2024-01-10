using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SimpleLauncher : MonoBehaviourPunCallbacks
{

    public PhotonView playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        AuthenticationValues authValues = new AuthenticationValues("0");
        PhotonNetwork.AuthValues.UserId = 1.ToString();
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom(){
        Debug.Log("Joined a room.");
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }

}