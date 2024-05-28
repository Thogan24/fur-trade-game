using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButtons : MonoBehaviour
{
    public GameObject imageObject;
    public void Start()
    {
        imageObject = GameObject.FindGameObjectWithTag("DescriptionImage");
        var newColor = new Color(1.0f, 1.0f, 1.0f, 0f);
        imageObject.GetComponent<Image>().color = newColor;
    }
    public void SceneChange()
    {
        imageObject = GameObject.FindGameObjectWithTag("DescriptionImage");
        var newColor = new Color(1.0f, 1.0f, 1.0f, 0f);
        imageObject.GetComponent<Image>().color = newColor;
    }

    
    public string[] tagArray = { "DutchInfo", "PhilipsesInfo", "SixNationsInfo", "MunseeInfo" };
    public string[] imageDescriptionPathArray = { "SixNationsDescription" };
    public int index;
    public void infoOnClicked()
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
        imageObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageDescriptionPathArray[index]) as Sprite;
        
    }

    public void closeInfo()
    {
        var newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        imageObject.GetComponent<Image>().color = newColor;
    }

}
