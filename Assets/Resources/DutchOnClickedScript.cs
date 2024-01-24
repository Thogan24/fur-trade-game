using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class DutchOnClickedScript : MonoBehaviourPunCallbacks
{
    public GameManager gameManager;
    public GameObject gameObject;
    public TeamSelectHandler teamSelectHandler;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DutchButtonClicked()
    {
        canvas = GameObject.Find("Canvas");
        teamSelectHandler = canvas.GetComponent<TeamSelectHandler>();

        //Debug.Log(PhotonNetwork.AuthValues.UserId);
        string Value = PhotonNetwork.AuthValues.UserId;
        Debug.Log("Button PlayerID Value: " + Value);
        gameObject = GameObject.Find("GameManager");
        gameManager = gameObject.GetComponent<GameManager>();
        gameManager.DutchWestIndiaCompany = Value;
        gameManager.DutchJoined = true;
        //Debug.Log(gameManager.DutchWestIndiaCompany);
        //Debug.Log(teamSelectHandler.Dutch);
        PhotonNetwork.Destroy(teamSelectHandler.Dutch);
        
    }


}
