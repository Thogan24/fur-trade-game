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
        Destroy(GameObject.FindGameObjectWithTag("Instructions")); //GameObject.FindGameObjectWithTag("Instructions")
    }
}
