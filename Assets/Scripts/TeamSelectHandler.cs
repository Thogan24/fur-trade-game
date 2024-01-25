using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class TeamSelectHandler : MonoBehaviourPunCallbacks
{
    public GameManager gameManager;
    public GameObject gameManagerPrefab;

    public Text debug;


    public GameObject debugger;

    public PhotonView playerPrefab;
    public PhotonView sixNationsButtonInstantiated;
    public PhotonView dutchButtonInstantiated;

    public GameObject Dutch;
    public GameObject SixNations;
    public GameObject theCanvas;



    bool menuOpen = false;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        // User ID Assignment
        Debug.Log("Joined a room.");
        RoomOptions roomOptions = new RoomOptions();


        roomOptions.PublishUserId = true;
        AuthenticationValues authValues = new AuthenticationValues("0");
        PhotonNetwork.AuthValues.UserId = gameManager.userID.ToString();

        Debug.LogError("PhotonView teamselecthandler: " + this.GetComponent<PhotonView>().ViewID);

        PhotonNetwork.NickName = gameManager.userID.ToString();
        //Debug.LogError("YOUR AuthValue:" + PhotonNetwork.AuthValues.UserId);



        foreach (Player player in PhotonNetwork.PlayerList)
        {
            //Debug.LogError("Player:" + PhotonNetwork.LocalPlayer.NickName);
            //Debug.LogError("Player AuthValue:" + PhotonNetwork.AuthValues.UserId);
            //Debug.LogError("GameManager UserID:" + gameManager.userID);
        }


        //Debug.LogError(PhotonNetwork.AuthValues.UserId);
        SixNations.transform.parent = theCanvas.transform;
        Dutch.transform.parent = theCanvas.transform;
        //Debug.Log(SixNations.transform.position);
        SixNations.transform.position = new Vector3(400, 400, 0);
        Dutch.transform.position = new Vector3(900, 200, 0);
        SixNations.SetActive(true);
        Dutch.SetActive(true);
        //DEBUG.SetActive(true);
        //DEBUG.transform.position = new Vector3(400, 200, 0);

    }

}
