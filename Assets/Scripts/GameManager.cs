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

    public string Dutch;
    public string SixNations;
    public string Munsee;
    public string Philipses;

    public bool SixNationsJoined = false;
    public bool MunseeJoined = false;
    public bool DutchJoined = false;
    public bool PhilipsesJoined = false;

    public PhotonView DutchCamera;
    public GameObject SixNationsCamera;
    public GameObject MunseeCamera;
    public GameObject PhilipsesCamera;


    public GameObject DutchTextCanvasObject;
    public GameObject DutchBackgroundCanvasObject;
    public GameObject DutchCardsCanvasObject;


    public int userID = 0;
    public int newUserID = 100;
    //public GameObject gameobject;
    public int OldPlayerListLength;
    public bool AlreadyLoaded = false;

    void Start()
    {
        Debug.Log(Dutch);
    }


    void Update()
    {
        if (SixNationsJoined && DutchJoined && !AlreadyLoaded) //  && MunseeJoined && PhilipsesJoined
        {
            Debug.Log("AAAA");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
            
            //DutchCamera = GameObject.FindGameObjectWithTag("DWIC Camera");
            this.GetComponent<PhotonView>().RPC("mainSceneCameraRPC", RpcTarget.All, transform.position);
            DutchTextCanvasObject = GameObject.FindGameObjectWithTag("Dutch Text Canvas");
            DutchBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Dutch Background Canvas");
            DutchCardsCanvasObject = GameObject.FindGameObjectWithTag("Dutch Card Canvas");
            DutchTextCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            DutchBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            DutchCardsCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            AlreadyLoaded = true;
        }

            //Debug.Log(Philipses);



        if (Philipses != "" && PhilipsesJoined == false)
        {
            this.GetComponent<PhotonView>().RPC("philipsesJoinedRPC", RpcTarget.All, Philipses);
        }
        if (Dutch != "" && DutchJoined == false)
        {
            this.GetComponent<PhotonView>().RPC("dutchJoinedRPC", RpcTarget.All, Dutch);
        }
        if (SixNations != "" && SixNationsJoined == false)
        {
            this.GetComponent<PhotonView>().RPC("sixNationsJoinedRPC", RpcTarget.All, SixNations);
        }
        if (Munsee != "" && MunseeJoined == false)
        {
            this.GetComponent<PhotonView>().RPC("munseeJoinedRPC", RpcTarget.All, Munsee);
        }







        if (PhotonNetwork.PlayerList.Length > OldPlayerListLength)
        {
            Debug.LogError("User ID: " + userID + " New User ID: " + newUserID);
            //Debug.LogError("PlayerList: " + PhotonNetwork.PlayerList.Length + " | Old: " + OldPlayerListLength);
            this.GetComponent<PhotonView>().RPC("changeUserID", RpcTarget.All, transform.position);
            

        }
    }


    // TEAM SELECT STUFF
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
    [PunRPC]
    void philipsesJoinedRPC(string userIDPhilipses)
    {
        PhilipsesJoined = true;
        Philipses = userIDPhilipses;
        Debug.LogError(PhilipsesJoined);
        Debug.LogError(Philipses);
        // Mapping shit
    }

    [PunRPC]
    void dutchJoinedRPC(string userIDDutch)
    {
        DutchJoined = true;
        Dutch = userIDDutch;
        Debug.LogError(DutchJoined);
        Debug.LogError(Dutch);
        // Mapping shit
    }

    [PunRPC]
    void sixNationsJoinedRPC(string userIDSixNations)
    {
        SixNationsJoined = true;
        SixNations = userIDSixNations;
        Debug.LogError(SixNationsJoined);
        Debug.LogError(SixNations);
        // Mapping shit
    }

    [PunRPC]
    void munseeJoinedRPC(string userIDMunsee)
    {
        MunseeJoined = true;
        Munsee = userIDMunsee;
        Debug.LogError(MunseeJoined);
        Debug.LogError(Munsee);
        // Mapping shit
    }


    // MAIN SCENE STUFF

    [PunRPC]
    void mainSceneCameraRPC(Vector3 transform)
    {
        bool instantiatedCamera = false;
        if(PhotonNetwork.LocalPlayer.ToString() == Dutch && AlreadyLoaded == false)
        {
            Debug.LogError("Instiated" + AlreadyLoaded);
            DutchCamera = PhotonView.Instantiate(DutchCamera);
            instantiatedCamera = true;
            //DutchCamera.SetActive(true);
        }
        if (PhotonNetwork.LocalPlayer.ToString() == SixNations)
        {
            Debug.LogError("C");
            //SixNationsCamera = GameObject.FindGameObjectWithTag("Six Nations Camera");
            PhotonView.Instantiate(SixNationsCamera);
            // DutchCamera.SetActive(false);
            //SixNationsCamera.SetActive(true);
        }
        if (PhotonNetwork.LocalPlayer.ToString() == Munsee)
        {
            //DutchCamera.SetActive(false);
            MunseeCamera.SetActive(true);
        }
        if (PhotonNetwork.LocalPlayer.ToString() == Philipses)
        {
            //DutchCamera.SetActive(false);
            PhilipsesCamera.SetActive(true);

        }
    }

}
