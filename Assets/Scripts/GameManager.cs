using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public bool DebugStart;
    
    // Instantiating Singleton GameManager
    public static GameManager instance;

    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }


    }

    // Stores individual userID
    public string Dutch;
    public string SixNations;
    public string Munsee;
    public string Philipses;

    // Whether or not a team is joined
    public bool SixNationsJoined = false;
    public bool MunseeJoined = false;
    public bool DutchJoined = false;
    public bool PhilipsesJoined = false;

    // Prefab Gameobject of Camera
    public GameObject DutchCameraGameObject;
    public GameObject SixNationsCameraGameObject;
    public GameObject MunseeCameraGameObject;
    public GameObject PhilipsesCameraGameObject;

    // Prefab PhotonView of Camera
    public PhotonView DutchCameraPrefab;
    public PhotonView SixNationsCameraPrefab;
    public PhotonView MunseeCameraPrefab;
    public PhotonView PhilipsesCameraPrefab;

    // Actual in-game PhotonView (post-instantiation)
    public PhotonView DutchCamera;
    public PhotonView SixNationsCamera;
    public PhotonView MunseeCamera;
    public PhotonView PhilipsesCamera;


    // Canvases which attach instantiated camera later on
    public GameObject DutchTextCanvasObject;
    public GameObject DutchBackgroundCanvasObject;
    public GameObject DutchCardsCanvasObject;
    public GameObject SixNationsTextCanvasObject;
    public GameObject SixNationsBackgroundCanvasObject;
    public GameObject SixNationsCardsCanvasObject;
    public GameObject MunseeTextCanvasObject;
    public GameObject MunseeBackgroundCanvasObject;
    public GameObject MunseeCardsCanvasObject;
    public GameObject PhilipsesTextCanvasObject;
    public GameObject PhilipsesBackgroundCanvasObject;
    public GameObject PhilipsesCardsCanvasObject;

    public GameObject DutchObject;
    public GameObject PhilipsesObject;
    public GameObject SixNationsObject;
    public GameObject MunseeObject;

    public bool AlreadyLoaded = false;

    // Currently Trading Teams
    public bool DutchTrading = false;
    public bool SixNationsTrading = false;
    public bool MunseeTrading = false;
    public bool PhilipsesTrading = false;

    public bool DutchAccepted = false;
    public bool SixNationsAccepted = false;
    public bool MunseeAccepted = false;
    public bool PhilipsesAccepted = false;

    public int numberOfAcceptedTeams = 0;

    public GameObject[] DutchTradeButton = { };
    public GameObject[] SixNationsTradeButton = { };
    public GameObject[] MunseeTradeButton = { };
    public GameObject[] PhilipsesTradeButton = { };

    public GameObject gameManager; // This Object



    // TRADING VARIABLES

    public int InventoryCardsInTrade = 0;
    public int WishlistCardsInTrade = 0;
    

    public GameObject BeaverCard;
    public GameObject DuffelsCard;
    public GameObject DeerSkinCard;

    public string theSender = "";
    public float distanceFromLineGiving = 1.5f;
    public float distanceFromLineReceiving = 6.08f;
    public float ZAxisLineDistance = 0;
    public float YAxisLineDistance = 0.10f;


    public bool clearTradeButton = false;


    /*
    STARTING INVENTORIES

    Six Nations 
    Beaver Pelts - 12 cards
    Deerskins - 5 cards
    Bear Pelts - 6 cards
    Fisher Pelts - 4 cards
    Fox Pelts - 3 cards 

    Munsee
    Beaver Pelts - 10 cards
    Deerskins - 6 cards
    Bear Pelts - 2 cards
    Fisher Pelts - 5 cards
    Fox Pelts - 13 cards
    Schepels of Corn - 6 cards

    Philipses
    Duffels Blankets - 3 cards
    Linen Shirts - 8 cards
    Pairs of Stockings - 10 cards
    Ells of Strouds - 4 cards
    Large Axes - 2 cards
    Strings of Beads - 12 cards
    Scissors - 5 cards

    Dutch
    Duffels Blankets - 12 cards
    Ells of Strouds - 9 cards
    Large Axes - 5 cards
    Scissors - 3 cards
    Strings of Beads - 20 cards

    */

    // CHECK AMOUNTS IN INSPECTOR THEY MIGHT NOT MATCH
    // Note: First 13 Elements are the inventory amounts, last 13 elements are the wishlisted amounts. A comment indicator has been placed when wishlist starts
    public int[] SixNationsAmounts = {12, 5, 6, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 9, 4, 6, 3, 3, 20, 2};
    public int[] MunseeAmounts =     {10, 6, 2, 5, 13, 6, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 6, 4, 4, 10, 4, 12, 6};
    public int[] PhilipsesAmounts =  {0, 0, 0, 0, 0, 0, 3, 8, 10, 4, 2, 3, 5,/**/ 10, 7, 4, 6, 4, 6, 0, 0, 0, 0, 0, 0, 0};
    public int[] DutchAmounts =      {0, 0, 0, 0, 0, 0, 12, 0, 0, 9, 5, 20, 3,/**/ 12, 4, 4, 5, 10, 0, 0, 0, 0, 0, 0, 0, 0};

    // NEEDS TO BE CHANGED IN FINAL GAME
    public int[] SixNationsWampumValues = { 108, 40, 30, 16, 6, 6, 27, 56, 70, 24, 6, 12, 5 };
    public int[] MunseeWampumValues = { 108, 40, 30, 16, 6, 6, 27, 56, 70, 24, 6, 12, 5 };
    public int[] PhilipsesWampumValues = { 108, 40, 30, 16, 6, 6, 27, 56, 70, 24, 6, 12, 5 };
    public int[] DutchWampumValues = { 108, 40, 30, 16, 6, 6, 27, 56, 70, 24, 6, 12, 5 };


    public List<int> SixNationsWampumValuesTrades = new List<int>();
    public List<int> MunseeWampumValuesTrades = new List<int>();
    public List<int> PhilipsesWampumValuesTrades = new List<int>();
    public List<int> DutchWampumValuesTrades = new List<int>();

    public int SixNationsWampum;
    public int MunseeWampum;
    public int PhilipsesWampum;
    public int DutchWampum;

    public GameObject[] SixNationsAmountsGameObjects = { };
    public GameObject[] MunseeAmountsGameObjects = { };
    public GameObject[] PhilipsesAmountsGameObjects = { };
    public GameObject[] DutchAmountsGameObjects = { };
    public GameObject[] Prefabs;
    public string[] tags = { };


    public string[] teamNames = { "Dutch", "Philipses", "Six Nations", "Munsee" }; // FOR ENEMY TEAM BUTTONS
    public string[] cameras = { "Dutch", "Philipses", "Six Nations", "Munsee"}; // FOR CAMERA 


    public GameObject[] tradeGivingCardsParent = {null, null, null, null }; // All giving card parents
    public GameObject[] tradeReceivingCardsParent = {null, null, null, null }; // All receiving card parents


    // TURN SYSTEM VARIABLES
    public int turn = 1;
    public int totalTurnNumber = 1;
    public float TurnTimer = 90.0f;
    
    public GameObject[] SeasonalTimers = { };

    public bool doNotDoAnything = false;
    
    /*
     Turn numbers:
     Dutch - 1
     Philipses - 2
     Six Nations - 3
     Munsee - 4
     */



    void Start()
    {
        Debug.Log(Prefabs.Length);
        gameManager = this.gameObject;
        Debug.Log(Prefabs[0]);
        for(int i = 0; i < Prefabs.Length; i++)
        {
            Debug.Log(i);
            tags[i] = Prefabs[i].ToString().Remove(Prefabs[i].ToString().Length - 29);
        }


}


    void Update()
    {
        
        // Main Scene
        //Debug.Log(DutchJoined);

        if ((DutchJoined && !AlreadyLoaded && SixNationsJoined && MunseeJoined && PhilipsesJoined) || (DebugStart == true && DutchJoined && !AlreadyLoaded))
        {
            Debug.Log("Teans joined, loading main screen");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
                SceneManager.LoadScene(1);
                
            }
            this.GetComponent<PhotonView>().RPC("mainSceneCameraRPC", RpcTarget.All);
            this.GetComponent<PhotonView>().RPC("mainSceneSetInventoryAmountsRPC", RpcTarget.All);
            DeactivateAllOtherButtons();
            DeactivateTeamFlags();

            AlreadyLoaded = true;
        }

        //aggregiously bad code
        if (SceneManager.GetActiveScene().name == "Final_Wampum_Value")
        {
            Debug.Log("Inside if");
            GameObject.FindGameObjectWithTag("DutchWampumText").gameObject.GetComponent<Text>().text = "Dutch Wampum: " + DutchWampum.ToString();
            GameObject.FindGameObjectWithTag("PhilipsesWampumText").gameObject.GetComponent<Text>().text = "Philipses Wampum: " + PhilipsesWampum.ToString();
            GameObject.FindGameObjectWithTag("SixNationsWampumText").gameObject.GetComponent<Text>().text = "Six Nations Wampum: " + SixNationsWampum.ToString();
            GameObject.FindGameObjectWithTag("MunseeWampumText").gameObject.GetComponent<Text>().text = "Munsee Wampum: " + MunseeWampum.ToString();
        }


        // Team Select Scene

        // Passes in userID into variables
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
    }



    public void moveSceneIfReadyCaller()
    {
        this.GetComponent<PhotonView>().RPC("moveSceneIfReady", RpcTarget.All);
        Debug.Log("did i get called");
    }

    [PunRPC] 
    void moveSceneIfReady()
    {
        /*Debug.Log(DutchJoined);

        if (DutchJoined && !AlreadyLoaded) // && SixNationsJoined && MunseeJoined && PhilipsesJoined
        {
            Debug.Log("Teans joined, loading main screen");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
                SceneManager.LoadScene(1);
                this.GetComponent<PhotonView>().RPC("mainSceneCameraRPC", RpcTarget.All);
                this.GetComponent<PhotonView>().RPC("mainSceneSetInventoryAmountsRPC", RpcTarget.All);
            }

            DeactivateAllOtherButtons();
            DeactivateTeamFlags();

            AlreadyLoaded = true;
        }*/
    }



    [PunRPC]
    void philipsesJoinedRPC(string userIDPhilipses)
    {
        PhilipsesJoined = true;
        Philipses = userIDPhilipses;
        Debug.LogError(PhilipsesJoined);
        Debug.LogError(Philipses);
        // Mapping
    }

    [PunRPC]
    void dutchJoinedRPC(string userIDDutch)
    {
        DutchJoined = true;
        Dutch = userIDDutch;
        Debug.LogError(DutchJoined);
        Debug.LogError(Dutch);

        // Mapping
    }

    [PunRPC]
    void sixNationsJoinedRPC(string userIDSixNations)
    {
        SixNationsJoined = true;
        SixNations = userIDSixNations;
        Debug.LogError(SixNationsJoined);
        Debug.LogError(SixNations);
        // Mapping
    }

    [PunRPC]
    void munseeJoinedRPC(string userIDMunsee)
    {
        MunseeJoined = true;
        Munsee = userIDMunsee;
        Debug.LogError(MunseeJoined);
        Debug.LogError(Munsee);
        // Mapping
    }




    // MAIN SCENE






    public GameObject[] MunseeCamerasCheckArray = { };

    [PunRPC]
    void mainSceneCameraRPC()
    {
        //Debug.LogError(SceneManager.GetActiveScene().name);
        DutchCardsCanvasObject = GameObject.FindGameObjectWithTag("Dutch Card Canvas");
        PhilipsesCardsCanvasObject = GameObject.FindGameObjectWithTag("Philipses Card Canvas");
        SixNationsCardsCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Card Canvas");
        MunseeCardsCanvasObject = GameObject.FindGameObjectWithTag("Munsee Card Canvas");

        GameObject[] DutchCamerasCheckArray = GameObject.FindGameObjectsWithTag("DWIC Camera");
        if (PhotonNetwork.LocalPlayer.ToString() == Dutch && AlreadyLoaded == false && DutchCamerasCheckArray.Length <= 1)
        {
            DutchCameraPrefab = DutchCameraGameObject.GetPhotonView();
            DutchCamera = PhotonView.Instantiate(DutchCameraPrefab);
            DutchTextCanvasObject = GameObject.FindGameObjectWithTag("Dutch Text Canvas");
            DutchBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Dutch Background Canvas");
            DutchCamera.transform.parent = DutchTextCanvasObject.transform.parent;
            DutchTextCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            DutchBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            DutchCardsCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            //DutchCamera.SetActive(true);
        }
        GameObject[] SixNationsCamerasCheckArray = GameObject.FindGameObjectsWithTag("Six Nations Camera");
        if (PhotonNetwork.LocalPlayer.ToString() == SixNations && AlreadyLoaded == false && SixNationsCamerasCheckArray.Length <= 1)
        {
            SixNationsCameraPrefab = SixNationsCameraGameObject.GetPhotonView();
            SixNationsCamera = PhotonView.Instantiate(SixNationsCameraPrefab);
            SixNationsTextCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Text Canvas");
            SixNationsBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Background Canvas");
            SixNationsCamera.transform.parent = SixNationsTextCanvasObject.transform.parent;
            SixNationsTextCanvasObject.GetComponent<Canvas>().worldCamera = SixNationsCamera.gameObject.GetComponent<Camera>();
            SixNationsBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = SixNationsCamera.gameObject.GetComponent<Camera>();
            SixNationsCardsCanvasObject.GetComponent<Canvas>().worldCamera = SixNationsCamera.gameObject.GetComponent<Camera>();

        }
        MunseeCamerasCheckArray = GameObject.FindGameObjectsWithTag("Munsee Camera");
        if (PhotonNetwork.LocalPlayer.ToString() == Munsee && AlreadyLoaded == false && MunseeCamerasCheckArray.Length <= 1)
        {
            Debug.Log("Ran1");
            MunseeCameraPrefab = MunseeCameraGameObject.GetPhotonView();
            MunseeCamera = PhotonView.Instantiate(MunseeCameraPrefab);
            MunseeTextCanvasObject = GameObject.FindGameObjectWithTag("Munsee Text Canvas");
            MunseeBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Munsee Background Canvas");
            MunseeCamera.transform.parent = MunseeTextCanvasObject.transform.parent;
            MunseeTextCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
            MunseeBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
            MunseeCardsCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
        }
        GameObject[] PhilipsesCamerasCheckArray = GameObject.FindGameObjectsWithTag("Philipse Camera");
        if (PhotonNetwork.LocalPlayer.ToString() == Philipses && AlreadyLoaded == false && PhilipsesCamerasCheckArray.Length <= 1)
        {
            PhilipsesCameraPrefab = PhilipsesCameraGameObject.GetPhotonView();
            PhilipsesCamera = PhotonView.Instantiate(PhilipsesCameraPrefab);
            PhilipsesTextCanvasObject = GameObject.FindGameObjectWithTag("Philipses Text Canvas");
            PhilipsesBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Philipses Background Canvas");
            PhilipsesCamera.transform.parent = PhilipsesTextCanvasObject.transform.parent;
            PhilipsesTextCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
            PhilipsesBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
            PhilipsesCardsCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
        }

        // Arrays of trade buttons
        DutchTradeButton = GameObject.FindGameObjectsWithTag("Dutch Trading");
        SixNationsTradeButton = GameObject.FindGameObjectsWithTag("Six Nations Trading");  
        MunseeTradeButton = GameObject.FindGameObjectsWithTag("Munsee Trading");
        PhilipsesTradeButton = GameObject.FindGameObjectsWithTag("Philipses Trading");

        for (int aa = 0; aa < DutchTradeButton.Length; aa++)
        {
            Debug.Log("Dutch Trade Button sub " + aa + ": " + DutchTradeButton[aa]);
        }

        // If there is somehow than one of specified camera
        //GameObject[] DutchCamerasCheckArray = GameObject.FindGameObjectsWithTag("DWIC Camera");
/*        for(int a = 0; a < DutchCamerasCheckArray.Length; a++)
        {
            if (DutchTextCanvasObject.GetComponent<Canvas>().worldCamera != DutchCamerasCheckArray[a].GetComponent<Camera>() || DutchBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != DutchCamerasCheckArray[a].GetComponent<Camera>() || DutchCardsCanvasObject.GetComponent<Canvas>().worldCamera != DutchCamerasCheckArray[a].GetComponent<Camera>())
            {
                DutchCamerasCheckArray[a].SetActive(false);
            }
            else
            {
                DutchCamerasCheckArray[a].SetActive(true);
            }
        }*/
/*        GameObject[] SixNationsCamerasCheckArray = GameObject.FindGameObjectsWithTag("Six Nations Camera");
        for (int a = 0; a < SixNationsCamerasCheckArray.Length; a++)
        {
            if (SixNationsTextCanvasObject.GetComponent<Canvas>().worldCamera != SixNationsCamerasCheckArray[a].GetComponent<Camera>() || SixNationsBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != SixNationsCamerasCheckArray[a].GetComponent<Camera>() || DutchCardsCanvasObject.GetComponent<Canvas>().worldCamera != SixNationsCamerasCheckArray[a].GetComponent<Camera>())
            {
                SixNationsCamerasCheckArray[a].SetActive(false);
            }
            else
            {
                SixNationsCamerasCheckArray[a].SetActive(true);
            }
        }*/
       
        for (int a = 0; a < MunseeCamerasCheckArray.Length; a++)
        {
            if (MunseeTextCanvasObject.GetComponent<Canvas>().worldCamera != MunseeCamerasCheckArray[a].GetComponent<Camera>() || MunseeBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != MunseeCamerasCheckArray[a].GetComponent<Camera>() || MunseeCamerasCheckArray[a].GetComponent<Canvas>().worldCamera != MunseeCamerasCheckArray[a].GetComponent<Camera>())
            {
                MunseeCamerasCheckArray[a].SetActive(false);
            }
            else
            {
                MunseeCamerasCheckArray[a].SetActive(true);
            }
        }
        //GameObject[] PhilipsesCamerasCheckArray = GameObject.FindGameObjectsWithTag("Philipse Camera");
/*        for (int a = 0; a < PhilipsesCamerasCheckArray.Length; a++)
        {
            if (PhilipsesTextCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>() || PhilipsesBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>() || PhilipsesCardsCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>())
            {
                PhilipsesCamerasCheckArray[a].SetActive(false);
            }
            else
            {
                PhilipsesCamerasCheckArray[a].SetActive(true);
            }
        }*/


    }


    [PunRPC]
    void mainSceneSetInventoryAmountsRPC() // TO DO
    {
        DutchObject = GameObject.FindGameObjectWithTag("Dutch");
        PhilipsesObject = GameObject.FindGameObjectWithTag("Philipses");
        SixNationsObject = GameObject.FindGameObjectWithTag("Six Nations");
        MunseeObject = GameObject.FindGameObjectWithTag("Munsee");
        GameObject[] AmountsGameObjectsWithTag = { };
        if (!AlreadyLoaded)
        {
            Debug.Log("DID I RUN?");
            for (int i = 0; i < DutchAmounts.Length; i++)
            {
                if (i < 13)
                {
                    Debug.Log("DID I RUN??");
                    Debug.Log(i);
                    AmountsGameObjectsWithTag = GameObject.FindGameObjectsWithTag(tags[i] + "Amount");
                    for(int j = 0; j < AmountsGameObjectsWithTag.Length; j++)
                    {
                        if(AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Dutch")
                        {
                            DutchAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            DutchAmountsGameObjects[i].GetComponent<Text>().text = DutchAmounts[i].ToString() + "x";
                        }
                        else if(AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Philipses")
                        {
                            PhilipsesAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            PhilipsesAmountsGameObjects[i].GetComponent<Text>().text = PhilipsesAmounts[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Six Nations")
                        {
                            SixNationsAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            SixNationsAmountsGameObjects[i].GetComponent<Text>().text = SixNationsAmounts[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Munsee")
                        {
                            MunseeAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            MunseeAmountsGameObjects[i].GetComponent<Text>().text = MunseeAmounts[i].ToString() + "x";
                        }
                    }
                    
                }
                // Part of Wishlist
                else
                {
                    if (DutchAmounts[i] == 0)
                    {
                        DutchAmountsGameObjects[i] = null;
                    }
                    if (PhilipsesAmounts[i] == 0)
                    {
                        DutchAmountsGameObjects[i] = null;
                    }
                    if (SixNationsAmounts[i] == 0)
                    {
                        DutchAmountsGameObjects[i] = null;
                    }
                    if (MunseeAmounts[i] == 0)
                    {
                        DutchAmountsGameObjects[i] = null;
                    }
                    
                    
                    AmountsGameObjectsWithTag = GameObject.FindGameObjectsWithTag(tags[i - 13] + " Amount Wishlist");
                    for (int j = 0; j < AmountsGameObjectsWithTag.Length; j++)
                    {
                        if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Dutch")
                        {
                            DutchAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            DutchAmountsGameObjects[i].GetComponent<Text>().text = DutchAmounts[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Philipses")
                        {
                            PhilipsesAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            PhilipsesAmountsGameObjects[i].GetComponent<Text>().text = PhilipsesAmounts[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Six Nations")
                        {
                            SixNationsAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            SixNationsAmountsGameObjects[i].GetComponent<Text>().text = SixNationsAmounts[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Munsee")
                        {
                            MunseeAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            MunseeAmountsGameObjects[i].GetComponent<Text>().text = MunseeAmounts[i].ToString() + "x";
                        }
                    }
                        
                    
                }


            }
        }

        GameObject[] tradeGivingCardsParentfake = GameObject.FindGameObjectsWithTag("Giving");

        for (int y = 0; y < tradeGivingCardsParentfake.Length; y++)
        {
            Debug.Log(y);
            Debug.Log(tradeGivingCardsParent[0]);
            Debug.Log(y);
            if (tradeGivingCardsParentfake[y].transform.parent.parent.name == "Dutch")
            {
                tradeGivingCardsParent[0] = tradeGivingCardsParentfake[y];
            }
            else if (tradeGivingCardsParentfake[y].transform.parent.parent.name == "Philipses")
            {
                tradeGivingCardsParent[1] = tradeGivingCardsParentfake[y];
            }
            else if (tradeGivingCardsParentfake[y].transform.parent.parent.name == "Six Nations")
            {
                tradeGivingCardsParent[2] = tradeGivingCardsParentfake[y];
            }
            else if (tradeGivingCardsParentfake[y].transform.parent.parent.name == "Munsee")
            {
                tradeGivingCardsParent[3] = tradeGivingCardsParentfake[y];
            }
            else
            {
                Debug.Log("smth isn't working");
            }

        }

        GameObject[] tradeReceivingCardsParentfake = GameObject.FindGameObjectsWithTag("Receiving");
        
        for (int y = 0; y < tradeReceivingCardsParentfake.Length; y++)
        {
            if (tradeReceivingCardsParentfake[y].transform.parent.parent.name == "Dutch")
            {
                tradeReceivingCardsParent[0] = tradeReceivingCardsParentfake[y];
            }
            else if (tradeReceivingCardsParentfake[y].transform.parent.parent.name == "Philipses")
            {
                tradeReceivingCardsParent[1] = tradeReceivingCardsParentfake[y];
            }
            else if (tradeReceivingCardsParentfake[y].transform.parent.parent.name == "Six Nations")
            {
                tradeReceivingCardsParent[2] = tradeReceivingCardsParentfake[y];
            }
            else if (tradeReceivingCardsParentfake[y].transform.parent.parent.name == "Munsee")
            {
                tradeReceivingCardsParent[3] = tradeReceivingCardsParentfake[y];
            }
            else
            {
                Debug.Log("smth isn't working");
            }

        }
        SeasonalTimers = GameObject.FindGameObjectsWithTag("Seasonal Timer");
        for (int k = 0; k < SeasonalTimers.Length; k++)
        {
            SeasonalTimers[k].GetComponent<Text>().text = "Year: " + (totalTurnNumber + 1600).ToString() + " | Turn: " + teamNames[turn - 1].ToString();
        }
    }

    public void DeactivateAllOtherButtons()
    {
        GameObject[] inventoryCards = GameObject.FindGameObjectsWithTag("Inventory");
        GameObject[] wishlistCards = GameObject.FindGameObjectsWithTag("Wishlist");

        for (int x = 0; x < inventoryCards.Length; x++)
        {
            if (inventoryCards[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                inventoryCards[x].gameObject.SetActive(false);
            }

        }
        for (int x = 0; x < wishlistCards.Length; x++)
        {
            if (wishlistCards[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                wishlistCards[x].gameObject.SetActive(false);
            }

        }
   
    }


    Vector3[] DutchOriginalPositions = { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] PhilipsesOriginalPositions = { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] SixNationsOriginalPositions = { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] MunseeOriginalPositions = { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };

    public void DeactivateTeamFlags()
    {
        
        for (int x = 0; x < teamNames.Length; x++)
        {
            Debug.Log("X: " + x);
            if (DutchTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                
                DutchOriginalPositions[x] = DutchTradeButton[x].transform.position;
                DutchTradeButton[x].gameObject.transform.position = new Vector3(1000, 1000, 1000);
            }
            if (PhilipsesTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                PhilipsesOriginalPositions[x] = PhilipsesTradeButton[x].transform.position;
                PhilipsesTradeButton[x].gameObject.transform.position = new Vector3(1000, 1000, 1000);
            }
            if (SixNationsTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                SixNationsOriginalPositions[x] = SixNationsTradeButton[x].transform.position;
                SixNationsTradeButton[x].gameObject.transform.position = new Vector3(1000, 1000, 1000);
            }
            if (MunseeTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                MunseeOriginalPositions[x] = MunseeTradeButton[x].transform.position;
                MunseeTradeButton[x].gameObject.transform.position = new Vector3(1000, 1000, 1000);
            }
            
        }
    }

    public void CallReactivateTeamFlagsRPC()
    {
        this.GetComponent<PhotonView>().RPC("ReactivateTeamFlags", RpcTarget.All);
    }
    [PunRPC]
    public void ReactivateTeamFlags()
    {
        for (int x = 0; x < teamNames.Length; x++)
        {

            if (DutchTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                DutchTradeButton[x].gameObject.transform.position = DutchOriginalPositions[x];
            }
            if (PhilipsesTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                PhilipsesTradeButton[x].gameObject.transform.position = PhilipsesOriginalPositions[x];
            }
            if (SixNationsTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                SixNationsTradeButton[x].gameObject.transform.position = SixNationsOriginalPositions[x];
            }
            if (MunseeTradeButton[x].transform.parent.parent.name != findPlayerTeamForDeactivation(PhotonNetwork.LocalPlayer.ToString()))
            {
                MunseeTradeButton[x].gameObject.transform.position = MunseeOriginalPositions[x];
            }
            
        }
    }



    public string findPlayerTeam(string userID)
    {
        if(userID == Dutch)
        {
            return "Dutch";
        }
        if(userID == SixNations)
        {
            return "SixNations";
        }
        if(userID == Philipses)
        {
            return "Philipses";
        }
        return "Munsee";

    }

    public string findPlayerTeamForDeactivation(string userID)
    {
        if (userID == Dutch)
        {
            return "Dutch";
        }
        if (userID == SixNations)
        {
            return "Six Nations";
        }
        if (userID == Philipses)
        {
            return "Philipses";
        }
        return "Munsee";

    }

    public Vector3[] enemyTeamButtonPos = { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    public GameObject[] instantiatedCard = { null, null, null, null };
    public GameObject[] CardsCanvasObjects = { null, null, null, null };
    [PunRPC]
    void addCardToTrade(string tag, string parentTag, PhotonMessageInfo info)
    {
        string playerString = info.Sender.ToString();
        Debug.Log("Player: " + playerString);
        Debug.Log("Sender: " + info.Sender.ToString());
        Debug.Log((turn == 1 && playerString == Dutch));
        if ((turn == 1 && playerString == Dutch) || (turn == 2 && playerString == Philipses) || (turn == 3 && playerString == SixNations) || (turn == 4 && playerString == Munsee))
        {
            //Vector3[] enemyTeamButtonPos = { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
            bool[] addToReceiving = { false, false, false, false }; // Add to the receiving side of the trade (right) instead of the giving side (left)


            // Sets up enemy team button positions & addToReceiving

            #region


            if (playerString == Dutch && DutchTrading == true)
            {
                

                if (SixNationsTrading == true) // If six nations is the enemy team
                {
                    for (int a = 0; a < SixNationsTradeButton.Length; a++)
                    {
                        
                        if (SixNationsTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = SixNationsTradeButton[a].transform.position;
                        }
                        else if (SixNationsTradeButton[1].transform.parent.parent.name == "Philipses") // If the screen is Philipses
                        {
                            enemyTeamButtonPos[1] = SixNationsTradeButton[a].transform.position;

                        }
                        if (SixNationsTradeButton[a].transform.parent.parent.name == "Six Nations") // If the screen is Six Nations
                        {
                            addToReceiving[2] = true;
                            enemyTeamButtonPos[2] = DutchTradeButton[a].transform.position;
                        }

                        else if (SixNationsTradeButton[a].transform.parent.parent.name == "Munsee") // If the screen is Munsee
                        {
                            addToReceiving[3] = true;
                            enemyTeamButtonPos[3] = SixNationsTradeButton[a].transform.position;

                        }

                        
                    }

                }

                else if (MunseeTrading == true)
                {
                    for (int a = 0; a < MunseeTradeButton.Length; a++)
                    {
                        if (MunseeTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = MunseeTradeButton[a].transform.position;
                        }
                        else if (MunseeTradeButton[1].transform.parent.parent.name == "Philipses") 
                        {
                            enemyTeamButtonPos[a] = MunseeTradeButton[a].transform.position;

                        }
                        else if (MunseeTradeButton[2].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = MunseeTradeButton[a].transform.position;
                        }
                        
                        else if (MunseeTradeButton[a].transform.parent.parent.name == "Munsee") // If the screen is Munsee
                        {
                            addToReceiving[3] = true;
                            enemyTeamButtonPos[3] = MunseeTradeButton[a].transform.position;

                        }
                    }
                }

                else if (PhilipsesTrading == true)
                {
                    for (int a = 0; a < PhilipsesTradeButton.Length; a++)
                    {
                        if(PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = PhilipsesTradeButton[a].transform.position;
                        }
                        /*enemyTeamButtonPos[a] = PhilipsesTradeButton[a].transform.position;*/
                        else if (PhilipsesTradeButton[a].transform.parent.parent.name == "Philipses") // If the screen is Philipses
                        {
                            addToReceiving[1] = true;
                            enemyTeamButtonPos[1] = DutchTradeButton[a].transform.position;

                        }
                        else if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = PhilipsesTradeButton[a].transform.position;
                        }
                        else if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Munsee")
                        {
                            enemyTeamButtonPos[3] = DutchTradeButton[a].transform.position;
                        }
                    }
                }

                else
                {
                    Debug.LogError("No Enemy Team Selected");
                    return;
                }
            }




            if (playerString == Philipses && PhilipsesTrading == true)
            {


                if (SixNationsTrading == true) // If six nations is the enemy team
                {
                    for (int a = 0; a < SixNationsTradeButton.Length; a++)
                    {

                        if (SixNationsTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = SixNationsTradeButton[a].transform.position;
                        }
                        else if (SixNationsTradeButton[1].transform.parent.parent.name == "Philipses") // If the screen is Philipses
                        {
                            enemyTeamButtonPos[1] = SixNationsTradeButton[a].transform.position;

                        }
                        if (SixNationsTradeButton[a].transform.parent.parent.name == "Six Nations") // If the screen is Six Nations
                        {
                            addToReceiving[2] = true;
                            enemyTeamButtonPos[2] = PhilipsesTradeButton[a].transform.position;
                        }

                        else if (SixNationsTradeButton[a].transform.parent.parent.name == "Munsee") // If the screen is Munsee
                        {
                            enemyTeamButtonPos[3] = SixNationsTradeButton[a].transform.position;

                        }


                    }

                }

                else if (MunseeTrading == true)
                {
                    for (int a = 0; a < MunseeTradeButton.Length; a++)
                    {
                        if (MunseeTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = MunseeTradeButton[a].transform.position;
                        }
                        else if (MunseeTradeButton[1].transform.parent.parent.name == "Philipses") 
                        {
                            enemyTeamButtonPos[a] = MunseeTradeButton[a].transform.position;

                        }
                        else if (MunseeTradeButton[2].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = MunseeTradeButton[a].transform.position;
                        }

                        else if (MunseeTradeButton[a].transform.parent.parent.name == "Munsee") // If the screen is Munsee
                        {
                            addToReceiving[3] = true;
                            enemyTeamButtonPos[3] = PhilipsesTradeButton[a].transform.position;

                        }
                    }
                }

                else if (DutchTrading == true)
                {
                    for (int a = 0; a < PhilipsesTradeButton.Length; a++)
                    {
                        if (DutchTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            addToReceiving[0] = true;
                            enemyTeamButtonPos[0] = PhilipsesTradeButton[a].transform.position;
                        }
                        /*enemyTeamButtonPos[a] = PhilipsesTradeButton[a].transform.position;*/
                        else if (DutchTradeButton[a].transform.parent.parent.name == "Philipses")
                        {
                            enemyTeamButtonPos[1] = DutchTradeButton[a].transform.position;

                        }
                        else if (DutchTradeButton[a].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = DutchTradeButton[a].transform.position;
                        }
                        else if (DutchTradeButton[a].transform.parent.transform.parent.name == "Munsee")
                        {
                            enemyTeamButtonPos[3] = DutchTradeButton[a].transform.position;
                        }
                    }
                }

                else
                {
                    Debug.LogError("No Enemy Team Selected");
                    return;
                }
            }
            // A

            if (playerString == SixNations && SixNationsTrading == true)
            {


                if (PhilipsesTrading == true)
                {
                    for (int a = 0; a < PhilipsesTradeButton.Length; a++)
                    {
                        if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = PhilipsesTradeButton[a].transform.position;
                        }
                        /*enemyTeamButtonPos[a] = PhilipsesTradeButton[a].transform.position;*/
                        else if (PhilipsesTradeButton[a].transform.parent.parent.name == "Philipses") // If the screen is Philipses
                        {
                            addToReceiving[1] = true;
                            enemyTeamButtonPos[1] = SixNationsTradeButton[a].transform.position;

                        }
                        else if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = PhilipsesTradeButton[a].transform.position;
                        }
                        else if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Munsee")
                        {
                            enemyTeamButtonPos[3] = DutchTradeButton[a].transform.position;
                        }
                    }
                }

                else if (MunseeTrading == true)
                {
                    for (int a = 0; a < MunseeTradeButton.Length; a++)
                    {
                        if (MunseeTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = MunseeTradeButton[a].transform.position;
                        }
                        else if (MunseeTradeButton[1].transform.parent.parent.name == "Philipses")
                        {
                            enemyTeamButtonPos[a] = MunseeTradeButton[a].transform.position;

                        }
                        else if (MunseeTradeButton[2].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = MunseeTradeButton[a].transform.position;
                        }

                        else if (MunseeTradeButton[a].transform.parent.parent.name == "Munsee") // If the screen is Munsee
                        {
                            addToReceiving[3] = true;
                            enemyTeamButtonPos[3] = SixNationsTradeButton[a].transform.position;

                        }
                    }
                }

                else if (DutchTrading == true)
                {
                    for (int a = 0; a < PhilipsesTradeButton.Length; a++)
                    {
                        if (DutchTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            addToReceiving[0] = true;
                            enemyTeamButtonPos[0] = SixNationsTradeButton[a].transform.position;
                        }
                        /*enemyTeamButtonPos[a] = PhilipsesTradeButton[a].transform.position;*/
                        else if (DutchTradeButton[a].transform.parent.parent.name == "Philipses")
                        {
                            enemyTeamButtonPos[1] = DutchTradeButton[a].transform.position;

                        }
                        else if (DutchTradeButton[a].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = DutchTradeButton[a].transform.position;
                        }
                        else if (DutchTradeButton[a].transform.parent.transform.parent.name == "Munsee")
                        {
                            enemyTeamButtonPos[3] = DutchTradeButton[a].transform.position;
                        }
                    }
                }

                else
                {
                    Debug.LogError("No Enemy Team Selected");
                    return;
                }
            }

            // A

            if (playerString == Munsee && MunseeTrading == true)
            {


                if (PhilipsesTrading == true)
                {
                    for (int a = 0; a < PhilipsesTradeButton.Length; a++)
                    {
                        if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = PhilipsesTradeButton[a].transform.position;
                        }
                        /*enemyTeamButtonPos[a] = PhilipsesTradeButton[a].transform.position;*/
                        else if (PhilipsesTradeButton[a].transform.parent.parent.name == "Philipses") // If the screen is Philipses
                        {
                            addToReceiving[1] = true;
                            enemyTeamButtonPos[1] = MunseeTradeButton[a].transform.position;

                        }
                        else if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = PhilipsesTradeButton[a].transform.position;
                        }
                        else if (PhilipsesTradeButton[a].transform.parent.transform.parent.name == "Munsee")
                        {
                            enemyTeamButtonPos[3] = DutchTradeButton[a].transform.position;
                        }
                    }
                }

                else if (SixNationsTrading == true) // If six nations is the enemy team
                {
                    for (int a = 0; a < SixNationsTradeButton.Length; a++)
                    {

                        if (SixNationsTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            enemyTeamButtonPos[0] = SixNationsTradeButton[a].transform.position;
                        }
                        else if (SixNationsTradeButton[1].transform.parent.parent.name == "Philipses") // If the screen is Philipses
                        {
                            enemyTeamButtonPos[1] = SixNationsTradeButton[a].transform.position;

                        }
                        if (SixNationsTradeButton[a].transform.parent.parent.name == "Six Nations") // If the screen is Six Nations
                        {
                            addToReceiving[2] = true;
                            enemyTeamButtonPos[2] = MunseeTradeButton[a].transform.position;
                        }

                        else if (SixNationsTradeButton[a].transform.parent.parent.name == "Munsee") // If the screen is Munsee
                        {
                            enemyTeamButtonPos[3] = SixNationsTradeButton[a].transform.position;

                        }


                    }

                }

                else if (DutchTrading == true)
                {
                    for (int a = 0; a < PhilipsesTradeButton.Length; a++)
                    {
                        if (DutchTradeButton[a].transform.parent.transform.parent.name == "Dutch")
                        {
                            addToReceiving[0] = true;
                            enemyTeamButtonPos[0] = MunseeTradeButton[a].transform.position;
                        }
                        /*enemyTeamButtonPos[a] = PhilipsesTradeButton[a].transform.position;*/
                        else if (DutchTradeButton[a].transform.parent.parent.name == "Philipses")
                        {
                            enemyTeamButtonPos[1] = DutchTradeButton[a].transform.position;

                        }
                        else if (DutchTradeButton[a].transform.parent.transform.parent.name == "Six Nations")
                        {
                            enemyTeamButtonPos[2] = DutchTradeButton[a].transform.position;
                        }
                        else if (DutchTradeButton[a].transform.parent.transform.parent.name == "Munsee")
                        {
                            enemyTeamButtonPos[3] = DutchTradeButton[a].transform.position;
                        }
                    }
                }

                else
                {
                    Debug.LogError("No Enemy Team Selected");
                    return;
                }
            }

            Vector3[] topButtonPos = {new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
            GameObject[] sixNationsButtons = GameObject.FindGameObjectsWithTag("Six Nations Trading");
            for (int ab = 0; ab < sixNationsButtons.Length; ab++)
            {
                if(sixNationsButtons[ab].transform.parent.parent.name == "Dutch")
                {
                    topButtonPos[0] = sixNationsButtons[ab].transform.position;
                }
                else if (sixNationsButtons[ab].transform.parent.parent.name == "Philipses")
                {
                    topButtonPos[1] = sixNationsButtons[ab].transform.position;
                }
                else if (sixNationsButtons[ab].transform.parent.parent.name == "Munsee")
                {
                    topButtonPos[3] = sixNationsButtons[ab].transform.position;
                }
            }
            GameObject[] dutchButtons = GameObject.FindGameObjectsWithTag("Dutch Trading");
            for (int ab = 0; ab < dutchButtons.Length; ab++)
            {
                if (dutchButtons[ab].transform.parent.parent.name == "Six Nations")
                {
                    topButtonPos[2] = dutchButtons[ab].transform.position;
                }

            }
            

















            #endregion // This  

            //
            // 
            // ACTUALLY ADDING CARD PORTION
            //
            //

            Debug.LogError("Adding card...");
            /*GameObject[] instantiatedCard = { null, null, null, null };*/
            int isParentWishlist = 0;
            int isParentInventory = 1;


            if (parentTag == "Wishlist")
            {
                isParentWishlist = 1;
                isParentInventory = 0;
            }


            Debug.Log("Is inventory: " + isParentInventory + "| Is wishlist: " + isParentWishlist);
            Debug.Log("equals: " + playerString == Dutch);
            Debug.Log("equals2: " + playerString == Philipses);
            // The Dutch Region
            #region
            if (playerString == Dutch && DutchTrading == true)
            {
                for (int z = 0; z < tags.Length; z++)
                {
                    Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((DutchAmounts[z] > 0 && isParentInventory == 1) || isParentWishlist == 1)) // If the card amounts are greater than zero or is in their wishlist
                    {

                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % 14), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }
                                else
                                { // Shouldn't do anything if we get rid of the isparentwishlist side
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % 14))), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }

                            }
                            Debug.Log(DutchAmounts[z]);
                            DutchAmounts[z]--;
                            DutchAmountsGameObjects[z].GetComponent<Text>().text = DutchAmounts[z].ToString() + "x";
                        }
                        else
                        {
                            Debug.Log(DutchAmounts[z + 13]);
                            if (DutchAmountsGameObjects[z + 13] != null && DutchAmounts[z + 13] > 0)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3(distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % 14)), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {
                                        
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineReceiving + ((float)0.3 * (WishlistCardsInTrade % 14))), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                DutchAmounts[z + 13]--;
                                DutchAmountsGameObjects[z + 13].GetComponent<Text>().text = DutchAmounts[z + 13].ToString() + "x";

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                return;
                            }
                        }
                        //


                        break;
                    }
                    else
                    {
                        Debug.LogError("None of specified card left");
                        if (z + 1 == tags.Length)
                        {
                            return;
                        }
                    }
                }
            }
            #endregion
            // The Philipses Region

            #region 
            if (playerString == Philipses && PhilipsesTrading == true)
            {
                for (int z = 0; z < tags.Length; z++)
                {
                    Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((PhilipsesAmounts[z] > 0 && isParentInventory == 1) || isParentWishlist == 1)) // If the card amounts are greater than zero or is in their wishlist
                    {

                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % 14), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }
                                else
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % 14))), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }

                            }
                            Debug.Log(PhilipsesAmounts[z]);
                            PhilipsesAmounts[z]--;
                            PhilipsesAmountsGameObjects[z].GetComponent<Text>().text = PhilipsesAmounts[z].ToString() + "x";
                        }
                        else
                        {
                            Debug.Log(PhilipsesAmounts[z + 13]);
                            if (PhilipsesAmountsGameObjects[z + 13] != null && PhilipsesAmounts[z + 13] > 0)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % 14))), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % 14), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                PhilipsesAmounts[z + 13]--;
                                PhilipsesAmountsGameObjects[z + 13].GetComponent<Text>().text = PhilipsesAmounts[z + 13].ToString() + "x";

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                return;
                            }
                        }
                        //


                        break;
                    }
                    else
                    {
                        Debug.LogError("None of specified card left");
                        if (z + 1 == tags.Length)
                        {
                            return;
                        }
                    }
                }
            }
            #endregion
            // The Six Nations Region
            #region
            if (playerString == SixNations && SixNationsTrading == true)
            {
                for (int z = 0; z < tags.Length; z++)
                {
                    Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((SixNationsAmounts[z] > 0 && isParentInventory == 1) || isParentWishlist == 1)) // If the card amounts are greater than zero or is in their wishlist
                    {

                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % 14), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }
                                else
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % 14))), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }

                            }
                            Debug.Log(SixNationsAmounts[z]);
                            SixNationsAmounts[z]--;
                            SixNationsAmountsGameObjects[z].GetComponent<Text>().text = SixNationsAmounts[z].ToString() + "x";
                        }
                        else
                        {
                            Debug.Log(SixNationsAmounts[z + 13]);
                            if (SixNationsAmountsGameObjects[z + 13] != null && SixNationsAmounts[z + 13] > 0)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % 14))), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % 14), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                SixNationsAmounts[z + 13]--;
                                SixNationsAmountsGameObjects[z + 13].GetComponent<Text>().text = SixNationsAmounts[z + 13].ToString() + "x";

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                return;
                            }
                        }
                        //


                        break;
                    }
                    else
                    {
                        Debug.LogError("None of specified card left");
                        if (z + 1 == tags.Length)
                        {
                            return;
                        }
                    }
                }
            }
            #endregion
            // The Munsee Region
            #region
            if (playerString == Munsee && MunseeTrading == true)
            {
                for (int z = 0; z < tags.Length; z++)
                {
                    Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((MunseeAmounts[z] > 0 && isParentInventory == 1) || isParentWishlist == 1)) // If the card amounts are greater than zero or is in their wishlist
                    {

                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % 14), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }
                                else
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % 14))), YAxisLineDistance - (InventoryCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                }

                            }
                            Debug.Log(MunseeAmounts[z]);
                            MunseeAmounts[z]--;
                            MunseeAmountsGameObjects[z].GetComponent<Text>().text = MunseeAmounts[z].ToString() + "x";
                        }
                        else
                        {
                            Debug.Log(MunseeAmounts[z + 13]);
                            if (MunseeAmountsGameObjects[z + 13] != null && MunseeAmounts[z + 13] > 0)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % 14))), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % 14), YAxisLineDistance - (WishlistCardsInTrade / 14), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                MunseeAmounts[z + 13]--;
                                MunseeAmountsGameObjects[z + 13].GetComponent<Text>().text = MunseeAmounts[z + 13].ToString() + "x";

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                return;
                            }
                        }
                        //


                        break;
                    }
                    else
                    {
                        Debug.LogError("None of specified card left");
                        if (z + 1 == tags.Length)
                        {
                            return;
                        }
                    }
                }
            }
            #endregion
            CardsCanvasObjects[0] = DutchCardsCanvasObject;
            CardsCanvasObjects[1] = PhilipsesCardsCanvasObject;
            CardsCanvasObjects[2] = SixNationsCardsCanvasObject;
            CardsCanvasObjects[3] = MunseeCardsCanvasObject;
            /*CardsCanvasObjects = {DutchCardsCanvasObject, PhilipsesCardsCanvasObject, SixNationsCardsCanvasObject, MunseeCardsCanvasObject };*/

            if (parentTag == "Wishlist")
            {
                WishlistCardsInTrade++;
            }
            else
            {
                InventoryCardsInTrade++;
            }
            for (int j = 0; j < instantiatedCard.Length; j++)
            {
                instantiatedCard[j].GetComponent<Button>().enabled = false;

                instantiatedCard[j].transform.position = new Vector3(instantiatedCard[j].transform.position.x, instantiatedCard[j].transform.position.y, 10);
                Debug.LogError(instantiatedCard[j].transform.position);

                if (parentTag == "Wishlist")
                {

                    Debug.Log(WishlistCardsInTrade);
                    if (addToReceiving[j] == true)
                    {
                        Debug.Log(CardsCanvasObjects[j].tag);
                        Debug.Log(j);
                        instantiatedCard[j].transform.SetParent(CardsCanvasObjects[j].transform.GetChild(0)); // SETS IT TO GIVING CARD
                    }
                    else
                    {
                        Debug.Log(CardsCanvasObjects[j].tag);
                        Debug.Log(j);
                        instantiatedCard[j].transform.SetParent(CardsCanvasObjects[j].transform.GetChild(1)); // SETS IT TO RECEIVING CARD
                    }

                }
                else
                {
                    Debug.Log(InventoryCardsInTrade);
                    if (addToReceiving[j] == true)
                    {
                        Debug.Log(CardsCanvasObjects[j].tag);
                        Debug.Log(j);
                        instantiatedCard[j].transform.SetParent(CardsCanvasObjects[j].transform.GetChild(1)); // SETS IT TO RECEIVING CARD
                    }
                    else
                    {
                        Debug.Log(CardsCanvasObjects[j].tag);
                        Debug.Log(j);
                        instantiatedCard[j].transform.SetParent(CardsCanvasObjects[j].transform.GetChild(0)); // SETS IT TO GIVING CARD
                    }

                }

                DutchAccepted = false;
                PhilipsesAccepted = false;
                MunseeAccepted = false;
                SixNationsAccepted = false;
            }

        }
    }

    [PunRPC]
    void cardSwitchTeams(PhotonMessageInfo info) // TODO
    {
        // Switching things for every team do ASAP
        theSender = info.Sender.ToString();
        if (theSender == PhotonNetwork.LocalPlayer.ToString())
        {
            Debug.Log("how many times did this run");
            // Team who's turn it is recieves their items
            if (DutchAccepted && turn == 1)
            {
                // Dutch inventory + Trade Receiving Cards
                int c = 0;

                while (tradeReceivingCardsParent[0].transform.childCount != c && c < 500)
                {

                    Debug.Log(tradeReceivingCardsParent[0].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            DutchAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                DutchAccepted = false;
                DutchTrading = false;
            }

            else if (PhilipsesAccepted && turn == 2)
            {
                // Philipses inventory + Trade Receiving Cards

                int c = 0;

                while (tradeReceivingCardsParent[1].transform.childCount != c && c < 500)
                {

                    Debug.Log(tradeReceivingCardsParent[1].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[1].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            PhilipsesAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                PhilipsesAccepted = false;
                PhilipsesTrading = false;

            }
            else if (SixNationsAccepted && turn == 3)
            {
                // Six Nations inventory + Trade Receiving Cards
                int c = 0;

                while (tradeReceivingCardsParent[2].transform.childCount != c && c < 500)
                {

                    Debug.Log(tradeReceivingCardsParent[2].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[2].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            SixNationsAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                SixNationsAccepted = false;
                SixNationsTrading = false;
            }
            else if (MunseeAccepted && turn == 4)
            {
                // Munsee inventory + Trade Receiving Cards
                int c = 0;

                while (tradeReceivingCardsParent[3].transform.childCount != c && c < 500)
                {

                    Debug.Log(tradeReceivingCardsParent[3].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[3].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            MunseeAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                MunseeAccepted = false;
                MunseeTrading = false;
            }

            // Team who accepted the trade recieves their items

            if (DutchAccepted)
            {
                // Dutch inventory + Trade Receiving Cards - Trade Giving Cards
                int c = 0;

                while (tradeReceivingCardsParent[0].transform.childCount != c && c < 500)
                {
                    Debug.Log(tradeReceivingCardsParent[0].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            DutchAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[0].transform.childCount != c && tradeGivingCardsParent[0].transform.childCount > c && c < 500)
                {
                    Debug.Log(c);
                    Debug.Log(tradeGivingCardsParent[0].transform.GetChild(c));
                    string cardTag = tradeGivingCardsParent[0].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            DutchAmounts[childIndex]--;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                DutchAccepted = false;
                DutchTrading = false;
            }
            else if (PhilipsesAccepted)
            {
                // Philipses inventory + Trade Receiving Cards - Trade Giving Cards
                int c = 0;

                while (tradeReceivingCardsParent[1].transform.childCount != c && c < 500)
                {
                    Debug.Log(tradeReceivingCardsParent[1].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[1].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            PhilipsesAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[1].transform.childCount != c && tradeGivingCardsParent[1].transform.childCount > c && c < 500)
                {
                    Debug.Log(c);
                    Debug.Log(tradeGivingCardsParent[1].transform.GetChild(c));
                    string cardTag = tradeGivingCardsParent[1].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            PhilipsesAmounts[childIndex]--;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                PhilipsesAccepted = false;
                PhilipsesTrading = false;
            }
            else if (SixNationsAccepted)
            {
                // Six Nations inventory + Trade Receiving Cards - Trade Giving Cards
                int c = 0;

                while (tradeReceivingCardsParent[2].transform.childCount != c && c < 500)
                {
                    Debug.Log(tradeReceivingCardsParent[2].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[2].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            SixNationsAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[2].transform.childCount != c && tradeGivingCardsParent[2].transform.childCount > c && c < 500)
                {
                    Debug.Log(c);
                    Debug.Log(tradeGivingCardsParent[2].transform.GetChild(c));
                    string cardTag = tradeGivingCardsParent[2].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            SixNationsAmounts[childIndex]--;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                SixNationsAccepted = false;
                SixNationsTrading = false;
            }
            else if (MunseeAccepted)
            {
                // Munsee inventory + Trade Receiving Cards - Trade Giving Cards
                int c = 0;

                while (tradeReceivingCardsParent[3].transform.childCount != c && c < 500)
                {
                    Debug.Log(tradeReceivingCardsParent[3].transform.GetChild(c));
                    string cardTag = tradeReceivingCardsParent[3].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            MunseeAmounts[childIndex]++;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[3].transform.childCount != c && tradeGivingCardsParent[3].transform.childCount > c && c < 500)
                {
                    Debug.Log(c);
                    Debug.Log(tradeGivingCardsParent[3].transform.GetChild(c));
                    string cardTag = tradeGivingCardsParent[3].transform.GetChild(c).gameObject.tag;
                    GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                    for (int d = 0; d < cardAmountObjects.Length; d++)
                    {
                        if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                        {
                            Debug.Log(cardAmountObjects[d]);
                            int childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                            MunseeAmounts[childIndex]--;
                            cardAmountObjects[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex].ToString() + "x";
                        }
                    }
                    c++;
                }
                MunseeAccepted = false;
                MunseeTrading = false;
            }

            Debug.Log("Clearing trades on: " + PhotonNetwork.LocalPlayer.ToString());
            this.GetComponent<PhotonView>().RPC("clearAllTrades", RpcTarget.All);

            // Check if anyone has won
            for (int i = 13; i < DutchAmounts.Length; i++)
            {
                if (DutchAmounts[i] != 0)
                {
                    break;
                }
                else
                {
                    if(i == DutchAmounts.Length - 1)
                    {
                        this.GetComponent<PhotonView>().RPC("moveToCalculationScene", RpcTarget.All);
                        doNotDoAnything = true;
                    }
                }
            }
            Debug.Log("did i even run vro");
            for (int i = 13; i < PhilipsesAmounts.Length; i++)
            {
                Debug.Log(i + " | Amount:" + PhilipsesAmounts[i]);
                if (PhilipsesAmounts[i] != 0)
                {
                    break;
                }
                else
                {
                    if (i == PhilipsesAmounts.Length - 1)
                    {
                        this.GetComponent<PhotonView>().RPC("moveToCalculationScene", RpcTarget.All);
                        doNotDoAnything = true;
                    }
                }
            }

            for (int i = 13; i < SixNationsAmounts.Length; i++)
            {
                if (SixNationsAmounts[i] != 0)
                {
                    break;
                }
                else
                {
                    if (i == SixNationsAmounts.Length - 1)
                    {
                        this.GetComponent<PhotonView>().RPC("moveToCalculationScene", RpcTarget.All);
                        doNotDoAnything = true;
                    }
                }
            }

            for (int i = 13; i < MunseeAmounts.Length; i++)
            {
                if (MunseeAmounts[i] != 0)
                {
                    break;
                }
                else
                {
                    if (i == MunseeAmounts.Length - 1)
                    {
                        this.GetComponent<PhotonView>().RPC("moveToCalculationScene", RpcTarget.All);
                        doNotDoAnything = true;
                    }
                }
            }
            if(doNotDoAnything == false)
            {
                
                Debug.Log("Moving turns on: " + PhotonNetwork.LocalPlayer.ToString());
                this.GetComponent<PhotonView>().RPC("MoveTurns", RpcTarget.All);
            }
            
        }
        


    }

    [PunRPC]
    void moveToCalculationScene(PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(2);
            SceneManager.LoadScene(2);
        }
        Debug.Log("Moved Scenes");
        if (SceneManager.GetActiveScene().name == "Final_Wampum_Value")
        {
            Debug.Log("Inside if");
            GameObject.FindGameObjectWithTag("DutchWampumText").gameObject.GetComponent<Text>().text = "Wampum: " + DutchWampum.ToString();
            GameObject.FindGameObjectWithTag("PhilipsesWampumText").gameObject.GetComponent<Text>().text = "Wampum: " + PhilipsesWampum.ToString();
            GameObject.FindGameObjectWithTag("SixNationsWampumText").gameObject.GetComponent<Text>().text = "Wampum: " + SixNationsWampum.ToString();
            GameObject.FindGameObjectWithTag("MunseeWampumText").gameObject.GetComponent<Text>().text = "Wampum: " + MunseeWampum.ToString();
        }
    }

    [PunRPC]
    void moveToEndScene(PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(3);
            SceneManager.LoadScene(3);
        }
    }
    public int ad = 0;
    public string trader = "";
    // FIX
    [PunRPC]
    void clearAllTrades(PhotonMessageInfo info)
    {
        Debug.Log("HELLO?");
        Debug.Log(info.Sender.ToString() + " RUN 1 " + PhotonNetwork.LocalPlayer.ToString() + " ");
        Debug.Log(info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString());
        Debug.Log(totalTurnNumber);
        if (DutchWampumValuesTrades.Count <= 0)
        {
            DutchWampumValuesTrades.Add(0);
        }
        else if (PhilipsesWampumValuesTrades.Count <= 0)
        {
            PhilipsesWampumValuesTrades.Add(0);
        }
        else if (SixNationsWampumValuesTrades.Count <= 0)
        {
            SixNationsWampumValuesTrades.Add(0);
        }
        else if (MunseeWampumValuesTrades.Count <= 0)
        {
            MunseeWampumValuesTrades.Add(0);
        }

        if (info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString()) // Bring back / revert theSender == info.Sender.ToString() &&  if neccessary
        {
            // Note; int b is utilized in case of crashing aswell. It will not run over 1000 iterations

            Debug.Log("Maybe Giving got called multiple times? The Sender is: " + info.Sender.ToString());
            for (int ae = 0; ae < tradeGivingCardsParent.Length; ae++) // For every Trade giving card object, set inactive    AE DESCRIBES THE PARENTS FOR EACH TEAM
            {
                Debug.Log("RUN 3");
                int b = 0;
                while (tradeGivingCardsParent[ae].transform.childCount != b && b < 1000)
                {
                    if (tradeGivingCardsParent[ae].transform.GetChild(b).gameObject.activeSelf == true) // If the card is activated
                    {
                        Debug.Log("Details about object: " + tradeGivingCardsParent[ae] + " " + tradeGivingCardsParent[ae].transform.GetChild(b) + " " + tradeGivingCardsParent[ae].transform.GetChild(b).gameObject);
                        
                        for (int ad = 0; ad < tags.Length; ad++)
                        {
                            if (clearTradeButton && tradeGivingCardsParent[ae].transform.GetChild(b).gameObject.tag == tags[ad])
                            {
                                Debug.Log("Running Giving Card Parent Adding back. Adding to value: " + (ad).ToString() + "");
                                if (turn == 1 && tradeGivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Dutch")
                                {
                                    DutchAmounts[ad] += 1;
                                    DutchAmountsGameObjects[ad].GetComponent<Text>().text = DutchAmounts[ad].ToString() + "x";
                                }
                                else if (turn == 2 && tradeGivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Philipses")
                                {
                                    PhilipsesAmounts[ad] += 1;
                                    PhilipsesAmountsGameObjects[ad].GetComponent<Text>().text = PhilipsesAmounts[ad].ToString() + "x";

                                }
                                else if (turn == 3 && tradeGivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Six Nations")
                                {
                                    SixNationsAmounts[ad] += 1;
                                    SixNationsAmountsGameObjects[ad].GetComponent<Text>().text = SixNationsAmounts[ad].ToString() + "x";

                                }
                                else if (turn == 4 && tradeGivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Munsee")
                                {
                                    MunseeAmounts[ad] += 1;
                                    MunseeAmountsGameObjects[ad].GetComponent<Text>().text = MunseeAmounts[ad].ToString() + "x";

                                }

                            }
                        }
                        tradeGivingCardsParent[ae].transform.GetChild(b).gameObject.SetActive(false);



                        
                    }
                    b++;

                }




                b = 0;
                Debug.Log("Maybe Receiving got called multiple times? The Sender is: " + info.Sender.ToString());
                while (tradeReceivingCardsParent[ae].transform.childCount != b && b < 1000) // For every Trade receiving card object, set inactive
                {
                    
                    if (tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.activeSelf == true) // If the card is activated
                    {
                        if (ae == (turn - 1))
                        {

                            for (int ad = 0; ad < tags.Length; ad++)
                            {
                                if (tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.tag == tags[ad] && !clearTradeButton) // FOR WAMPUM CALCULATION CHANGE THIS #WAMPUM
                                {
                                    if (turn == 1)
                                    {
                                        trader = Dutch;
                                    }
                                    else if (turn == 2)
                                    {
                                        trader = Philipses;
                                    }
                                    else if (turn == 3)
                                    {
                                        trader = SixNations;
                                    }
                                    else if (turn == 4)
                                    {
                                        trader = Munsee;
                                    }

                                    Debug.Log("We got here, " + PhotonNetwork.LocalPlayer.ToString());
                                    this.GetComponent<PhotonView>().RPC("addWampumValues", RpcTarget.All);
                                }
                                else if (tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.tag == tags[ad] && clearTradeButton) // Add back values to wishlist
                                {
                                    Debug.Log("Running Receiving Card Parent Adding back. Adding to value: " + (ad + 13).ToString() + "");
                                    if (turn == 1 && tradeReceivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Dutch")
                                    {
                                        DutchAmounts[ad + 13] += 1;
                                        DutchAmountsGameObjects[ad + 13].GetComponent<Text>().text = DutchAmounts[ad + 13].ToString() + "x";


                                    }
                                    else if ((turn == 2 && tradeReceivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Philipses"))
                                    {
                                        PhilipsesAmounts[ad + 13] += 1;
                                        PhilipsesAmountsGameObjects[ad + 13].GetComponent<Text>().text = PhilipsesAmounts[ad + 13].ToString() + "x";


                                    }
                                    else if (turn == 3 && tradeReceivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Six Nations")
                                    {
                                        SixNationsAmounts[ad + 13] += 1;
                                        SixNationsAmountsGameObjects[ad + 13].GetComponent<Text>().text = SixNationsAmounts[ad + 13].ToString() + "x";


                                    }
                                    else if (turn == 4 && tradeReceivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Munsee")
                                    {
                                        MunseeAmounts[ad + 13] += 1;
                                        MunseeAmountsGameObjects[ad + 13].GetComponent<Text>().text = MunseeAmounts[ad + 13].ToString() + "x";


                                    }
                                }
                                Debug.Log(tags[ad] + " " + tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.tag + " ae: " + ae.ToString());
                                Debug.Log(tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.tag == tags[ad]);
                            }
                        }

                        tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.SetActive(false);
                    }
                    
                    b++;
                }


                // DESTROYING METHOD (not working)

                /*
                 * 
                 * int j = 0;
                 * while(tradeGivingCardsParent[ae].transform.childCount > 0 && j < 150)
                {
                    PhotonView.Destroy(tradeGivingCardsParent[ae].transform.GetChild(0).gameObject);
                    j++;
                    Debug.Log(tradeGivingCardsParent[ae].transform.childCount);
                    Debug.Log(tradeGivingCardsParent[ae].transform.GetChild(0).gameObject);
                }
                j = 0;
                while (tradeReceivingCardsParent[ae].transform.childCount > 0 && j < 150)
                {
                    PhotonView.Destroy(tradeReceivingCardsParent[ae].transform.GetChild(0).gameObject);
                    j++;
                }*/
            }
        }

        if (clearTradeButton)
        {
            Debug.Log("ClearTradeButton Clicked, reactivating team flags & removing all trading");
            ReactivateTeamFlags();
            DutchTrading = false;
            PhilipsesTrading = false;
            SixNationsTrading = false;
            MunseeTrading = false;
            clearTradeButton = false;

            for (int i = 0; i < DutchTradeButton.Length; i++)
            {
                DutchTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
            }

            for (int i = 0; i < PhilipsesTradeButton.Length; i++)
            {
                PhilipsesTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
            }


            for (int i = 0; i < MunseeTradeButton.Length; i++)
            {
                MunseeTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
            }
            for (int i = 0; i < SixNationsTradeButton.Length; i++)
            {
                SixNationsTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
            }



        }
        

    }

    [PunRPC]
    void addWampumValues(PhotonMessageInfo info)
    {

        Debug.Log("We got here, RPC: " + trader);
        if (trader == Dutch)
        {
            DutchWampum += DutchWampumValues[ad];
            //DutchWampumValuesTrades[(totalTurnNumber) / 4] += DutchWampumValues[ad];
            Debug.Log("Added to Dutch Wampum by: " + DutchWampumValues[ad] + ", for a total of: " + DutchWampum);
        }
        else if (trader == Philipses)
        {
            PhilipsesWampum += PhilipsesWampumValues[ad];
            //PhilipsesWampumValuesTrades[((totalTurnNumber) / 4) - 1] += PhilipsesWampumValues[ad];
            Debug.Log("Added to Philipses Wampum by: " + PhilipsesWampumValues[ad] + ", for a total of: " + PhilipsesWampum);
        }
        else if (trader == SixNations)
        {
            SixNationsWampum += SixNationsWampumValues[ad];
            //SixNationsWampumValuesTrades[((totalTurnNumber) / 4) - 2] += SixNationsWampumValues[ad];
            Debug.Log("Added to SixNations Wampum by: " + SixNationsWampumValues[ad] + ", for a total of: " + SixNationsWampum);
        }
        else if (trader == Munsee)
        {
            MunseeWampum += MunseeWampumValues[ad];
            //MunseeWampumValuesTrades[((totalTurnNumber) / 4) - 3] += MunseeWampumValues[ad];
            Debug.Log("Added to Munsee Wampum by: " + MunseeWampumValues[ad] + ", for a total of: " + MunseeWampum);
        }
    }










    [PunRPC]
    void MoveTurns(PhotonMessageInfo info)
    {
        DeactivateTeamFlags();
        Debug.Log("This was sent by: " + info.Sender.ToString() + "; This is running on " + PhotonNetwork.LocalPlayer.ToString() + "; theSender: " + theSender);
        
        if (info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString()) // theSender == info.Sender.ToString() && 
        {

            totalTurnNumber++;
            Debug.Log("It is now moving turns");
            if (turn == 4)
            {
                turn = 1;
                Debug.Log("Turn is 4, making turn 1");
            }
            else
            {
                turn++;
                Debug.Log("turn++");
            }
        //}
            Debug.Log("Turn: " + turn + "Total turn number: " + totalTurnNumber);
            for (int k = 0; k < SeasonalTimers.Length; k++)
            {
                SeasonalTimers[k].GetComponent<Text>().text = "Year: " + (totalTurnNumber + 1600).ToString() + " | Turn: " + teamNames[(turn - 1)].ToString();
            }

            numberOfAcceptedTeams = 0;
            DutchAccepted = false;
            SixNationsAccepted = false;
            MunseeAccepted = false;
            PhilipsesAccepted = false;

            if (!DutchTrading)
            {
                for (int i = 0; i < DutchTradeButton.Length; i++)
                {
                     DutchTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }

            }
            if (!PhilipsesTrading)
            {
                for (int i = 0; i < PhilipsesTradeButton.Length; i++)
                {
                    PhilipsesTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
            }
            if (!MunseeTrading)
            {
                for (int i = 0; i < MunseeTradeButton.Length; i++)
                {
                    MunseeTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
            }
            if (!SixNationsTrading)
            {
                for (int i = 0; i < SixNationsTradeButton.Length; i++)
                {
                    SixNationsTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
            }
        }
        DutchObject.SetActive(false);
        DutchObject.SetActive(true);
        MunseeObject.SetActive(false);
        MunseeObject.SetActive(true);
        Debug.Log("did");

        InventoryCardsInTrade = 0;
        WishlistCardsInTrade = 0;
        
        /*PhilipsesTradingButtonOnClick philipsesTradingButtonOnClick = new PhilipsesTradingButtonOnClick();
        philipsesTradingButtonOnClick.GetComponent<PhotonView>().RPC("greyOutButtons", RpcTarget.All);*/

    }
    
        
   



    [PunRPC]
    void MoveTurns1(PhotonMessageInfo info)
    {
        DeactivateTeamFlags();
        Debug.Log("This was sent by: " + info.Sender.ToString() + "; This is running on " + PhotonNetwork.LocalPlayer.ToString() + "; theSender: " + theSender);

        if (info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString()) // theSender == info.Sender.ToString() && 
        {

            totalTurnNumber++;
            Debug.Log("It is now moving turns");
            if (turn == 4)
            {
                turn = 1;
                Debug.Log("Turn is 4, making turn 1");
            }
            else
            {
                turn++;
                Debug.Log("turn++");
            }
            //}
            Debug.Log("Turn: " + turn + "Total turn number: " + totalTurnNumber);
            for (int k = 0; k < SeasonalTimers.Length; k++)
            {
                SeasonalTimers[k].GetComponent<Text>().text = "Year: " + (totalTurnNumber + 1600).ToString() + " | Turn: " + teamNames[(turn - 1)].ToString();
            }

            numberOfAcceptedTeams = 0;
            DutchAccepted = false;
            SixNationsAccepted = false;
            MunseeAccepted = false;
            PhilipsesAccepted = false;

            if (!DutchTrading)
            {
                for (int i = 0; i < DutchTradeButton.Length; i++)
                {
                    DutchTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }

            }
            if (!PhilipsesTrading)
            {
                for (int i = 0; i < PhilipsesTradeButton.Length; i++)
                {
                    PhilipsesTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
            }
            if (!MunseeTrading)
            {
                for (int i = 0; i < MunseeTradeButton.Length; i++)
                {
                    MunseeTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
            }
            if (!SixNationsTrading)
            {
                for (int i = 0; i < SixNationsTradeButton.Length; i++)
                {
                    SixNationsTradeButton[i].GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
            }
        }
        DutchObject.SetActive(false);
        DutchObject.SetActive(true);
        MunseeObject.SetActive(false);
        MunseeObject.SetActive(true);
        Debug.Log("did");

        InventoryCardsInTrade = 0;
        WishlistCardsInTrade = 0;

        /*PhilipsesTradingButtonOnClick philipsesTradingButtonOnClick = new PhilipsesTradingButtonOnClick();
        philipsesTradingButtonOnClick.GetComponent<PhotonView>().RPC("greyOutButtons", RpcTarget.All);*/

    }




}