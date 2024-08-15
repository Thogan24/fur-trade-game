using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject Instructions;
    public GameObject InstructionsGameObject;
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
        SceneManager.LoadScene("RoomSelect");
    }

    public void InstructionsOnClick()
    {
        InstructionsGameObject = Instantiate(Instructions, GameObject.FindGameObjectWithTag("CreateOrJoinRoomCanvas").transform);
    }
    public void CloseOutInstructionsOnClick()
    {
        if(GameObject.FindGameObjectWithTag("Instructions") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Instructions")); //GameObject.FindGameObjectWithTag("Instructions")
        }
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
            foreach(GameObject description1 in descriptionArray)
            {
                Destroy(description1);
            }
        }


    }
}
