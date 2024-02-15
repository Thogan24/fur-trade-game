using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CardOnClick : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardOnClicked()
    {
        Debug.LogError("Card Clicked");
        // REMEMBER THIS IS HERE
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        string tag = this.gameObject.tag;
        gameManager.addCardToTrade(tag);
        //this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString());

    }
}