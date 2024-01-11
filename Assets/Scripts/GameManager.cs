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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allTeamsSelected)
        {
            //SceneManager.LoadScene(1);
        }
    }
}
