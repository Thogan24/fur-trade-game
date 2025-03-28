using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;
using TMPro;


public class MunseeOnClickedScript : MonoBehaviour
{

    public GameObject myMunseeButton;
    public bool teamJoined = false;
    void Start()
    {
        myMunseeButton = this.gameObject;
    }
    public void MunseeButtonClicked()
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
        GameObject MunseeButton = GameObject.FindGameObjectWithTag("Munsee Button");
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        MunseeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 0.3f);
        gameManager.Munsee = userIDOfClicker;
        gameManager.MunseeNickname = Nickname;

        teamJoined = true;
        GameObject.FindGameObjectWithTag("MunseePlayerName").GetComponent<TextMeshProUGUI>().text = info.Sender.NickName;

        gameManager.moveSceneIfReadyCaller();
    }
}
