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

    public bool IroquoisJoined = false;
    public bool MunseeJoined = false;
    public bool DutchJoined = false;
    public bool PhilipsesJoined = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(DutchWestIndiaCompany);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IroquoisJoined && MunseeJoined && DutchJoined && PhilipsesJoined)
        {
            SceneManager.LoadScene(1);
        }
    }
}
