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

    public void TradeButtonOnClicked()
    {

        Debug.Log("Trade Button Clicked");
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (gameManager.CannotAccessTradeButton)
        {
            Debug.Log("You cannot access the trades");
            return;
        }
        if (gameManager.tutorialFinishedGameSetup == false)
        {
            Debug.Log("tutorialClickFinishButton");
            tutorialClickFinishButton();
            return;
        }


        gameManager.gameObject.GetComponent<SoundEffectsPlayer>().playButtonSoundEffect();

        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString());

    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker, PhotonMessageInfo info) // 
    {
        /*if (this.gameObject != null)
        {
            Debug.Log(this.gameObject.name + this.gameObject.tag + this.gameObject.transform.parent.name + this.gameObject.transform.parent.parent.name + this.gameObject.transform.parent.parent.parent.name);
        }*/


        
        if (this.gameObject.tag == "clearButton")
        {
            Debug.Log("W");
            gameManager.clearTradeButton = true;
            gameManager.GetComponent<PhotonView>().RPC("clearAllTrades", RpcTarget.All);
        }



        Debug.Log(userIDOfClicker);
        Debug.Log(PhotonNetwork.LocalPlayer.ToString());
        Debug.Log(info.Sender.ToString());
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        string team = gameManager.findPlayerTeam(info.Sender.ToString()); // userIdOfClicker

        Debug.LogError("Team Selected: " + team);
        if (team == "Dutch")
        {
            Debug.LogError("Dutch Accepted");
            if (gameManager.DutchTrading)
            {
                if (gameManager.DutchAccepted == false)
                {
                    if (gameManager.numberOfAcceptedTeams < 1 && PhotonNetwork.LocalPlayer.IsMasterClient)
                        gameManager.gameObject.GetComponent<PhotonView>().RPC("AcceptButtonBackgroundFadeInFadeOut", RpcTarget.All);
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
                    
                }
            }
            else
            {
                GameObject[] Alert = gameManager.Alert;
                for (int harold = 0; harold < Alert.Length; harold++)
                {
                    Alert[harold].GetComponent<TMPro.TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
                    Alert[harold].GetComponent<TMPro.TMP_Text>().text = "You are not trading!";
                    gameManager.StartCoroutine(gameManager.FadeTextToZeroAlpha(1f, Alert[harold].GetComponent<TMPro.TMP_Text>()));
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
                    if (gameManager.numberOfAcceptedTeams < 1 && PhotonNetwork.LocalPlayer.IsMasterClient)
                        gameManager.gameObject.GetComponent<PhotonView>().RPC("AcceptButtonBackgroundFadeInFadeOut", RpcTarget.All);
                    gameManager.numberOfAcceptedTeams++;
                    

                }
                gameManager.PhilipsesAccepted = true;

                if (gameManager.numberOfAcceptedTeams == 2)
                {
                    Debug.LogError("Cards being switched, calling RPC");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
            }
            else
            {
                GameObject[] Alert = gameManager.Alert;
                for (int harold = 0; harold < Alert.Length; harold++)
                {
                    Alert[harold].GetComponent<TMPro.TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
                    Alert[harold].GetComponent<TMPro.TMP_Text>().text = "You are not trading!";
                    gameManager.StartCoroutine(gameManager.FadeTextToZeroAlpha(1f, Alert[harold].GetComponent<TMPro.TMP_Text>()));
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
                    if (gameManager.numberOfAcceptedTeams < 1 && PhotonNetwork.LocalPlayer.IsMasterClient)
                        gameManager.gameObject.GetComponent<PhotonView>().RPC("AcceptButtonBackgroundFadeInFadeOut", RpcTarget.All);
                    gameManager.numberOfAcceptedTeams++;
                    

                }
                gameManager.SixNationsAccepted = true;

                if (gameManager.numberOfAcceptedTeams == 2)
                {
                    Debug.LogError("Cards being switched, calling RPC");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
            }
            else
            {
                GameObject[] Alert = gameManager.Alert;
                for (int harold = 0; harold < Alert.Length; harold++)
                {
                    Alert[harold].GetComponent<TMPro.TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
                    Alert[harold].GetComponent<TMPro.TMP_Text>().text = "You are not trading!";
                    gameManager.StartCoroutine(gameManager.FadeTextToZeroAlpha(1f, Alert[harold].GetComponent<TMPro.TMP_Text>()));
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
                    if (gameManager.numberOfAcceptedTeams < 1 && PhotonNetwork.LocalPlayer.IsMasterClient)
                        gameManager.gameObject.GetComponent<PhotonView>().RPC("AcceptButtonBackgroundFadeInFadeOut", RpcTarget.All);
                    gameManager.numberOfAcceptedTeams++;


                }
                gameManager.MunseeAccepted = true;

                if (gameManager.numberOfAcceptedTeams == 2)
                {
                    Debug.LogError("Cards being switched, calling RPC");
                    gameManager.gameObject.GetComponent<PhotonView>().RPC("cardSwitchTeams", RpcTarget.All);
                }
            }
            else
            {
                GameObject[] Alert = gameManager.Alert;
                for (int harold = 0; harold < Alert.Length; harold++)
                {
                    Alert[harold].GetComponent<TMPro.TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
                    Alert[harold].GetComponent<TMPro.TMP_Text>().text = "You are not trading!";
                    gameManager.StartCoroutine(gameManager.FadeTextToZeroAlpha(1f, Alert[harold].GetComponent<TMPro.TMP_Text>()));
                }

            }
        }
    }

    void tutorialClickFinishButton()
    {
        //Flash in and out for a few seconds.
        Debug.Log("starting the coroutine");
        StartCoroutine("flashInFlashOutForAFewSeconds");
        //Stop flash
        // Clear cards and stuff



    }
    IEnumerator flashInFlashOutForAFewSeconds()
    {
        Debug.Log("turning on the flash");
        gameManager.startAcceptButtonBackgroundFadeInFadeOutTutorial();
        yield return new WaitForSeconds(5);


        if (gameManager.inst != null)
        {
            Debug.Log("Inst is NOT equal to null");
            gameManager.StopCoroutine(gameManager.inst); // OK?
        }
        Debug.Log("turning it off");
        gameManager.nextTurnChangeColorToNothing = true;
        Image imaginary = GameObject.FindGameObjectWithTag("AcceptButtonBackground").GetComponent<Image>();
        imaginary.color = new Color(imaginary.color.r, imaginary.color.g, imaginary.color.b, 0);



        /*for (int iterator = 0; iterator < gameManager.tutorialEndButtons.Length; iterator++)
        {
            Debug.Log("running the tutorial end button setActives");
            if (this.gameObject.transform.parent.parent.tag == gameManager.tutorialEndButtons[iterator].transform.parent.parent.parent.parent.tag)
            {
                gameManager.tutorialEndButtons[iterator].SetActive(true);
            }
        }*/

        gameManager.clearTradeTutorial();
        
    }

}
