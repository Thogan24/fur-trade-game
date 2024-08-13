using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InformationButtons : MonoBehaviour
{
    public GameObject imageObject;
    public Sprite image;
    public bool sceneChange = false;
    public GameManager gameManager;
    public void Start()
    {
        /*imageObject = GameObject.FindGameObjectWithTag("DescriptionImage");
        var newColor = new Color(1.0f, 1.0f, 1.0f, 0f);
        imageObject.GetComponent<Image>().color = newColor;


        image = Resources.Load<Sprite>("SixNationsDescription");*/
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeInfo();
        }

    }
    public void SceneChange()
    {
        imageObject = GameObject.FindGameObjectWithTag("DescriptionImage");
        var newColor = new Color(1.0f, 1.0f, 1.0f, 0f);
        imageObject.GetComponent<Image>().color = newColor;
    }


    public string[] tagArray = { "DutchInfo", "PhilipsesInfo", "SixNationsInfo", "MunseeInfo", "BeaverInfo", "DeerSkinInfo", "BearInfo", "FisherInfo", "FoxInfo", "SchepelsInfo", "DuffelsInfo", "LinenInfo", "StockingsInfo", "StroudsInfo", "AxesInfo", "BeadsInfo", "ScissorsInfo" };
    public string[] imageDescriptionTags = { "DutchDescription", "PhilipsesDescription", "SixNationsDescription", "MunseeDescription", "BeaverDescription", "DeerSkinDescription", "BearDescription", "FisherDescription", "FoxDescription", "SchepelsDescription", "DuffelsDescription", "LinenDescription", "StockingsDescription", "StroudsDescription", "AxesDescription", "BeadsDescription", "ScissorsDescription" };
    public int index;

    public void infoOnClicked()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        Debug.Log("Clicked");
        Debug.Log("Running through the array, searching for index with tag: " + this.gameObject.tag);
        var newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        for (int i = 0; i < tagArray.Length; i++)
        {

            if (tagArray[i] == this.gameObject.tag)
            {

                index = i;
                Debug.Log(index);
            }
        }
        if (SceneManager.GetActiveScene().name == "TeamSelect")
        {
            GameObject.FindGameObjectWithTag(imageDescriptionTags[index]).gameObject.GetComponent<Image>().color = newColor;
            GameObject.FindGameObjectWithTag("black").gameObject.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (SceneManager.GetActiveScene().name == "Main_Scene" && gameManager.opened == false)
        {
            GameObject[] descriptionArray = GameObject.FindGameObjectsWithTag(imageDescriptionTags[index]);
            if (descriptionArray != null)
            {
                for (int a = 0; a < descriptionArray.Length; a++)
                {
                    gameManager.opened = true;
                    Debug.Log(gameManager.opened);
                    descriptionArray[a].gameObject.GetComponent<Image>().color = newColor;
                    descriptionArray[a].gameObject.transform.position = descriptionArray[a].gameObject.transform.position + new Vector3(100, 0, 0);
                    
                }
            }
        }


    }

    public void closeInfo()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        var newColor = new Color(1.0f, 1.0f, 1.0f, 0f);

        for (int j = 0; j < imageDescriptionTags.Length; j++)
        {
            if (SceneManager.GetActiveScene().name == "TeamSelect")
            {
                if (GameObject.FindGameObjectWithTag(imageDescriptionTags[j]) != null)
                {
                    GameObject.FindGameObjectWithTag(imageDescriptionTags[j]).GetComponent<Image>().color = newColor;
                    GameObject.FindGameObjectWithTag("black").gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0f);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Main_Scene" && gameManager.opened == true)
            {
                Debug.Log("I'm still running");
                
                GameObject[] descriptionArray2 = GameObject.FindGameObjectsWithTag(imageDescriptionTags[j]);
                for (int b = 0; b < descriptionArray2.Length; b++)
                {
                    if (descriptionArray2[b] != null && descriptionArray2[b].gameObject.GetComponent<Image>().color != newColor)
                    {
                        Debug.Log("Got here");
                        descriptionArray2[b].gameObject.GetComponent<Image>().color = newColor;
                        descriptionArray2[b].gameObject.transform.position = descriptionArray2[b].gameObject.transform.position - new Vector3(100, 0, 0);
                    }
                }

            }
        }
        gameManager.opened = false;

    }
}
