using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// FOR ENEMY TEAM TRADING BUTTON
public class MunseeTradingButtonOnClick : MonoBehaviour
{
    public GameObject MunseeTradingButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        MunseeTradingButton = this.gameObject;
    }

    public void MunseeTradingOnClick()
    {
        Debug.Log("Hello");
        Debug.LogError("Munsee trade button clicked");
        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString()); //  After being mapped

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker, PhotonMessageInfo info) // 
    {
        Debug.Log(info.Sender.ToString());
        Debug.Log(PhotonNetwork.LocalPlayer.ToString());
        Debug.Log("UserID of Clicker: " + userIDOfClicker);
        if (info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString())
        {

            gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
            string playerString = PhotonNetwork.LocalPlayer.ToString();
            Debug.Log((gameManager.turn == 1 && playerString == gameManager.Dutch));
            Debug.Log(gameManager.turn == 2 && playerString == gameManager.Philipses);
            Debug.Log(playerString);
            if ((gameManager.turn == 1 && playerString == gameManager.Dutch) || (gameManager.turn == 2 && playerString == gameManager.Philipses) || (gameManager.turn == 3 && playerString == gameManager.SixNations) || (gameManager.turn == 4 && playerString == gameManager.Munsee))
            {
                Debug.Log(gameManager);

                Debug.Log("RPC is running");
                Debug.LogError("UserID of Clicker: " + userIDOfClicker);
                this.GetComponent<PhotonView>().RPC("teamsThatAreTrading", RpcTarget.All, userIDOfClicker);
            }
        }
        
    }

    [PunRPC]
    void teamsThatAreTrading(string userIDOfClicker)
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        gameManager.MunseeTrading = true;
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
        gameManager.CallReactivateTeamFlagsRPC();
        return;
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
