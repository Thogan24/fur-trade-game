using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            if (CanWishlist)
            {
                gameManager.gameObject.GetComponent<PhotonView>().RPC("addCardToTrade", RpcTarget.All, tag, parentTag);
            } else
            {
                StartCoroutine(FlashRed());
            }
        }

    }

    public bool CanWishlist()
    {
        string player_ID = PhotonNetwork.LocalPlayer.ToString();
        if (player_ID == gameManager.Dutch)
        {
            int index = FindIndexOfTag(gameManager.tags, this.gameObject.tag);
            Debug.Log(gameManager.PhilipsesAmounts[index]);
        }
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
        private SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = new Color(1f, originalColor.g * 0.5f, originalColor.b * 0.5f, originalColor.a);

        yield return new WaitForSeconds(1);

        spriteRenderer.color = originalColor;
    }
}
