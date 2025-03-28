using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject Instructions;
    public GameObject InstructionsGameObject;
    public GameObject WampumValueList;
    public GameObject WampumValueListGameObject;
    public GameObject InputFieldText;
    public GameObject SettingsScreenPrefab;
    public bool settingsOpened = false;
    public GameObject settingsScreen;
    public Slider volumeSlider; 
    public Slider musicVolumeSlider;
    public AudioSource musicAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseTutorial()
    {

    }

    public void SkipTutorial()
    {
        Debug.Log(this.name);
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.skipTutorial();
    }
    public void PauseGame()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (gameManager.tutorialFinishedGameSetup)
        {
            gameManager.GetComponent<PhotonView>().RPC("PauseGameRPC", RpcTarget.All);

        }

    }
    public void ValueChangeCheck()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.volume = volumeSlider.value;
        gameManager.GetComponent<AudioSource>().volume = volumeSlider.value;
        Debug.Log("setting game manager volume to: " + gameManager.volume);
        if(musicAudioSource == null)
        {
            musicAudioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        }
        musicAudioSource.volume = musicVolumeSlider.value;
        Debug.Log("setting music volume to: " + musicAudioSource.volume);



    }
    public void SettingsButtonOnClick()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        

        //Debug.Log("Opening settings. settingsOpened = " + settingsOpened + " " + this.transform.parent.parent.tag + " " + GameObject.FindGameObjectsWithTag("SettingsScreen").Length);
        GameObject[] settingsArray = (GameObject.FindGameObjectsWithTag("SettingsScreen"));
        if (settingsArray.Length > 0)
        {
            for(int i = 0; i < settingsArray.Length; i++)
            {
                Debug.Log(settingsArray[i].name + " " + settingsArray[i].transform.parent.parent.name);
            }
        }
        


        if (settingsOpened == false || GameObject.FindGameObjectsWithTag("SettingsScreen").Length == 0)
        {
            Debug.Log("Settings opened: " + PhotonNetwork.LocalPlayer.ToString());
            if(SceneManager.GetActiveScene().name == "TeamSelect")
            {
                settingsScreen = Instantiate(SettingsScreenPrefab, GameObject.FindGameObjectWithTag("theTeamSelectCanvas").transform);
                settingsScreen.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                volumeSlider = settingsScreen.transform.GetChild(1).GetComponent<Slider>();
                musicVolumeSlider = settingsScreen.transform.GetChild(2).GetComponent<Slider>();
                Destroy(settingsScreen.transform.GetChild(3).gameObject);
                Destroy(settingsScreen.transform.GetChild(0).gameObject);


            }
            if (PhotonNetwork.LocalPlayer.ToString() == gameManager.Dutch)
            {
                Debug.Log("Inside the Dutch instantiation");
                settingsScreen = Instantiate(SettingsScreenPrefab, gameManager.DutchInstructionsCanvasObject.transform);
                Debug.Log(settingsScreen.name + " " + settingsScreen.transform.parent);
                if(settingsScreen == null)
                {
                    Debug.Log("Had to go to the null case");
                    Instantiate(SettingsScreenPrefab, gameManager.DutchInstructionsCanvasObject.transform);
                }
                settingsScreen.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (gameManager.playersThatWantToEndTheGame > 0) ? "End Game " + gameManager.playersThatWantToEndTheGame + "/4": "End Game";
            }
            else if (PhotonNetwork.LocalPlayer.ToString() == gameManager.Philipses)
            {
                settingsScreen = Instantiate(SettingsScreenPrefab, gameManager.PhilipsesInstructionsCanvasObject.transform);
                settingsScreen.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (gameManager.playersThatWantToEndTheGame > 0) ? "End Game " + gameManager.playersThatWantToEndTheGame + "/4" : "End Game";

            }
            else if (PhotonNetwork.LocalPlayer.ToString() == gameManager.SixNations)
            {
                settingsScreen = Instantiate(SettingsScreenPrefab, gameManager.SixNationsInstructionsCanvasObject.transform);
                settingsScreen.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (gameManager.playersThatWantToEndTheGame > 0) ? "End Game " + gameManager.playersThatWantToEndTheGame + "/4" : "End Game";

            }
            else if (PhotonNetwork.LocalPlayer.ToString() == gameManager.Munsee)
            {
                settingsScreen = Instantiate(SettingsScreenPrefab, gameManager.MunseeInstructionsCanvasObject.transform);
                settingsScreen.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (gameManager.playersThatWantToEndTheGame > 0) ? "End Game " + gameManager.playersThatWantToEndTheGame + "/4" : "End Game";

            }
            if (SceneManager.GetActiveScene().name != "TeamSelect")
            {
                volumeSlider = settingsScreen.transform.GetChild(1).GetComponent<Slider>();            
                musicVolumeSlider = settingsScreen.transform.GetChild(2).GetComponent<Slider>();
            }
            volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            musicVolumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });


        }
        settingsOpened = true;
    }

    public void EndGameOnClick()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.GetComponent<PhotonView>().RPC("EndGameRPC", RpcTarget.All, PhotonNetwork.LocalPlayer.ToString());


        
    }



    public void ExitSettingsButton()
    {

        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (SceneManager.GetActiveScene().name != "TeamSelect")
        {
            gameManager.callEndGameCancelation(PhotonNetwork.LocalPlayer.ToString());
        }
        Debug.Log("Closing settings");
        settingsScreen = GameObject.FindGameObjectWithTag("SettingsScreen");
        settingsScreen.SetActive(false);
        settingsOpened = false;
/*        if (settingsScreen != null)
        {
            settingsScreen.SetActive(false);
        }
        else
        {
            settingsScreen = GameObject.FindGameObjectWithTag("SettingsScreen");
            settingsScreen.SetActive(false);
        }*/
    }



    public void ExitPauseGame()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.GetComponent<PhotonView>().RPC("ExitPauseGameRPC", RpcTarget.All);
    }
    public void StartGameOnClick()
    {
        GameObject.FindGameObjectWithTag("MainMenu").GetComponent<Canvas>().sortingOrder = -10;
        GameObject.FindGameObjectWithTag("MainMenu").SetActive(false);
        //SceneManager.LoadScene("RoomSelect");
    }

    public void NameCreatedOnClick()
    {
        GameObject.FindGameObjectWithTag("NameCanvas").GetComponent<Canvas>().sortingOrder = -10;
        PhotonNetwork.NickName = InputFieldText.GetComponent<TextMeshProUGUI>().text;
    }


    public void WampumValueListOnClick()
    {
        if(WampumValueListGameObject == null)
        {
            WampumValueListGameObject = Instantiate(WampumValueList, GameObject.FindGameObjectWithTag("TheWampumCanvas").transform);
        }
        
    }
    public void CloseWampumValueList()
    {
        Destroy(GameObject.FindGameObjectWithTag("WampumValueList"));
    }
    public void InstructionsOnClick()
    {
        if(SceneManager.GetActiveScene().name != "Main_Scene")
        {
            Debug.Log(this.transform.parent.name);
            Debug.Log(GameObject.FindGameObjectWithTag(this.transform.parent.name));
            InstructionsGameObject = Instantiate(Instructions, GameObject.FindGameObjectWithTag(this.transform.parent.name).transform);
        }
        else
        {
            // CHANGE!!!
            GameManager gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
            if (gameManager.tutorialFinishedGameSetup)
            {
                if (gameManager.Dutch == PhotonNetwork.LocalPlayer.ToString())
                {
                    InstructionsGameObject = Instantiate(Instructions, this.transform.parent.transform);
                }
                else if (gameManager.Philipses == PhotonNetwork.LocalPlayer.ToString())
                {
                    InstructionsGameObject = Instantiate(Instructions, GameObject.FindGameObjectWithTag("PhilipsesInstructionsCanvas").transform);
                }
                else if (gameManager.SixNations == PhotonNetwork.LocalPlayer.ToString())
                {
                    InstructionsGameObject = Instantiate(Instructions, GameObject.FindGameObjectWithTag("SixNationsInstructionsCanvas").transform);
                }
                else if (gameManager.Munsee == PhotonNetwork.LocalPlayer.ToString())
                {
                    InstructionsGameObject = Instantiate(Instructions, GameObject.FindGameObjectWithTag("MunseeInstructionsCanvas").transform);
                }
            }
        }
        
    }
    public void QuitOnClick()
    {
        Application.Quit();
    }
    public void CloseOutInstructionsOnClick()
    {
        if (GameObject.FindGameObjectWithTag("Instructions") != null)
        {
            //Destroy(GameObject.FindGameObjectWithTag("Instructions")); //GameObject.FindGameObjectWithTag("Instructions")
            GameObject[] instructionArray = GameObject.FindGameObjectsWithTag("Instructions");
            if (instructionArray != null)
            {
                foreach (GameObject instruction in instructionArray)
                {
                    Destroy(instruction);
                }
            }
        }
        if (SceneManager.GetActiveScene().name != "Main_Scene")
        {
            GameObject[] descriptionArray = GameObject.FindGameObjectsWithTag("DutchDescription");
            if (descriptionArray != null)
            {
                foreach (GameObject description1 in descriptionArray)
                {
                    Destroy(description1);
                }
            }
            GameObject description = GameObject.FindGameObjectWithTag("PhilipsesDescription");
            if (description != null)
            {
                Destroy(description);
            }
            descriptionArray = GameObject.FindGameObjectsWithTag("SixNationsDescription");
            if (descriptionArray != null)
            {
                foreach (GameObject description1 in descriptionArray)
                {
                    Destroy(description1);
                }
            }
            description = GameObject.FindGameObjectWithTag("MunseeDescription");
            if (description != null)
            {
                Destroy(description);
            }
            descriptionArray = GameObject.FindGameObjectsWithTag("black");
            if (descriptionArray != null)
            {
                foreach (GameObject description1 in descriptionArray)
                {
                    Destroy(description1);
                }
            }
        }

        else
        {
            //string[] tagArray = { "BeaverInfo", "DeerSkinInfo", "BearInfo", "FisherInfo", "FoxInfo", "SchepelsInfo", "DuffelsInfo", "LinenInfo", "StockingsInfo", "StroudsInfo", "AxesInfo", "BeadsInfo", "ScissorsInfo" };
            //string[] imageDescriptionTags = { "BeaverDescription", "DeerSkinDescription", "BearDescription", "FisherDescription", "FoxDescription", "SchepelsDescription", "DuffelsDescription", "LinenDescription", "StockingsDescription", "StroudsDescription", "AxesDescription", "BeadsDescription", "ScissorsDescription" };
            InformationButtons informationButtons = new InformationButtons();
            informationButtons.closeInfo();

        }
        
        


    }
}
