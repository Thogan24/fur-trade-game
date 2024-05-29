using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InformationButtons : MonoBehaviour
{
    public GameObject imageObject;
    public Sprite image;
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

    
    public string[] tagArray = { "DutchInfo", "PhilipsesInfo", "SixNationsInfo", "MunseeInfo" };
    public string[] imageDescriptionTags = { "DutchDescription", "PhilipsesDescription", "SixNationsDescription", "MunseeDescription", };
    public int index;

    public void infoOnClicked()
    {

        Debug.Log("Clicked");
        var newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        for (int i = 0; i < tagArray.Length; i++)
        {

            if (tagArray[i] == this.gameObject.tag)
            {

                index = i;
                Debug.Log(index);
            }
        }
        if(SceneManager.GetActiveScene().name == "TeamSelect")
        {
            GameObject.FindGameObjectWithTag(imageDescriptionTags[index]).gameObject.GetComponent<Image>().color = newColor;
        }
        else if (SceneManager.GetActiveScene().name == "Main_Scene")
        {
            GameObject[] descriptionArray = GameObject.FindGameObjectsWithTag(imageDescriptionTags[index]);
            if(descriptionArray != null)
            {
                for (int a = 0; a < descriptionArray.Length; a++)
                {
                    descriptionArray[a].gameObject.GetComponent<Image>().color = newColor;
                }
            }
        }
        

    }
















/*    public void infoOnClicked()
    {
        
        Debug.Log("Clicked");
        Debug.Log("tag is: " + gameObject.tag);

        var newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        imageObject.GetComponent<Image>().color = newColor;
        //imageObject.gameObject.SetActive(true);
        Debug.Log(gameObject.tag);
        for (int i = 0; i < tagArray.Length; i++)
        {
            
            if(tagArray[i] == this.gameObject.tag)
            {

                index = i;
                Debug.Log(index);
            }
        }
        Debug.Log(Resources.Load<Sprite>("SixNationsDescription"));
        image = Resources.Load<Sprite>(imageDescriptionPathArray[index]);
        imageObject.GetComponent<Image>().sprite = image;
        
    }*/

    public void closeInfo()
    {
        var newColor = new Color(1.0f, 1.0f, 1.0f, 0f);

        for(int j = 0; j < imageDescriptionTags.Length; j++)
        {
            if (SceneManager.GetActiveScene().name == "TeamSelect")
            {
                if (GameObject.FindGameObjectWithTag(imageDescriptionTags[j]) != null)
                {
                    GameObject.FindGameObjectWithTag(imageDescriptionTags[j]).GetComponent<Image>().color = newColor;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Main_Scene")
            {
                GameObject[] descriptionArray2 = GameObject.FindGameObjectsWithTag(imageDescriptionTags[j]);
                for (int b = 0; b < descriptionArray2.Length; b++)
                {
                    if(descriptionArray2[b] != null)
                    {
                        descriptionArray2[b].gameObject.GetComponent<Image>().color = newColor;
                    }
                }
                
            }
    }

}
