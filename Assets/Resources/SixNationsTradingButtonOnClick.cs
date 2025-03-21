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
        Debug.LogError("Six Nations Button Clicked");
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
        
        gameManager.SixNationsTrading = true;
        gameManager.receivingTeam = "Six Nations";
        string team = gameManager.findPlayerTeam(userIDOfClicker);

        Debug.LogError("Team Selected: " + team);
        if (team == "Dutch")
        {
            Debug.LogError("Dutch is Trading");
            gameManager.DutchTrading = true;
            gameManager.givingTeam = "Dutch";
        }
        if (team == "Philipses")
        {
            Debug.LogError("Philipses is Trading");
            gameManager.PhilipsesTrading = true;
            gameManager.givingTeam = "Philipses";
        }
        if (team == "SixNations")
        {
            Debug.LogError("Six Nations is Trading");
            gameManager.SixNationsTrading = true;
            gameManager.givingTeam = "Six Nations";

        }
        if (team == "Munsee")
        {
            Debug.LogError("Munsee is Trading");
            gameManager.MunseeTrading = true;
            gameManager.givingTeam = "Munsee";

        }
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Six Nations is calling the flag");

            gameManager.GetComponent<PhotonView>().RPC("FlagButtonBackgroundFadeInFadeOutEnd", RpcTarget.All);

        }
        greyOutButtons();
        setLabels();
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

    void setLabels()
    {
        for (int ind = 0; ind < gameManager.givingTeamLabels.Length; ind++)
        {
            if (gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.givingTeam || gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.receivingTeam)
            {
                gameManager.givingTeamLabels[ind].GetComponent<Text>().text = "Cards you give";
            }
            else
            {
                gameManager.givingTeamLabels[ind].GetComponent<Text>().text = gameManager.givingTeam + "\u2192" + gameManager.receivingTeam;
            }

        }
        for (int ind = 0; ind < gameManager.receivingTeamLabels.Length; ind++)
        {
            this.GetComponent<PhotonView>().RPC("Debugr", RpcTarget.All, ind);
            Debug.Log(gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag + " " + gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.givingTeam + " " + gameManager.givingTeam + " " + gameManager.receivingTeam);
            if (gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.givingTeam || gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.receivingTeam)
            {
                gameManager.receivingTeamLabels[ind].GetComponent<Text>().text = "Cards you receive";
            }
            else
            {
                gameManager.receivingTeamLabels[ind].GetComponent<Text>().text = gameManager.receivingTeam + "\u2192" + gameManager.givingTeam;
            }

        }
    }
    [PunRPC]
    void Debugr(int ind)
    {
        Debug.Log(gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag + " " + gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.givingTeam + " " + gameManager.givingTeam + " " + gameManager.receivingTeam);
        Debug.Log("Sender is " + ((gameManager.Dutch == PhotonNetwork.LocalPlayer.ToString()) ? "Dutch" : " ") + ((gameManager.Philipses == PhotonNetwork.LocalPlayer.ToString()) ? "Philipses" : " ") + ((gameManager.SixNations == PhotonNetwork.LocalPlayer.ToString()) ? "SixNations" : " ") + ((gameManager.Munsee == PhotonNetwork.LocalPlayer.ToString()) ? "Munsee" : " "));
    }


    // Update is called once per frame
    void Update()
    {

    }
}
