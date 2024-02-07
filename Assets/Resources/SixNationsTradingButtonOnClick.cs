using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SixNationsTradingButtonOnClick : MonoBehaviour
{
    public GameObject sixNationsTradingButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        sixNationsTradingButton = this.gameObject;
    }

    public void SixNationsTradingOnClick()
    {
        Debug.Log("Hello");
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        Debug.Log(gameManager);
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString()); //  After being mapped

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        Debug.LogError("UserID of Clicker: " + userIDOfClicker);
        if (gameManager.SixNations == userIDOfClicker)
        {
            // Skip to next turn
        }

        else
        {
            gameManager.SixNationsTrading = true;
            string team = gameManager.findPlayerTeam(userIDOfClicker);
            Debug.LogError("Team Selected: " + team);
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
