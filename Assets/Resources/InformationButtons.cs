using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        GameObject.FindGameObjectWithTag(imageDescriptionTags[index]).gameObject.GetComponent<Image>().color = newColor;

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
            if(GameObject.FindGameObjectWithTag(imageDescriptionTags[j]) != null)
            {
                GameObject.FindGameObjectWithTag(imageDescriptionTags[j]).GetComponent<Image>().color = newColor;
            }
        }
    }

}
