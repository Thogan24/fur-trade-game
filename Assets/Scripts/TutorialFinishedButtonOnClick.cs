using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class TutorialFinishedButtonOnClick : MonoBehaviour
{
    public GameManager gameManager;

    public void TutorialFinishedOnClick()
    {
        

        this.GetComponent<PhotonView>().RPC("WhenClicked", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString());
        changeText(PhotonNetwork.LocalPlayer.ToString());
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.gameObject.GetComponent<SoundEffectsPlayer>().playButtonSoundEffect();
        this.gameObject.SetActive(false);
    }

    [PunRPC]
    void WhenClicked(string userIDOfClicker) // 
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (userIDOfClicker == gameManager.Dutch)
        {
            gameManager.tutorialFinishedDutch = true;


        }
        else if (userIDOfClicker == gameManager.Philipses)
        {
            gameManager.tutorialFinishedPhilipses = true;

        }
        else if (userIDOfClicker == gameManager.SixNations)
        {
            gameManager.tutorialFinishedSixNations = true;

        }
        else if (userIDOfClicker == gameManager.Munsee)
        {
            gameManager.tutorialFinishedMunsee = true;

        }
        else
        {
            Debug.Log("User doesn't exist");
        }
    }

    void changeText(string userIDOfClicker)
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (userIDOfClicker == gameManager.Dutch)
        {
            TMPro.TextMeshProUGUI TutorialAlertText = GameObject.FindGameObjectWithTag("TutorialAlert").GetComponent<TMPro.TextMeshProUGUI>();
            if (!(gameManager.tutorialFinishedPhilipses == true && gameManager.tutorialFinishedDutch == true && gameManager.tutorialFinishedSixNations && gameManager.tutorialFinishedMunsee))
            {
                TutorialAlertText.text = "You have completed the tutorial. Please wait for competitors to finish.";
            }

        }
        else if (userIDOfClicker == gameManager.Philipses)
        {
            TMPro.TextMeshProUGUI TutorialAlertText = GameObject.FindGameObjectWithTag("TutorialAlert").GetComponent<TMPro.TextMeshProUGUI>();

            if (!(gameManager.tutorialFinishedPhilipses == true && gameManager.tutorialFinishedDutch == true && gameManager.tutorialFinishedSixNations && gameManager.tutorialFinishedMunsee))
            {
                TutorialAlertText.text = "You have completed the tutorial. Please wait for competitors to finish.";
            }
        }
        else if (userIDOfClicker == gameManager.SixNations)
        {
            TMPro.TextMeshProUGUI TutorialAlertText = GameObject.FindGameObjectWithTag("TutorialAlert").GetComponent<TMPro.TextMeshProUGUI>();

            if (!(gameManager.tutorialFinishedPhilipses == true && gameManager.tutorialFinishedDutch == true && gameManager.tutorialFinishedSixNations && gameManager.tutorialFinishedMunsee))
            {
                TutorialAlertText.text = "You have completed the tutorial. Please wait for competitors to finish.";
            }
        }
        else if (userIDOfClicker == gameManager.Munsee)
        {
            TMPro.TextMeshProUGUI TutorialAlertText = GameObject.FindGameObjectWithTag("TutorialAlert").GetComponent<TMPro.TextMeshProUGUI>();

            if (!(gameManager.tutorialFinishedPhilipses == true && gameManager.tutorialFinishedDutch == true && gameManager.tutorialFinishedSixNations && gameManager.tutorialFinishedMunsee))
            {
                TutorialAlertText.text = "You have completed the tutorial. Please wait for competitors to finish.";
            }
        }
        else
        {
            Debug.Log("User doesn't exist");
        }
    }
}