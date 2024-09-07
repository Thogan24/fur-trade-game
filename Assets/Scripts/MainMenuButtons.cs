using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
