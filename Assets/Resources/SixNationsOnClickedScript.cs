using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class SixNationsOnClickedScript : MonoBehaviour
{

    public GameObject mySixNationsButton;
    void Start()
    {
        mySixNationsButton = this.gameObject;
    }

    public void SixNationsButtonClicked()
    {
        GameManager gameManager1 = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if(gameManager1.Dutch != PhotonNetwork.LocalPlayer.ToString() && gameManager1.Munsee != PhotonNetwork.LocalPlayer.ToString() && gameManager1.Philipses != PhotonNetwork.LocalPlayer.ToString() && gameManager1.SixNations != PhotonNetwork.LocalPlayer.ToString())
        {
            this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, this.transform.position, PhotonNetwork.LocalPlayer.ToString()); //  After being mapped
        }
        

    }

    [PunRPC]
    void WhenClicked(Vector3 transform, string userIDOfClicker) // 
    {

        Debug.LogError("ismine: " + this.GetComponent<PhotonView>().IsMine + " viewid: " + this.GetComponent<PhotonView>().ViewID);
        Debug.LogError("User ID: " + userIDOfClicker);
        GameObject SixNationsButton = GameObject.FindGameObjectWithTag("Six Nations Button");
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        gameManager.SixNations = userIDOfClicker;


        PhotonNetwork.Destroy(SixNationsButton);
        // If it didn't get destroyed yet for any reason
        PhotonNetwork.Destroy(mySixNationsButton);
    }
}
