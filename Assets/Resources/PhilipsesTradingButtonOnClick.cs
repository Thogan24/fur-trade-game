using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhilipsesTradingButtonOnClick : MonoBehaviour
{
    public GameObject PhilipsesTradingButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        PhilipsesTradingButton = this.gameObject;
    }

    public void PhilipsesTradingOnClick()
    {
        Debug.Log("Hello");
        Debug.LogError("ItsWhenClickingHere");
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString()); //  After being mapped

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        Debug.Log(gameManager);

        Debug.Log("RPC is running");
        Debug.Log("UserID of Clicker: " + userIDOfClicker);
        Debug.LogError("UserID of Clicker: " + userIDOfClicker);
        //if (gameManager.Philipses == userIDOfClicker)
        //{
        //Debug.Log("This happened.");
        // Skip to next turn
        //}

        //else
        //{
        Debug.Log("Works right, " + userIDOfClicker);
        gameManager.PhilipsesTrading = true;
        string team = gameManager.findPlayerTeam(userIDOfClicker);

        Debug.LogError("Team Selected: " + team);
        if (team == "Dutch")
        {
            Debug.LogError("Dutch is Trading");
            gameManager.DutchTrading = true;
            Debug.LogError(gameManager.DutchTrading);
        }
        if (team == "Philipses")
        {
            Debug.LogError("Philipses is Trading");
            gameManager.PhilipsesTrading = true;
        }
        if (team == "Philipses")
        {
            Debug.LogError("Six Nations is Trading");
            gameManager.PhilipsesTrading = true;
        }
        if (team == "Munsee")
        {
            Debug.LogError("Munsee is Trading");
            gameManager.MunseeTrading = true;
        }
        teamsThatAreTrading(team);
        greyOutButtons();
        return;
        //}

    }

    void teamsThatAreTrading(string team)
    {

    }

    [PunRPC]
    void greyOutButtons()
    {
        if (!gameManager.DutchTrading)
        {
            gameManager.DutchTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
        }
        if (!gameManager.PhilipsesTrading)
        {
            gameManager.PhilipsesTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
        }
        if (!gameManager.MunseeTrading)
        {
            gameManager.MunseeTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
        }
        if (!gameManager.SixNationsTrading)
        {
            gameManager.SixNationsTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
