using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// FOR ENEMY TEAM TRADING BUTTON
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
        Debug.LogError("ItsWhenClickingHere");
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString()); //  After being mapped

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        string playerString = PhotonNetwork.LocalPlayer.ToString();
        if ((gameManager.turn == 1 && playerString == gameManager.Dutch) || (gameManager.turn == 2 && playerString == gameManager.Philipses) || (gameManager.turn == 3 && playerString == gameManager.SixNations) || (gameManager.turn == 4 && playerString == gameManager.Munsee))
        {
            Debug.Log(gameManager);

            Debug.Log("RPC is running");
            Debug.Log("UserID of Clicker: " + userIDOfClicker);
            Debug.LogError("UserID of Clicker: " + userIDOfClicker);
            //if (gameManager.SixNations == userIDOfClicker)
            //{
            //Debug.Log("This happened.");
            // Skip to next turn
            //}

            //else
            //{
            Debug.Log("Works right, " + userIDOfClicker);
            gameManager.SixNationsTrading = true;
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
            if (team == "SixNations")
            {
                Debug.LogError("Six Nations is Trading");
                gameManager.SixNationsTrading = true;
            }
            if (team == "Munsee")
            {
                Debug.LogError("Munsee is Trading");
                gameManager.MunseeTrading = true;
            }

            greyOutButtons();
            return;
            //}
        }
    }

    void teamsThatAreTrading(string team)
    {
        
    }
    [PunRPC]
    void greyOutButtons()
    {
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
