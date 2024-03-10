using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// Green trade button
public class TradeButtonOnClick : MonoBehaviour
{

    public GameManager gameManager;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TradeButtonOnClicked()
    {
        Debug.Log("Trade Button Clicked");
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString());

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        string team = gameManager.findPlayerTeam(userIDOfClicker);

        Debug.LogError("Team Selected: " + team);
        if (team == "Dutch")
        {
            Debug.LogError("Dutch Accepted");
            if (gameManager.DutchTrading)
            {
                if (gameManager.DutchAccepted == false)
                {
                    gameManager.numberOfAcceptedTeams++;
                }
                gameManager.DutchAccepted = true;
                
                if(gameManager.numberOfAcceptedTeams == 2)
                {
                    Debug.LogError("Cards being switched, calling RPC");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
                else if(gameManager.numberOfAcceptedTeams > 2)
                {
                    Debug.LogError("Number of accepted teams above 2, something is wrong");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
            }
            
        }
        if (team == "Philipses")
        {
            Debug.LogError("Philipses Accepted");
            if (gameManager.PhilipsesTrading)
            {
                if (gameManager.PhilipsesAccepted == false)
                {
                    gameManager.numberOfAcceptedTeams++;
                }
                gameManager.PhilipsesAccepted = true;

                if (gameManager.numberOfAcceptedTeams == 2)
                {
                    Debug.LogError("Cards being switched, calling RPC");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
            }
        }
        if (team == "SixNations")
        {
            Debug.LogError("Six Nations Accepted");
            if (gameManager.SixNationsTrading)
            {
                if (gameManager.SixNationsAccepted == false)
                {
                    gameManager.numberOfAcceptedTeams++;
                }
                gameManager.SixNationsAccepted = true;

                if (gameManager.numberOfAcceptedTeams == 2)
                {
                    Debug.LogError("Cards being switched, calling RPC");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
            }
        }
        if (team == "Munsee")
        {
            Debug.LogError("Munsee Accepted");
            if (gameManager.MunseeTrading)
            {
                if (gameManager.MunseeAccepted == false)
                {
                    gameManager.numberOfAcceptedTeams++;
                }
                gameManager.MunseeAccepted = true;

                if (gameManager.numberOfAcceptedTeams == 2)
                {
                    Debug.LogError("Cards being switched, calling RPC");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
            }
        }
    }
}
