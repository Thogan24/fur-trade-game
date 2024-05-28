using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ClearTradeButtonOnClick : MonoBehaviour
{
    public GameManager gameManager;
    
    void Start()
    {

    }

    void Update()
    {

    }

    public void ClearTradeButtonOnClicked()
    {
        Debug.Log("Clear Trade Button Clicked");
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString());

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.clearTradeButton = true;
        gameManager.GetComponent<PhotonView>().RPC("clearAllTrades", RpcTarget.All);
    }
}
