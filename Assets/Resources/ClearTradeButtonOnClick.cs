using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ClearTradeButtonOnClick : MonoBehaviour
{
    public GameManager gameManager;

    public void ClearTradeButtonOnClicked()
    {

        Debug.Log("Clear Trade Button Clicked");
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (gameManager.tutorialFinishedGameSetup)
        {
            if (gameManager.turn == 1 && PhotonNetwork.LocalPlayer.ToString() == gameManager.Dutch || gameManager.turn == 2 && PhotonNetwork.LocalPlayer.ToString() == gameManager.Philipses || gameManager.turn == 3 && PhotonNetwork.LocalPlayer.ToString() == gameManager.SixNations || gameManager.turn == 4 && PhotonNetwork.LocalPlayer.ToString() == gameManager.Munsee)
            {
                this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString());
            }
        }
        else
        {
            tutorialClickClearButton();
        }
        gameManager.gameObject.GetComponent<SoundEffectsPlayer>().playButtonSoundEffect();

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.clearTradeButton = true;
        gameManager.GetComponent<PhotonView>().RPC("clearAllTrades", RpcTarget.All);
    }

    void tutorialClickClearButton()
    {
        // Clear cards and stuff
        /*gameManager.clearTradeButton = true;
        gameManager.clearTradeTutorial();
*/
        return; // functionality disabled during tutorial
    }
}
