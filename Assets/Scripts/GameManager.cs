using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (instance == null)
        {
            gameObject.AddComponent<PhotonView>();
            

            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);

        
    }

    public string DutchWestIndiaCompany;
    public string SixNations;
    public string Munsee;
    public string Philipses;

    public bool SixNationsJoined = false;
    public bool MunseeJoined = false;
    public bool DutchJoined = false;
    public bool PhilipsesJoined = false;


    public int userID = 1;
    public GameObject gameobject;

    void Start()
    {
        Debug.Log(DutchWestIndiaCompany);
    }


    void Update()
    {
        if (SixNationsJoined && DutchJoined && MunseeJoined && PhilipsesJoined)
        {
            Debug.Log("AAAA");
            SceneManager.LoadScene(1);
        }
    }
}
