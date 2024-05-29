using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class CardOnClick : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardOnClicked()
    {
        Debug.LogError("Card Clicked");
        // REMEMBER THIS IS HERE
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if(!gameManager.DutchTrading && !gameManager.PhilipsesTrading && !gameManager.SixNationsTrading && !gameManager.MunseeTrading)
        {
            Debug.LogError("Bruh no ones trading");
        }
        else
        {
            string tag = this.gameObject.tag;
            string parentTag = this.gameObject.transform.parent.tag;
            Debug.Log(tag);
            if (CanWishlist())
            {
                gameManager.gameObject.GetComponent<PhotonView>().RPC("addCardToTrade", RpcTarget.All, tag, parentTag);
            } else
            {
                StartCoroutine(FlashRedCoroutine());
            }
        }

    }

    public bool CanWishlist()
    {
        //if (player_ID == gameManager.Dutch && gameManager.PhilipsesTrading)
        //{
        //    int index = FindIndexOfTag(gameManager.tags, this.gameObject.tag);
        //    if (gameManager.PhilipsesAmounts[index] == 0)
        //    {
        //        return false;
        //    }
        //}
        //if (player_ID == gameManager.Dutch && gameManager.MunseeTrading)
        //{
        //    int index = FindIndexOfTag(gameManager.tags, this.gameObject.tag);
        //    if (gameManager.MunseeAmounts[index] == 0)
        //    {
        //        return false;
        //    }
        //if (player_ID == gameManager.Dutch && gameManager.SixNationsTrading)
        //{
        //    int index = FindIndexOfTag(gameManager.tags, this.gameObject.tag);
        //    if (gameManager.SixNationsAmounts[index] == 0)
        //    {
        //        return false;
        //    }
        //if (player_ID == gameManager.Phillipses && gameManager.MunseeTrading)
        //{
        //    int index = FindIndexOfTag(gameManager.tags, this.gameObject.tag);
        //    if (gameManager.MunseeAmounts[index] == 0)
        //    {
        //        return false;
        //    }
        //if (player_ID == gameManager.Phillipses && gameManager.SixNationsTrading)
        //{
        //    int index = FindIndexOfTag(gameManager.tags, this.gameObject.tag);
        //    if (gameManager.SixNationsAmounts[index] == 0)
        //    {
        //        return false;
        //    }
        //}
        return true;
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
    private IEnumerator FlashRedCoroutine()
    {
        yield return null;
    }
}
