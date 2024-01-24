using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class SixNationsOnClickedScript : MonoBehaviourPunCallbacks
{
    public GameManager gameManager;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SixNationsButtonClicked()
    {
        string Value = PhotonNetwork.AuthValues.UserId;
        Debug.Log("Button PlayerID Value: " + Value);
        gameObject = GameObject.Find("GameManager");
        gameManager = gameObject.GetComponent<GameManager>();
        gameManager.SixNations = Value;
        gameManager.SixNationsJoined = true;
        //Debug.Log(gameManager.SixNations);

    }


}
