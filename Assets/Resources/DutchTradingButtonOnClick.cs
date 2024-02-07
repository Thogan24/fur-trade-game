using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DutchTradingButtonOnClick : MonoBehaviour
{
    public GameObject dutchTradingButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        dutchTradingButton = this.gameObject;
    }

    public void DutchTradingOnClick()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString()); //  After being mapped
        
    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        Debug.LogError(userIDOfClicker);
        if(gameManager.Dutch == userIDOfClicker)
        {
            // Skip to next turn
        }

        else
        {
            gameManager.DutchTrading = true;
            string team = gameManager.findPlayerTeam(userIDOfClicker);
            Debug.Log(team);
            teamsThatAreTrading(team);


        }
        
    }

    void teamsThatAreTrading(string team)
    {
        if (team == "Dutch")
        {
            gameManager.DutchTrading = true;
        }
        if (team == "Philipses")
        {
            gameManager.PhilipsesTrading = true;
        }
        if (team == "SixNations")
        {
            gameManager.SixNationsTrading = true;
        }
        if (team == "Munsee")
        {
            gameManager.MunseeTrading = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
