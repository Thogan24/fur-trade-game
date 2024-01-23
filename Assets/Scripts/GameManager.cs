using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    bool allTeamsSelected = true;

    public string DutchWestIndiaCompany;
    public string SixNations;
    public string Munsee;
    public string Philipses;

    public bool SixNationsJoined = false;
    public bool MunseeJoined = false;
    public bool DutchJoined = false;
    public bool PhilipsesJoined = false;

    public Text debug;
    public int userID = 1;
    public GameObject gameobject;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(DutchWestIndiaCompany);
    }

    // Update is called once per frame
    void Update()
    {
        //gameobject = GameObject.FindWithTag("thedebugger");
        //debug = gameobject.GetComponent<Text>();
        //debug.text = DutchWestIndiaCompany + " | " + SixNations;
        if (SixNationsJoined && DutchJoined && MunseeJoined && PhilipsesJoined)
        {
            Debug.Log("AAAA");
            SceneManager.LoadScene(1);
        }
    }
}
