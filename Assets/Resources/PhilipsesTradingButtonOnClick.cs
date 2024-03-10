using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// FOR ENEMY TEAM TRADING BUTTON
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
        Debug.Log("greyying out");
        if (!gameManager.DutchTrading)
        {
            for (int i = 0; i < gameManager.DutchTradeButton.Length; i++)
            {
                gameManager.DutchTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
            }
                
        }
        if (!gameManager.PhilipsesTrading)
        {
            for (int i = 0; i < gameManager.PhilipsesTradeButton.Length; i++)
            {
                gameManager.PhilipsesTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
            }
        }
        if (!gameManager.MunseeTrading)
        {
            for (int i = 0; i < gameManager.MunseeTradeButton.Length; i++)
            {
                gameManager.MunseeTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
            }
        }
        if (!gameManager.SixNationsTrading)
        {
            for (int i = 0; i < gameManager.SixNationsTradeButton.Length; i++)
            {
                gameManager.SixNationsTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.4f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
