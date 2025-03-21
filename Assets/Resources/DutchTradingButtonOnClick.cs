using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// FOR ENEMY TEAM TRADING BUTTON
public class DutchTradingButtonOnClick : MonoBehaviour
{
    public GameObject DutchTradingButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        DutchTradingButton = this.gameObject;
    }

    public void DutchTradingOnClick()
    {
        Debug.Log("Hello");
        Debug.LogError("Dutch Trading Button Clicked");
        Debug.Log(this.gameObject.tag);
        Debug.Log(this.gameObject.name);
        Debug.Log(this.gameObject.transform.parent.parent.name);
        Debug.Log(this.gameObject.transform.parent.name);
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        // bandage fix
        if (gameManager.turn == 1 && PhotonNetwork.LocalPlayer.ToString() == gameManager.Dutch)
        {
            SixNationsTradingButtonOnClick sixNationsTradingButtonOnClick = new SixNationsTradingButtonOnClick();
            sixNationsTradingButtonOnClick.SixNationsTradingOnClick();
        }

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

        gameManager.DutchTrading = true;
        gameManager.receivingTeam = "Dutch";
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
            Debug.Log("Dutch is calling the flag");

            gameManager.GetComponent<PhotonView>().RPC("FlagButtonBackgroundFadeInFadeOutEnd", RpcTarget.All);
        }
        greyOutButtons();
        setLabels();
        gameManager.CallReactivateTeamFlagsRPC();
        return;
            //}
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
        for(int ind = 0; ind < gameManager.givingTeamLabels.Length; ind++)
        {
            if(gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.givingTeam || gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.receivingTeam)
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
            string s = gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag.ToString() + " " + gameManager.givingTeam.ToString() + " " + gameManager.receivingTeam.ToString();
            string s2 = (gameManager.givingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.givingTeam).ToString();
            this.GetComponent<PhotonView>().RPC("Debugr", RpcTarget.All, ind, PhotonNetwork.LocalPlayer.ToString(), s, s2);
            if (gameManager.receivingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.givingTeam || gameManager.receivingTeamLabels[ind].transform.parent.parent.parent.tag == gameManager.receivingTeam)
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
    void Debugr(int ind, string userIDOfClicker, string message, string message2)
    {
        Debug.Log(message);
        Debug.Log("message 2: " + message2);
        Debug.Log("Sender is " + ((gameManager.Dutch == userIDOfClicker) ? "Dutch" : " ") + ((gameManager.Philipses == userIDOfClicker) ? "Philipses" : " ") + ((gameManager.SixNations == userIDOfClicker) ? "SixNations" : " ") + ((gameManager.Munsee == userIDOfClicker) ? "Munsee" : " "));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
