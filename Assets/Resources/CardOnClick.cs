using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;


public class CardOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameManager gameManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (gameManager.CannotAccessCards)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click");
        }
            
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            if (!gameManager.tutorialFinishedGameSetup)
            {
                string t = this.gameObject.tag;
                string pT = "Wishlist"; 
                if (gameManager.firstWishlistCardClicked == false)
                {
                    gameManager.firstWishlistCardClicked = true;
                }
                gameManager.addCardToTradeTutorial(t, pT, true);

                if (gameManager.firstWishlistCardClicked && gameManager.firstInventoryCardClicked)
                {
                    gameManager.startContinueTutorial2();
                }
            }
            if (!gameManager.DutchTrading && !gameManager.PhilipsesTrading && !gameManager.SixNationsTrading && !gameManager.MunseeTrading)
            {
                Debug.LogError("Bruh no ones trading");
            }
            
            else
            {
                string t = this.gameObject.tag;
                string pT = "Wishlist";
                gameManager.gameObject.GetComponent<PhotonView>().RPC("addCardToTrade", RpcTarget.All, t, pT, true);
            }
        }
            
    }

    public void CardOnClicked()
    {
        Debug.LogError("Card Clicked");
        // REMEMBER THIS IS HERE
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        if (gameManager.CannotAccessCards)
        {
            return;
        }

        if (!gameManager.tutorialFinishedGameSetup) {
            string tag = this.gameObject.tag;
            string parentTag = this.gameObject.transform.parent.tag;
            Debug.Log("Card tutorial clicked 1");

            if (parentTag == "Inventory" && gameManager.firstInventoryCardClicked == false)
            {
                gameManager.firstInventoryCardClicked = true;
            }
            gameManager.addCardToTradeTutorial(tag, parentTag, false);
            if (gameManager.firstWishlistCardClicked && gameManager.firstInventoryCardClicked)
            {
                gameManager.startContinueTutorial2();
            }
        }



        if (!gameManager.DutchTrading && !gameManager.PhilipsesTrading && !gameManager.SixNationsTrading && !gameManager.MunseeTrading)
        {
            Debug.LogError("Bruh no ones trading");

            GameObject[] Alert = gameManager.Alert;
            string playerString = PhotonNetwork.LocalPlayer.ToString();
            for (int harold = 0; harold < Alert.Length; harold++)
            {
                if (gameManager.tutorialFinishedGameSetup && ((gameManager.turn == 1 && playerString == gameManager.Dutch) || (gameManager.turn == 2 && playerString == gameManager.Philipses) || (gameManager.turn == 3 && playerString == gameManager.SixNations) || (gameManager.turn == 4 && playerString == gameManager.Munsee)))
                {
                    Alert[harold].GetComponent<TMPro.TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
                    Debug.Log("How did I get in here: " + gameManager.tutorialFinishedGameSetup);
                    Alert[harold].GetComponent<TMPro.TMP_Text>().text = "Select a team first!";
                    gameManager.StartCoroutine(gameManager.FadeTextToZeroAlpha(1f, Alert[harold].GetComponent<TMPro.TMP_Text>()));

                }
                else if (gameManager.tutorialFinishedGameSetup)
                {
                    Alert[harold].GetComponent<TMPro.TMP_Text>().color = new Color(1f, 1f, 1f, 1f);
                    Debug.Log("How did I get in here2: " + gameManager.tutorialFinishedGameSetup);
                    Alert[harold].GetComponent<TMPro.TMP_Text>().text = "It is not your turn to trade!";
                    gameManager.StartCoroutine(gameManager.FadeTextToZeroAlpha(1f, Alert[harold].GetComponent<TMPro.TMP_Text>()));

                }

            }
        }
        else
        {
            string tag = this.gameObject.tag;
            string parentTag = this.gameObject.transform.parent.tag;
            Debug.Log(tag);
            gameManager.gameObject.GetComponent<PhotonView>().RPC("addCardToTrade", RpcTarget.All, tag, parentTag, false);
            
        }

    }


    public int FindIndexOfTag(string[] arr, string str)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == str)
            {
                return i;
            }
        }
        return -1;
    }
}
