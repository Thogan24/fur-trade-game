using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;
using TMPro;

public class DutchOnClickedScript : MonoBehaviour
{

    public GameObject myDutchButton;
    public bool teamJoined = false;
    void Start()
    {
        myDutchButton = this.gameObject;
    }

    public void DutchButtonClicked()
    {
        GameManager gameManager1 = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (gameManager1.Dutch != PhotonNetwork.LocalPlayer.ToString() && gameManager1.Munsee != PhotonNetwork.LocalPlayer.ToString() && gameManager1.Philipses != PhotonNetwork.LocalPlayer.ToString() && gameManager1.SixNations != PhotonNetwork.LocalPlayer.ToString() && teamJoined == false)
        {
            gameManager1.gameObject.GetComponent<SoundEffectsPlayer>().playButtonSoundEffect();
            this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, this.transform.position, PhotonNetwork.LocalPlayer.ToString(), PhotonNetwork.LocalPlayer.NickName); //  After being mapped
        }
    }
    [PunRPC]
    void WhenClicked(Vector3 transform, string userIDOfClicker, string Nickname, PhotonMessageInfo info) // 
    {

        Debug.LogError("ismine: " + this.GetComponent<PhotonView>().IsMine + " viewid: " + this.GetComponent<PhotonView>().ViewID);
        Debug.LogError("User ID: " + userIDOfClicker);
        GameObject DutchButton = GameObject.FindGameObjectWithTag("Dutch Button");
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        DutchButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.3f);
        gameManager.Dutch = userIDOfClicker;
        gameManager.DutchNickname = Nickname;

        teamJoined = true;
        GameObject.FindGameObjectWithTag("DutchPlayerName").GetComponent<TextMeshProUGUI>().text = info.Sender.NickName;

        gameManager.moveSceneIfReadyCaller();
    }
}
