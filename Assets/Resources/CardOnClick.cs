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
            if(parentTag == "Wishlist" && gameManager.firstWishlistCardClicked == false)
            {
                gameManager.firstWishlistCardClicked = true;
            }
            else if (parentTag == "Inventory" && gameManager.firstInventoryCardClicked == false)
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
