using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class MunseeOnClickedScript : MonoBehaviour
{

    public GameObject myMunseeButton;
    void Start()
    {
        myMunseeButton = this.gameObject;
    }

    void Update()
    {

    }

    public void MunseeButtonClicked()
    {
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, this.transform.position, PhotonNetwork.LocalPlayer.ToString()); //  After being mapped

    }

    [PunRPC]
    void WhenClicked(Vector3 transform, string userIDOfClicker) // 
    {

        Debug.LogError("ismine: " + this.GetComponent<PhotonView>().IsMine + " viewid: " + this.GetComponent<PhotonView>().ViewID);
        Debug.LogError("User ID: " + userIDOfClicker);
        GameObject MunseeButton = GameObject.FindGameObjectWithTag("Munsee Button");
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        gameManager.Munsee = userIDOfClicker;


        PhotonNetwork.Destroy(MunseeButton);
        // If it didn't get destroyed yet for any reason
        PhotonNetwork.Destroy(myMunseeButton);
    }
}
