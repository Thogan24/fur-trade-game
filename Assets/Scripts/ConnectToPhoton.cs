using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToPhoton : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    int PlayerNumber = 1;
    string playerName;
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        playerName = "Player " + Random.Range(0, 10000).ToString(); // Random.Range(0, 10000).ToString

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            
            while (!testIfNickNameTaken(p))
            {
                Debug.Log("Ran this?");
                playerName = "Player " + Random.Range(0, 10000).ToString();
                if (testIfNickNameTaken(p))
                {
                    //PhotonNetwork.NickName = playerName;
                }
            }
            if (testIfNickNameTaken(p))
            {
                Debug.Log("Ran this");
                //PhotonNetwork.NickName = playerName;
            }
        }
        if(PhotonNetwork.NickName == null || (PhotonNetwork.NickName == ""))
        {
            //PhotonNetwork.NickName = playerName;
        }
        Debug.Log(PhotonNetwork.NickName);
        PhotonNetwork.ConnectUsingSettings();
        if (PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.Disconnect();
        }
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("connected");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
    public override void OnDisconnected(DisconnectCause Cause)
    {
        print("Disconnected from server: " + Cause.ToString());
    }

    public bool testIfNickNameTaken(Player player)
    {
        if (player.NickName == playerName)
        {
            return false;
        }
        return true;
    }
}


