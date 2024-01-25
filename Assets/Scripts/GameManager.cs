using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
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
            //  gameObject.AddComponent<PhotonView>();


            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }

        
    }

    public string DutchWestIndiaCompany;
    public string SixNations;
    public string Munsee;
    public string Philipses;

    public bool SixNationsJoined = false;
    public bool MunseeJoined = false;
    public bool DutchJoined = false;
    public bool PhilipsesJoined = false;


    public int userID = 0;
    public int newUserID = 100;
    public GameObject gameobject;
    public int OldPlayerListLength;

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
        
        if (PhotonNetwork.PlayerList.Length > OldPlayerListLength)
        {
            Debug.LogError("User ID: " + userID + " New User ID: " + newUserID);
            //Debug.LogError("PlayerList: " + PhotonNetwork.PlayerList.Length + " | Old: " + OldPlayerListLength);
            this.GetComponent<PhotonView>().RPC("changeUserID", RpcTarget.All, transform.position);
            

        }
    }

    [PunRPC]
    void changeUserID(Vector3 transform)
    {
        
        Debug.LogError("ismine: " + this.GetComponent<PhotonView>().IsMine + " viewid: " + this.GetComponent<PhotonView>().ViewID);
        Debug.LogError("PlayerList: " + PhotonNetwork.PlayerList.Length + " | OldPlayerList: " + OldPlayerListLength);
        Debug.LogError("User ID: " + userID + " New User ID: " + newUserID);

        if (PhotonNetwork.PlayerList.Length != OldPlayerListLength)
        {
            userID = PhotonNetwork.PlayerList.Length;
            OldPlayerListLength = PhotonNetwork.PlayerList.Length;
        }

    }

    
}
