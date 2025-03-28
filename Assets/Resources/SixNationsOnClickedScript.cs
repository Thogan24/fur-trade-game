using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;
using TMPro;


public class SixNationsOnClickedScript : MonoBehaviour
{

    public GameObject mySixNationsButton;
    public bool teamJoined = false;
    void Start()
    {
        mySixNationsButton = this.gameObject;
    }

    public void SixNationsButtonClicked()
    {
        GameManager gameManager1 = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if(gameManager1.Dutch != PhotonNetwork.LocalPlayer.ToString() && gameManager1.Munsee != PhotonNetwork.LocalPlayer.ToString() && gameManager1.Philipses != PhotonNetwork.LocalPlayer.ToString() && gameManager1.SixNations != PhotonNetwork.LocalPlayer.ToString() && teamJoined == false)
        {
            gameManager1.gameObject.GetComponent<SoundEffectsPlayer>().playButtonSoundEffect();

            this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, this.transform.position, PhotonNetwork.LocalPlayer.ToString(), PhotonNetwork.LocalPlayer.NickName);
        }

        else if (teamJoined)
        {
            // Display "Team already joined by another player" text
        }
        

    }

    [PunRPC]
    void WhenClicked(Vector3 transform, string userIDOfClicker, string Nickname, PhotonMessageInfo info) // 
    {
        
        Debug.LogError("ismine: " + this.GetComponent<PhotonView>().IsMine + " viewid: " + this.GetComponent<PhotonView>().ViewID);
        Debug.LogError("User ID: " + userIDOfClicker + "Nickname: " + Nickname);

        GameObject SixNationsButton = GameObject.FindGameObjectWithTag("Six Nations Button");
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        gameManager.SixNations = userIDOfClicker;
        gameManager.SixNationsNickname = Nickname;
        SixNationsButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.3f);
        teamJoined = true;
        //PhotonNetwork.Destroy(SixNationsButton);

        // If it didn't get destroyed yet for any reason
        //PhotonNetwork.Destroy(mySixNationsButton);
        GameObject.FindGameObjectWithTag("SixNationsPlayerName").GetComponent<TextMeshProUGUI>().text = info.Sender.NickName;
        gameManager.moveSceneIfReadyCaller();
    }
}
