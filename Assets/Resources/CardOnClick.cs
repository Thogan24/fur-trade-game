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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click");
        }
            
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
            if (!gameManager.tutorialFinishedGameSetup)
            {
                string t = this.gameObject.tag;
                string pT = "Wishlist";
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
        if (!gameManager.tutorialFinishedGameSetup) {
            string tag = this.gameObject.tag;
            string parentTag = this.gameObject.transform.parent.tag;
            Debug.Log("Card tutorial clicked 1");
            gameManager.addCardToTradeTutorial(tag, parentTag, false);
            
           

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
