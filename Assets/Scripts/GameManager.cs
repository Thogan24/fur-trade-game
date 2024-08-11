using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{


    // If the game manager is reset for any reason, set Camera Prefabs, Card Prefabs, 
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

    // Prefab Gameobject of Camera SET IN INSPECTOR
    public GameObject DutchCameraGameObject;
    public GameObject SixNationsCameraGameObject;
    public GameObject MunseeCameraGameObject;
    public GameObject PhilipsesCameraGameObject;

    // Prefab PhotonView of Camera SET IN INSPECTOR
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
    public GameObject DutchSecondBackgroundCanvasObject;
    public GameObject DutchCardsCanvasObject;
    public GameObject SixNationsTextCanvasObject;
    public GameObject SixNationsBackgroundCanvasObject;
    public GameObject SixNationsSecondBackgroundCanvasObject;
    public GameObject SixNationsCardsCanvasObject;
    public GameObject MunseeTextCanvasObject;
    public GameObject MunseeBackgroundCanvasObject;
    public GameObject MunseeSecondBackgroundCanvasObject;
    public GameObject MunseeCardsCanvasObject;
    public GameObject PhilipsesTextCanvasObject;
    public GameObject PhilipsesBackgroundCanvasObject;
    public GameObject PhilipsesSecondBackgroundCanvasObject;
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
    public bool alreadyRanCalculation = false;


    // TRADING VARIABLES


    

    public string theSender = "";
    public float distanceFromLineGiving = -10f;
    public float distanceFromLineReceiving = 6.08f;
    public float ZAxisLineDistance = 0;
    public float YAxisLineDistance = 0.10f;


    public bool clearTradeButton = false;

    public GameObject[] cardAmountObjects;
    public GameObject[] cardAmountObjects2;

    public int time;
    public int turnTimeLength = 5000;
    public int turnTimeLengthFirstIteration = 5000;
    public GameObject countdownTextObject;
    public Text countdownTimerText;
    [HideInInspector] public bool isTimeFinished = true;
    public bool TurnTimerRanOut = false;


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
    public int[] SixNationsAmountsStarting = { 12, 5, 6, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 9, 4, 6, 3, 3, 20, 2 };
    public int[] MunseeAmountsStarting = { 10, 6, 2, 5, 13, 6, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 6, 4, 4, 10, 4, 12, 6 };
    public int[] PhilipsesAmountsStarting = { 0, 0, 0, 0, 0, 0, 3, 8, 10, 4, 2, 3, 5,/**/ 10, 7, 4, 6, 4, 6, 0, 0, 0, 0, 0, 0, 0 };
    public int[] DutchAmountsStarting = { 0, 0, 0, 0, 0, 0, 12, 0, 0, 9, 5, 20, 3,/**/ 12, 4, 4, 5, 10, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int InventoryCardsInTrade = 0;
    public int WishlistCardsInTrade = 0;

    public int[] SixNationsAmounts = {12, 5, 6, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 9, 4, 6, 3, 3, 20, 2};
    public int[] MunseeAmounts =     {10, 6, 2, 5, 13, 6, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 6, 4, 4, 10, 4, 12, 6};
    public int[] PhilipsesAmounts =  {0, 0, 0, 0, 0, 0, 3, 8, 10, 4, 2, 3, 5,/**/ 10, 7, 4, 6, 4, 6, 0, 0, 0, 0, 0, 0, 0};
    public int[] DutchAmounts =      {0, 0, 0, 0, 0, 0, 12, 0, 0, 9, 5, 20, 3,/**/ 12, 4, 4, 5, 10, 0, 0, 0, 0, 0, 0, 0, 0};

    public int[] SixNationsAmountsSubtractedDuringTrade = { 12, 5, 6, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 9, 4, 6, 3, 3, 20, 2 };
    public int[] MunseeAmountsSubtractedDuringTrade = { 10, 6, 2, 5, 13, 6, 0, 0, 0, 0, 0, 0, 0,/**/ 0, 0, 0, 0, 0, 0, 6, 4, 4, 10, 4, 12, 6 };
    public int[] PhilipsesAmountsSubtractedDuringTrade = { 0, 0, 0, 0, 0, 0, 3, 8, 10, 4, 2, 3, 5,/**/ 10, 7, 4, 6, 4, 6, 0, 0, 0, 0, 0, 0, 0 };
    public int[] DutchAmountsSubtractedDuringTrade = { 0, 0, 0, 0, 0, 0, 12, 0, 0, 9, 5, 20, 3,/**/ 12, 4, 4, 5, 10, 0, 0, 0, 0, 0, 0, 0, 0 };

    // used for debugging
    public int[] allAmountsSummed = { 22, 11, 8, 9, 16, 6, 15, 8, 10, 13, 7, 23, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    // NEEDS TO BE CHANGED IN FINAL GAME
    public int[] PointMultiplier = { 9, 8, 5, 4, 2, 1, 9, 7, 7, 6, 3, 1, 1};



    public List<int> SixNationsWampumValuesTrades = new List<int>();
    public List<int> MunseeWampumValuesTrades = new List<int>();
    public List<int> PhilipsesWampumValuesTrades = new List<int>();
    public List<int> DutchWampumValuesTrades = new List<int>();

    public int SixNationsPoints;
    public int MunseePoints;
    public int PhilipsesPoints;
    public int DutchPoints;

    public GameObject[] SixNationsAmountsGameObjects = { };
    public GameObject[] MunseeAmountsGameObjects = { };
    public GameObject[] PhilipsesAmountsGameObjects = { };
    public GameObject[] DutchAmountsGameObjects = { };
    public GameObject[] Prefabs;
    public string[] tags = { }; // LENGTH 13


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
    public bool countDownFinished = false;


    public bool dutchMovedTurns = false;
    public bool philipsesMovedTurns = false;
    public bool sixNationsMovedTurns = false;
    public bool munseeMovedTurns = false;

    public int cardsPerLine = 12;
    /*
     Turn numbers:
     Dutch - 1
     Philipses - 2
     Six Nations - 3
     Munsee - 4
     */

    public string[] imageDescriptionTags = { "DutchDescription", "PhilipsesDescription", "SixNationsDescription", "MunseeDescription", "BeaverDescription", "DeerSkinDescription", "BearDescription", "FisherDescription", "FoxDescription", "SchepelsDescription", "DuffelsDescription", "LinenDescription", "StockingsDescription", "StroudsDescription", "AxesDescription", "BeadsDescription", "ScissorsDescription" };
    public bool sceneChange = false;
    public bool opened = false;


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

    public PhotonView test;
    void Update()
    {
        
        // Main Scene
        //Debug.Log(DutchJoined);

        if ((DutchJoined && !AlreadyLoaded && SixNationsJoined && MunseeJoined && PhilipsesJoined) || (DebugStart == true && DutchJoined && !AlreadyLoaded))
        {
            Debug.Log("Teans joined, loading main screen");
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Loading level on the MASTER client...");
                PhotonNetwork.LoadLevel(2);
                SceneManager.LoadScene(2);

                //test = Instantiate(MunseeCameraGameObject, new Vector3(2.0f, 0, 0), Quaternion.identity);
            }
            this.GetComponent<PhotonView>().RPC("mainSceneCameraRPC", RpcTarget.All);
/*            if (!AlreadyLoaded)
            {*/
            this.GetComponent<PhotonView>().RPC("mainSceneSetInventoryAmountsRPC", RpcTarget.All);
            DeactivateAllOtherButtons();
            DeactivateTeamFlags();
            StartCountDown();
            AlreadyLoaded = true;
            /*}*/

        }


        if (SceneManager.GetActiveScene().name == "Main_Scene" && sceneChange == false)
        {
            Debug.Log("Ran the thing");
            sceneChange = true;
            for (int j = 4; j < imageDescriptionTags.Length; j++)
            {
                GameObject[] startArray = GameObject.FindGameObjectsWithTag(imageDescriptionTags[j]);
                for (int b = 0; b < startArray.Length; b++)
                {
                    startArray[b].gameObject.transform.position = startArray[b].gameObject.transform.position - new Vector3(100, 0, 0);
                }

            }
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




        //aggregiously bad code (its in update)
        if (SceneManager.GetActiveScene().name == "Final_Wampum_Value")
        {
            Debug.Log("Inside if");
            GameObject.FindGameObjectWithTag("DutchWampumText").gameObject.GetComponent<Text>().text = "Dutch Points: " + DutchPoints.ToString();
            GameObject.FindGameObjectWithTag("PhilipsesWampumText").gameObject.GetComponent<Text>().text = "Philipses Points: " + PhilipsesPoints.ToString();
            GameObject.FindGameObjectWithTag("SixNationsWampumText").gameObject.GetComponent<Text>().text = "Six Nations Points: " + SixNationsPoints.ToString();
            GameObject.FindGameObjectWithTag("MunseeWampumText").gameObject.GetComponent<Text>().text = "Munsee Points: " + MunseePoints.ToString();
            if(alreadyRanCalculation == false)
            {
                Debug.Log("Calculating");
                if (alreadyRanCalculation == false)
                {
                    for (int wa = 0; wa < 13; wa++)
                    {

                        if (wa < 5 && DutchAmounts[wa] <= DutchAmountsStarting[wa + 13])
                        {
                            Debug.Log("Dutch: " + tags[wa] + " adding " + DutchAmounts[wa] * PointMultiplier[wa] + " for a total of " + DutchPoints);
                            DutchPoints += DutchAmounts[wa] * PointMultiplier[wa];

                        }
                        else if (wa < 5)
                        {
                            Debug.Log("Dutch: " + tags[wa] + "Max adding " + DutchAmounts[wa] * PointMultiplier[wa] + " for a total of " + DutchPoints);
                            DutchPoints += DutchAmountsStarting[wa + 13] * PointMultiplier[wa];
                        }

                        if (wa <= 5 && PhilipsesAmounts[wa] <= PhilipsesAmountsStarting[wa + 13])
                        {
                            Debug.Log("Philipses: " + tags[wa] + " adding " + PhilipsesAmounts[wa] * PointMultiplier[wa] + " for a total of " + PhilipsesPoints);
                            PhilipsesPoints += PhilipsesAmounts[wa] * PointMultiplier[wa];
                        }
                        else if (wa < 5)
                        {
                            PhilipsesPoints += PhilipsesAmountsStarting[wa + 13] * PointMultiplier[wa];
                            Debug.Log("Philipses: " + tags[wa] + "Max adding " + PhilipsesAmounts[wa] * PointMultiplier[wa] + " for a total of " + PhilipsesPoints);
                        }

                        if (wa > 5 && SixNationsAmounts[wa] <= SixNationsAmountsStarting[wa + 13])
                        {
                            Debug.Log("DutSixNationsch: " + tags[wa] + " adding " + SixNationsAmounts[wa] * PointMultiplier[wa] + " for a total of " + SixNationsPoints);
                            SixNationsPoints += SixNationsAmounts[wa] * PointMultiplier[wa];
                        }
                        else if (wa > 5)
                        {
                            SixNationsPoints += SixNationsAmountsStarting[wa + 13] * PointMultiplier[wa];
                            Debug.Log("SixNations: " + tags[wa] + "Max adding " + SixNationsAmounts[wa] * PointMultiplier[wa] + " for a total of " + SixNationsPoints);
                        }

                        if (wa > 5 && MunseeAmounts[wa] <= MunseeAmountsStarting[wa + 13])
                        {
                            Debug.Log("Munsee: " + tags[wa] + " adding " + MunseeAmounts[wa] * PointMultiplier[wa] + " for a total of " + MunseePoints);
                            MunseePoints += MunseeAmounts[wa] * PointMultiplier[wa];
                        }
                        else if (wa > 5)
                        {
                            MunseePoints += MunseeAmountsStarting[wa + 13] * PointMultiplier[wa];
                            Debug.Log("Munsee: " + tags[wa] + "Max adding " + MunseeAmounts[wa] * PointMultiplier[wa] + " for a total of " + MunseePoints);


                        }

                    }
                    if (DutchPoints >= PhilipsesPoints && DutchPoints >= SixNationsPoints && DutchPoints >= MunseePoints)
                    {
                        GameObject.FindGameObjectWithTag("DutchCrown").SetActive(true);
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("DutchCrown").SetActive(false);
                    }
                    if (PhilipsesPoints >= DutchPoints && PhilipsesPoints >= SixNationsPoints && PhilipsesPoints >= MunseePoints)
                    {
                        GameObject.FindGameObjectWithTag("PhilipsesCrown").SetActive(true);
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("PhilipsesCrown").SetActive(false);
                    }
                    if (SixNationsPoints >= PhilipsesPoints && SixNationsPoints >= DutchPoints && SixNationsPoints >= MunseePoints)
                    {
                        GameObject.FindGameObjectWithTag("SixNationsCrown").SetActive(true);
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("SixNationsCrown").SetActive(false);
                    }
                    if (MunseePoints >= PhilipsesPoints && MunseePoints >= SixNationsPoints && MunseePoints >= DutchPoints)
                    {
                        GameObject.FindGameObjectWithTag("MunseeCrown").SetActive(true);
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("MunseeCrown").SetActive(false);
                    }
                    alreadyRanCalculation = true;




                }
            }
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
/*        try
        {*/
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
            DutchSecondBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Dutch Second Background Canvas");
            DutchCamera.transform.parent = DutchTextCanvasObject.transform.parent;
            DutchTextCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            DutchBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            DutchCardsCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            DutchSecondBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = DutchCamera.gameObject.GetComponent<Camera>();
            //DutchCamera.SetActive(true);
        }
        GameObject[] SixNationsCamerasCheckArray = GameObject.FindGameObjectsWithTag("Six Nations Camera");
        if (PhotonNetwork.LocalPlayer.ToString() == SixNations && AlreadyLoaded == false && SixNationsCamerasCheckArray.Length <= 1)
        {
            SixNationsCameraPrefab = SixNationsCameraGameObject.GetPhotonView();
            SixNationsCamera = PhotonView.Instantiate(SixNationsCameraPrefab);
            SixNationsTextCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Text Canvas");
            SixNationsBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Background Canvas");
            SixNationsSecondBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Second Background Canvas");
            SixNationsCamera.transform.parent = SixNationsTextCanvasObject.transform.parent;
            SixNationsTextCanvasObject.GetComponent<Canvas>().worldCamera = SixNationsCamera.gameObject.GetComponent<Camera>();
            SixNationsBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = SixNationsCamera.gameObject.GetComponent<Camera>();
            SixNationsCardsCanvasObject.GetComponent<Canvas>().worldCamera = SixNationsCamera.gameObject.GetComponent<Camera>();
            SixNationsSecondBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = SixNationsCamera.gameObject.GetComponent<Camera>();

        }
        GameObject[] MunseeCamerasCheckArray = GameObject.FindGameObjectsWithTag("Munsee Camera");
        if (PhotonNetwork.LocalPlayer.ToString() == Munsee && AlreadyLoaded == false && MunseeCamerasCheckArray.Length <= 1)
        {
            Debug.Log("Ran1");
            MunseeCameraPrefab = MunseeCameraGameObject.GetPhotonView();
            MunseeCamera = PhotonView.Instantiate(MunseeCameraPrefab);
            MunseeTextCanvasObject = GameObject.FindGameObjectWithTag("Munsee Text Canvas");
            MunseeBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Munsee Background Canvas");
            MunseeSecondBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Munsee Second Background Canvas");
            MunseeCamera.transform.parent = MunseeTextCanvasObject.transform.parent;
            MunseeTextCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
            MunseeBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
            MunseeCardsCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
            MunseeSecondBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
        }
        GameObject[] PhilipsesCamerasCheckArray = GameObject.FindGameObjectsWithTag("Philipse Camera");
        if (PhotonNetwork.LocalPlayer.ToString() == Philipses && AlreadyLoaded == false && PhilipsesCamerasCheckArray.Length <= 1)
        {
            PhilipsesCameraPrefab = PhilipsesCameraGameObject.GetPhotonView();
            PhilipsesCamera = PhotonView.Instantiate(PhilipsesCameraPrefab);
            PhilipsesTextCanvasObject = GameObject.FindGameObjectWithTag("Philipses Text Canvas");
            PhilipsesBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Philipses Background Canvas");
            PhilipsesSecondBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Philipses Second Background Canvas");
            PhilipsesCamera.transform.parent = PhilipsesTextCanvasObject.transform.parent;
            PhilipsesTextCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
            PhilipsesBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
            PhilipsesCardsCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
            PhilipsesSecondBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
        }


        // Arrays of trade buttons
        DutchTradeButton = GameObject.FindGameObjectsWithTag("Dutch Trading");
        SixNationsTradeButton = GameObject.FindGameObjectsWithTag("Six Nations Trading");
        MunseeTradeButton = GameObject.FindGameObjectsWithTag("Munsee Trading");
        PhilipsesTradeButton = GameObject.FindGameObjectsWithTag("Philipses Trading");

        GameObject[] scrollViews = GameObject.FindGameObjectsWithTag("Scroll View");
        for (int an = 0; an < scrollViews.Length; an++)
        {
            if(PhotonNetwork.LocalPlayer.ToString() == Dutch && scrollViews[an].transform.parent.parent.parent.tag != "Dutch")
            {
                scrollViews[an].SetActive(false);
            }
            else if (PhotonNetwork.LocalPlayer.ToString() == Philipses && scrollViews[an].transform.parent.parent.parent.tag != "Philipses")
            {
                scrollViews[an].SetActive(false);
            }
            else if (PhotonNetwork.LocalPlayer.ToString() == SixNations && scrollViews[an].transform.parent.parent.parent.tag != "Six Nations")
            {
                scrollViews[an].SetActive(false);
            }
            else if (PhotonNetwork.LocalPlayer.ToString() == Munsee && scrollViews[an].transform.parent.parent.parent.tag != "Munsee")
            {
                scrollViews[an].SetActive(false);
            }
        }
            for (int aa = 0; aa < DutchTradeButton.Length; aa++)
            {
                Debug.Log("Dutch Trade Button sub " + aa + ": " + DutchTradeButton[aa]);

                if ((PhotonNetwork.LocalPlayer.ToString() == Dutch && turn == 1 && DutchTradeButton[aa].transform.parent.parent.tag != "Dutch") || (PhotonNetwork.LocalPlayer.ToString() == Philipses && turn == 2 && DutchTradeButton[aa].transform.parent.parent.tag != "Philipses") || (PhotonNetwork.LocalPlayer.ToString() == SixNations && turn == 3 && DutchTradeButton[aa].transform.parent.parent.tag != "Six Nations") || (PhotonNetwork.LocalPlayer.ToString() == Munsee && turn == 2 && DutchTradeButton[aa].transform.parent.parent.tag != "Munsee"))
                {
                    Debug.Log("sending dutch trade buttons off into space");
                    DutchTradeButton[aa].gameObject.transform.position = new Vector3(1000, 1000, 1000);

                }
                if ((PhotonNetwork.LocalPlayer.ToString() == Dutch && turn == 1 && PhilipsesTradeButton[aa].transform.parent.parent.tag != "Dutch") || (PhotonNetwork.LocalPlayer.ToString() == Philipses && turn == 2 && PhilipsesTradeButton[aa].transform.parent.parent.tag != "Philipses") || (PhotonNetwork.LocalPlayer.ToString() == SixNations && turn == 3 && PhilipsesTradeButton[aa].transform.parent.parent.tag != "Six Nations") || (PhotonNetwork.LocalPlayer.ToString() == Munsee && turn == 2 && PhilipsesTradeButton[aa].transform.parent.parent.tag != "Munsee"))
                {
                    Debug.Log("sending philipses trade buttons off into space");
                    PhilipsesTradeButton[aa].gameObject.transform.position = new Vector3(1000, 1000, 1000);

                }
                if ((PhotonNetwork.LocalPlayer.ToString() == Dutch && turn == 1 && SixNationsTradeButton[aa].transform.parent.parent.tag != "Dutch") || (PhotonNetwork.LocalPlayer.ToString() == Philipses && turn == 2 && SixNationsTradeButton[aa].transform.parent.parent.tag != "Philipses") || (PhotonNetwork.LocalPlayer.ToString() == SixNations && turn == 3 && SixNationsTradeButton[aa].transform.parent.parent.tag != "Six Nations") || (PhotonNetwork.LocalPlayer.ToString() == Munsee && turn == 2 && SixNationsTradeButton[aa].transform.parent.parent.tag != "Munsee"))
            {
                    Debug.Log("sending SixNationsTradeButton trade buttons off into space");
                    SixNationsTradeButton[aa].gameObject.transform.position = new Vector3(1000, 1000, 1000);

                }
                if ((PhotonNetwork.LocalPlayer.ToString() == Dutch && turn == 1 && MunseeTradeButton[aa].transform.parent.parent.tag != "Dutch") || (PhotonNetwork.LocalPlayer.ToString() == Philipses && turn == 2 && MunseeTradeButton[aa].transform.parent.parent.tag != "Philipses") || (PhotonNetwork.LocalPlayer.ToString() == SixNations && turn == 3 && MunseeTradeButton[aa].transform.parent.parent.tag != "Six Nations") || (PhotonNetwork.LocalPlayer.ToString() == Munsee && turn == 2 && MunseeTradeButton[aa].transform.parent.parent.tag != "Munsee"))
            {
                    Debug.Log("sending MunseeTradeButton trade buttons off into space");
                    MunseeTradeButton[aa].gameObject.transform.position = new Vector3(1000, 1000, 1000);

                }
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
            for (int a = 0; a < PhilipsesCamerasCheckArray.Length; a++)
            {
                if (PhilipsesTextCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>() || PhilipsesBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>() || PhilipsesCardsCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>())
                {
                    PhilipsesCamerasCheckArray[a].SetActive(false);
                }
                else
                {
                    PhilipsesCamerasCheckArray[a].SetActive(true);
                }
            }
/*        }
        catch (NullReferenceException exception)
        {
            Debug.Log("Failed... Exception caught");
            Debug.LogException(exception, this);
            //AlreadyLoaded = true;

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
                        Debug.Log(AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name);
                        if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Six Nations")
                        {
                            Debug.Log("It found the Six Nations Amounts one");
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
                        PhilipsesAmountsGameObjects[i] = null;
                    }
                    if (SixNationsAmounts[i] == 0)
                    {
                        SixNationsAmountsGameObjects[i] = null;
                    }
                    if (MunseeAmounts[i] == 0)
                    {
                        MunseeAmountsGameObjects[i] = null;
                    }
                    
                    
                    AmountsGameObjectsWithTag = GameObject.FindGameObjectsWithTag(tags[i - 13] + " Amount Wishlist");
                    for (int j = 0; j < AmountsGameObjectsWithTag.Length; j++)
                    {
                        if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Dutch")
                        {
                            DutchAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            DutchAmountsGameObjects[i].GetComponent<Text>().text = DutchAmounts[i].ToString() + "/" + DutchAmountsStarting[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Philipses")
                        {
                            PhilipsesAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            PhilipsesAmountsGameObjects[i].GetComponent<Text>().text = PhilipsesAmounts[i].ToString() + "/" + PhilipsesAmountsStarting[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Six Nations")
                        {
                            SixNationsAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            SixNationsAmountsGameObjects[i].GetComponent<Text>().text = SixNationsAmounts[i].ToString() + "/" + SixNationsAmountsStarting[i].ToString() + "x";
                        }
                        else if (AmountsGameObjectsWithTag[j].transform.parent.transform.parent.transform.parent.name == "Munsee")
                        {
                            MunseeAmountsGameObjects[i] = AmountsGameObjectsWithTag[j];
                            MunseeAmountsGameObjects[i].GetComponent<Text>().text = MunseeAmounts[i].ToString() + "/" + MunseeAmountsStarting[i].ToString() + "x";
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
            SeasonalTimers[k].GetComponent<Text>().text = "Year: " + (totalTurnNumber + 1600).ToString() + " \nTurn: " + teamNames[turn - 1].ToString();
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




    IEnumerator redCardAnimation(GameObject card)
    {


        card.GetComponent<Image>().color = Color.HSVToRGB(0f, 0.72f, 0.61f);
        yield return new WaitForSeconds(1);
        card.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);

        yield return null;

    }

    [PunRPC]
    public string findTeamBeingTradedWith()
    {
        if(DutchTrading == true && turn != 1)
        {
            return Dutch;
        }
        else if (PhilipsesTrading == true && turn != 2)
        {
            return Philipses;
        }
        else if (SixNationsTrading == true && turn != 3)
        {
            return SixNations;
        }
        else if (MunseeTrading == true && turn != 4)
        {
            return Munsee;
        }
        Debug.LogError("Cannot find team that is being traded with");
        return null;
    }

    [PunRPC]
    public void findTeamBeingTradedWithAndSubtractFromInventory(int index)
    {
        if (DutchTrading == true && turn != 1)
        {
            DutchAmountsSubtractedDuringTrade[index]--;
        }
        else if (PhilipsesTrading == true && turn != 2)
        {
            PhilipsesAmountsSubtractedDuringTrade[index]--;
        }
        else if (SixNationsTrading == true && turn != 3)
        {
            SixNationsAmountsSubtractedDuringTrade[index]--;
        }
        else if (MunseeTrading == true && turn != 4)
        {
            MunseeAmountsSubtractedDuringTrade[index]--;
        }
        else
        {
            Debug.LogError("Cannot find team that is being traded with");
        }
        
    }

    [PunRPC]
    public bool findifTeamBeingTradedWithHasEnoughCards(int index)
    {
        if (DutchTrading == true && turn != 1)
        {
            Debug.Log("Dutch has: " + DutchAmountsSubtractedDuringTrade[index] + " of the card at " + index);
            return DutchAmountsSubtractedDuringTrade[index] > 0;
        }
        else if (PhilipsesTrading == true && turn != 2)
        {
            Debug.Log("Philipses has: " + PhilipsesAmountsSubtractedDuringTrade[index] + " of the card at " + index);
            return PhilipsesAmountsSubtractedDuringTrade[index] > 0;
        }
        else if (SixNationsTrading == true && turn != 3)
        {
            Debug.Log("Six Nations has: " + SixNationsAmountsSubtractedDuringTrade[index] + " of the card at " + index);
            return SixNationsAmountsSubtractedDuringTrade[index] > 0;
        }
        else if (MunseeTrading == true && turn != 4)
        {
            Debug.Log("Munsee has: " + MunseeAmountsSubtractedDuringTrade[index] + " of the card at " + index);
            return MunseeAmountsSubtractedDuringTrade[index] > 0;
        }
        Debug.LogError("Cannot find team that is being traded with");
        return false;
    }





    [PunRPC]
    void addCardToTrade(string tag, string parentTag, bool leftClicked, PhotonMessageInfo info)
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
                
                //NOTE: ENEMYTEAMBUTTONPOS IS NO LONGER USED, THIS CODE IS ONLY HERE FOR THE ADDTORECEIVING!!


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
                            //addToReceiving[3] = true;
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




            else if (playerString == Philipses && PhilipsesTrading == true)
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

            else if (playerString == SixNations && SixNationsTrading == true)
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

            else if (playerString == Munsee && MunseeTrading == true)
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


            GameObject[] instantiatePos = GameObject.FindGameObjectsWithTag("InstantiatePos");
            

            Vector3[] topButtonPos = {new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
            GameObject[] sixNationsButtons = GameObject.FindGameObjectsWithTag("InstantiatePos"); //Six Nations Trading
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
            GameObject[] philipsesButtons = GameObject.FindGameObjectsWithTag("InstantiatePos"); // Philipses Trading
            for (int ab = 0; ab < philipsesButtons.Length; ab++)
            {
                if (philipsesButtons[ab].transform.parent.parent.name == "Six Nations")
                {
                    topButtonPos[2] = philipsesButtons[ab].transform.position;
                }

            }

            Vector3[] topButtonPosWishlist = { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
            GameObject[] InstantiatePosWishlist = GameObject.FindGameObjectsWithTag("InstantiatePosReceiving"); //Six Nations Trading
            for (int ab = 0; ab < sixNationsButtons.Length; ab++)
            {
                if (InstantiatePosWishlist[ab].transform.parent.parent.name == "Dutch")
                {
                    topButtonPosWishlist[0] = InstantiatePosWishlist[ab].transform.position;
                }
                else if (InstantiatePosWishlist[ab].transform.parent.parent.name == "Philipses")
                {
                    topButtonPosWishlist[1] = InstantiatePosWishlist[ab].transform.position;
                }
                else if (InstantiatePosWishlist[ab].transform.parent.parent.name == "Munsee")
                {
                    topButtonPosWishlist[3] = InstantiatePosWishlist[ab].transform.position;
                }
                if (InstantiatePosWishlist[ab].transform.parent.parent.name == "Six Nations")
                {
                    topButtonPosWishlist[2] = InstantiatePosWishlist[ab].transform.position;
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
                    Debug.Log("Checking at iteration" + z + "; Tag:" + tag + "; tags[z]" + tags[z] + "; DutchAmounts[z]" + DutchAmounts[z]);
                    if (tag == tags[z])
                    {
                        Debug.Log("The Card being traded has a tag of: " + tag + "; It has an amount of " + DutchAmounts[z]);
                    }

                    //Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((DutchAmounts[z] > 0 && isParentInventory == 1) || (isParentWishlist == 1 && findifTeamBeingTradedWithHasEnoughCards(z)))) // If the card amounts are greater than zero or is in their wishlist
                    {
                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % cardsPerLine), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    Debug.Log("addToReceiving card has been instantiated");
                                }
                                else
                                { // Shouldn't do anything if we get rid of the isparentwishlist side
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % cardsPerLine))), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    Debug.Log("regular card has been instantiated");
                                }

                            }
                            Debug.Log(DutchAmounts[z]);
                            DutchAmounts[z]--;
                            DutchAmountsGameObjects[z].GetComponent<Text>().text = DutchAmounts[z].ToString() + "x";

                            DutchAmounts[z + 13]++;
                            if (DutchAmountsStarting[z + 13] > 0)
                            {
                                DutchAmountsGameObjects[z + 13].GetComponent<Text>().text = DutchAmounts[z + 13].ToString() + "/" + DutchAmountsStarting[z + 13].ToString() + "x";

                                if (DutchAmounts[z + 13] < 0)
                                {
                                    DutchAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[z + 13]).ToString();
                                }


                            }
                        }
                        else //isWishlist
                        {
                            Debug.Log(DutchAmounts[z + 13]);
                            if (leftClicked)
                            {
                                Debug.Log("IN LEFT CLICKED");
                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    Debug.Log("Adding the card to each players screen");
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3(distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine)), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                        Debug.Log("addToReceiving card has been instantiated");
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3((distanceFromLineReceiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                        Debug.Log("regular card has been instantiated");
                                    }

                                }
                                DutchAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);
                                if (DutchAmountsStarting[z + 13] > 0)
                                {
                                    DutchAmountsGameObjects[z + 13].GetComponent<Text>().text = DutchAmounts[z + 13].ToString() + "/" + DutchAmountsStarting[z + 13].ToString() + "x";

                                    if (DutchAmounts[z + 13] < 0)
                                    {
                                        DutchAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[z + 13]).ToString();
                                    }


                                }
                                break;
                            }
                            else if (DutchAmountsGameObjects[z + 13] != null) // Clicking on wishlist cards (potentially remove)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3(distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine)), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                        Debug.Log("addToReceiving card has been instantiated");
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3((distanceFromLineReceiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                        Debug.Log("regular card has been instantiated");
                                    }

                                }
                                DutchAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);
                                DutchAmountsGameObjects[z + 13].GetComponent<Text>().text = DutchAmounts[z + 13].ToString() + "/" + DutchAmountsStarting[z + 13].ToString() + "x";

                                if (DutchAmounts[z + 13] < 0)
                                {
                                    DutchAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[z + 13]).ToString();
                                }

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                                for (int ah = 0; ah < cardsWithTag.Length; ah++)
                                {
                                    try
                                    {
                                        Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                        if (cardsWithTag[ah].transform.parent.parent.parent.name == "Dutch" && cardsWithTag[ah].transform.parent.tag == "Wishlist")
                                        {
                                            Debug.Log("Team does not have enough cards, starting animation for Dutch");
                                            StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                            return;
                                        }
                                    }
                                    catch (NullReferenceException ex)
                                    {
                                        Debug.Log("Card came back with null parents");
                                        cardsWithTag[ah].gameObject.SetActive(false);
                                    }

                                }
                                return;
                            }
                        }


                        break;
                    }

                    else if(tag == tags[z])
                    {
                        Debug.LogError("None of specified card left");
                        GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                        for (int ah = 0; ah < cardsWithTag.Length; ah++)
                        {
                            try
                            {
                                Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                Debug.Log(cardsWithTag[ah].transform.parent.parent.parent.name == "Dutch");
                                if (cardsWithTag[ah].transform.parent.parent.parent.name != null && cardsWithTag[ah].transform.parent.parent.parent.name == "Dutch" && ((((cardsWithTag[ah].transform.parent.tag == "Wishlist" || leftClicked) && isParentWishlist == 1)) || (isParentInventory == 1 && cardsWithTag[ah].transform.parent.tag == "Inventory")))
                                {
                                    Debug.Log("Team does not have enough cards, starting animation for Dutch");
                                    StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                    return;
                                }

                            }
                            catch (NullReferenceException ex)
                            {
                                Debug.Log("Card came back with null parents");
                                cardsWithTag[ah].gameObject.SetActive(false);
                            }
                            if (ah == cardsWithTag.Length - 1)
                            {
                                Debug.Log("Not correct team");
                                return;
                            }

                        }

                    }
                    else
                    {
                        Debug.Log("Not correct iteration?");
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
                    //Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((PhilipsesAmounts[z] > 0 && isParentInventory == 1) || (isParentWishlist == 1 && findifTeamBeingTradedWithHasEnoughCards(z)))) // If the card amounts are greater than zero or is in their wishlist
                    {

                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % cardsPerLine), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                }
                                else
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % cardsPerLine))), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                }

                            }
                            Debug.Log(PhilipsesAmounts[z]);
                            PhilipsesAmounts[z]--;
                            PhilipsesAmountsGameObjects[z].GetComponent<Text>().text = PhilipsesAmounts[z].ToString() + "x";

                            PhilipsesAmounts[z + 13]++;
                            if (PhilipsesAmountsStarting[z + 13] > 0)
                            {
                                PhilipsesAmountsGameObjects[z + 13].GetComponent<Text>().text = PhilipsesAmounts[z + 13].ToString() + "/" + PhilipsesAmountsStarting[z + 13].ToString() + "x";

                                if (PhilipsesAmounts[z + 13] < 0)
                                {
                                    PhilipsesAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[z + 13]).ToString();
                                }


                            }
                        }
                        else
                        {
                            Debug.Log(PhilipsesAmounts[z + 13]);
                            if (leftClicked)
                            {
                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % cardsPerLine), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                PhilipsesAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);

                                if(PhilipsesAmountsStarting[z+13] > 0)
                                {
                                    PhilipsesAmountsGameObjects[z + 13].GetComponent<Text>().text = PhilipsesAmounts[z + 13].ToString() + "/" + PhilipsesAmountsStarting[z + 13].ToString() + "x";

                                    if (PhilipsesAmounts[z + 13] < 0)
                                    {
                                        PhilipsesAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[z + 13]).ToString();
                                    }

                                    
                                }
                                break;
                                
                            }
                            else if (PhilipsesAmountsGameObjects[z + 13] != null)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % cardsPerLine), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                PhilipsesAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);
                                PhilipsesAmountsGameObjects[z + 13].GetComponent<Text>().text = PhilipsesAmounts[z + 13].ToString() + "/" + PhilipsesAmountsStarting[z+13].ToString() + "x";

                                if (PhilipsesAmounts[z + 13] < 0)
                                {
                                    PhilipsesAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[z + 13]).ToString();
                                }

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                                for (int ah = 0; ah < cardsWithTag.Length; ah++)
                                {
                                    try
                                    {
                                        Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                        if (cardsWithTag[ah].transform.parent.parent.parent.name == "Philipses" && cardsWithTag[ah].transform.parent.tag == "Wishlist")
                                        {
                                            Debug.Log("Team does not have enough cards, starting animation for Philipses");
                                            StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                            return;
                                        }
                                    }
                                    catch (NullReferenceException ex)
                                    {
                                        Debug.Log("Card came back with null parents");
                                        cardsWithTag[ah].gameObject.SetActive(false);
                                    }

                                }
                                return;
                            }
                        }
                        //


                        break;
                    }
/*                    else if (tag == tags[z] && (isParentWishlist == 1 && !findifTeamBeingTradedWithHasEnoughCards(z)))
                    {
                        GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                        for (int ah = 0; ah < cardsWithTag.Length; ah++)
                        {
                            try
                            {
                                Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                if (cardsWithTag[ah].transform.parent.parent.parent.name != null && cardsWithTag[ah].transform.parent.parent.parent.name == "Philipses" && cardsWithTag[ah].transform.parent.tag == "Wishlist")
                                {
                                    Debug.Log("Team does not have enough cards, starting animation");
                                    StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                    return;
                                }
                            }
                            catch (NullReferenceException ex)
                            {
                                Debug.Log("Card came back with null parents");
                                cardsWithTag[ah].gameObject.SetActive(false);
                            }



                        }

                    }*/
                    else if (tag == tags[z])
                    {
                        Debug.LogError("None of specified card left");
                        if (z + 1 == tags.Length)
                        {
                            return;
                        }
                        GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                        for (int ah = 0; ah < cardsWithTag.Length; ah++)
                        {
                            try {
                                Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                if (cardsWithTag[ah].transform.parent.parent.parent.name != null && cardsWithTag[ah].transform.parent.parent.parent.name == "Philipses" && ((((cardsWithTag[ah].transform.parent.tag == "Wishlist" || leftClicked) && isParentWishlist == 1)) || (isParentInventory == 1 && cardsWithTag[ah].transform.parent.tag == "Inventory")))
                                {
                                    Debug.Log("Team does not have enough cards, starting animation for Philipses");
                                    StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                    return;
                                }

                            }
                            catch(NullReferenceException ex)
                            {
                                Debug.Log("Card came back with null parents");
                                cardsWithTag[ah].gameObject.SetActive(false);
                            }
                            if (ah == cardsWithTag.Length - 1)
                            {
                                Debug.Log("Not correct team");
                                return;
                            }



                        }
                    }
                    else
                    {
                        Debug.Log("Not correct iteration?");
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
                    //Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((SixNationsAmounts[z] > 0 && isParentInventory == 1) || (isParentWishlist == 1 && findifTeamBeingTradedWithHasEnoughCards(z)))) // If the card amounts are greater than zero or is in their wishlist
                    {

                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % cardsPerLine), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                }
                                else
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % cardsPerLine))), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                }

                            }
                            Debug.Log(SixNationsAmounts[z]);
                            SixNationsAmounts[z]--;
                            SixNationsAmountsGameObjects[z].GetComponent<Text>().text = SixNationsAmounts[z].ToString() + "x";

                            SixNationsAmounts[z+13]++;

                            if (SixNationsAmountsStarting[z + 13] > 0)
                            {
                                SixNationsAmountsGameObjects[z + 13].GetComponent<Text>().text = SixNationsAmounts[z + 13].ToString() + "/" + SixNationsAmountsStarting[z + 13].ToString() + "x";
                                if (SixNationsAmounts[z + 13] < 0)
                                {
                                    SixNationsAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[z + 13]).ToString();
                                }
                            }
                        }
                        else
                        {
                            Debug.Log(SixNationsAmounts[z + 13]);
                            if (leftClicked)
                            {
                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % cardsPerLine), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                SixNationsAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);

                                if (SixNationsAmountsStarting[z+13] > 0){
                                    SixNationsAmountsGameObjects[z + 13].GetComponent<Text>().text = SixNationsAmounts[z + 13].ToString() + "/" + SixNationsAmountsStarting[z + 13].ToString() + "x";
                                    if (SixNationsAmounts[z + 13] < 0)
                                    {
                                        SixNationsAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[z + 13]).ToString();
                                    }
                                }
                                
                                break;
                            }
                            else if (SixNationsAmountsGameObjects[z + 13] != null)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % cardsPerLine), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                SixNationsAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);
                                SixNationsAmountsGameObjects[z + 13].GetComponent<Text>().text = SixNationsAmounts[z + 13].ToString() + "/" + SixNationsAmountsStarting[z+13].ToString() + "x";

                                if (SixNationsAmounts[z + 13] < 0)
                                {
                                    SixNationsAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[z + 13]).ToString();
                                }

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                                for (int ah = 0; ah < cardsWithTag.Length; ah++)
                                {
                                    try
                                    {
                                        Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                        if (cardsWithTag[ah].transform.parent.parent.parent.name == "Six Nations" && cardsWithTag[ah].transform.parent.tag == "Wishlist")
                                        {
                                            Debug.Log("Team does not have enough cards, starting animation for Six Nations");
                                            StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                            return;
                                        }

                                    }
                                    catch (NullReferenceException ex)
                                    {
                                        Debug.Log("Card came back with null parents");
                                        cardsWithTag[ah].gameObject.SetActive(false);
                                    }

                                }
                                return;
                            }
                        }
                        //


                        break;
                    }
                    else if (tag == tags[z])
                    {
                        Debug.LogError("None of specified card left");
                        if (z + 1 == tags.Length)
                        {
                            return;
                        }
                        GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                        for (int ah = 0; ah < cardsWithTag.Length; ah++)
                        {
                            try
                            {
                                Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                if (cardsWithTag[ah].transform.parent.parent.parent.name != null && cardsWithTag[ah].transform.parent.parent.parent.name == "Six Nations" && ((((cardsWithTag[ah].transform.parent.tag == "Wishlist" || leftClicked) && isParentWishlist == 1)) || (isParentInventory == 1 && cardsWithTag[ah].transform.parent.tag == "Inventory")))
                                {
                                    Debug.Log("Team does not have enough cards, starting animation for Six Nations");
                                    StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                    return;
                                }
                            }
                            catch (NullReferenceException ex)
                            {
                                Debug.Log("Card came back with null parents");
                                cardsWithTag[ah].gameObject.SetActive(false);
                            }
                            if (ah == cardsWithTag.Length - 1)
                            {
                                Debug.Log("Not correct team");
                                return;
                            }



                        }
                    }
                    else
                    {
                        Debug.Log("Not correct iteration?");
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
                    //Debug.Log(tag + " " + tags[z]);
                    if (tag == tags[z] && ((MunseeAmounts[z] > 0 && isParentInventory == 1) || (isParentWishlist == 1 && findifTeamBeingTradedWithHasEnoughCards(z)))) // If the card amounts are greater than zero or is in their wishlist
                    {

                        if (isParentInventory == 1)
                        {
                            for (int b = 0; b < topButtonPos.Length; b++)
                            {
                                
                                if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[b] + new Vector3(distanceFromLineReceiving + (float)0.3 * (InventoryCardsInTrade % cardsPerLine), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                }
                                else
                                {
                                    instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[b] + new Vector3((distanceFromLineGiving + ((float)0.3 * (InventoryCardsInTrade % cardsPerLine))), YAxisLineDistance - (InventoryCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                }

                            }
                            Debug.Log(MunseeAmounts[z]);
                            MunseeAmounts[z]--;
                            MunseeAmountsGameObjects[z].GetComponent<Text>().text = MunseeAmounts[z].ToString() + "x";

                            MunseeAmounts[z+13]++;
                            if (MunseeAmountsStarting[z + 13] > 0)
                            {
                                MunseeAmountsGameObjects[z + 13].GetComponent<Text>().text = MunseeAmounts[z + 13].ToString() + "/" + MunseeAmountsStarting[z + 13].ToString() + "x";
                                if (MunseeAmounts[z + 13] < 0)
                                {
                                    MunseeAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[z + 13]).ToString();
                                }
                            }
                        }
                        else
                        {
                            Debug.Log(MunseeAmounts[z + 13]);
                            if (leftClicked)
                            {
                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % cardsPerLine), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                MunseeAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);
                                if (MunseeAmountsStarting[z + 13] > 0)
                                {
                                    MunseeAmountsGameObjects[z + 13].GetComponent<Text>().text = MunseeAmounts[z + 13].ToString() + "/" + MunseeAmountsStarting[z + 13].ToString() + "x";
                                    if (MunseeAmounts[z + 13] < 0)
                                    {
                                        MunseeAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[z + 13]).ToString();
                                    }
                                }
                                break;
                            }
                            else if (MunseeAmountsGameObjects[z + 13] != null)
                            {

                                for (int j = 0; j < enemyTeamButtonPos.Length; j++)
                                {
                                    if (addToReceiving[j] == true) // Pretend that it is a isParentInventory side, yet counts the number of WishlistCardsInTrade
                                    {
                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPos[j] + new Vector3((distanceFromLineGiving + ((float)0.3 * (WishlistCardsInTrade % cardsPerLine))), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }
                                    else
                                    {

                                        instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), topButtonPosWishlist[j] + new Vector3(distanceFromLineReceiving + (float)0.3 * (WishlistCardsInTrade % cardsPerLine), YAxisLineDistance - (WishlistCardsInTrade / cardsPerLine), ZAxisLineDistance), Quaternion.identity);
                                    }

                                }
                                MunseeAmounts[z + 13]--;
                                findTeamBeingTradedWithAndSubtractFromInventory(z);
                                MunseeAmountsGameObjects[z + 13].GetComponent<Text>().text = MunseeAmounts[z + 13].ToString() + "/" + MunseeAmountsStarting[z+13].ToString() + "x";

                                if (MunseeAmounts[z + 13] < 0)
                                {
                                    MunseeAmountsGameObjects[z + 13].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[z + 13]).ToString();
                                }

                                break;
                            }
                            else
                            {
                                Debug.LogError("None of specified card left");
                                GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                                for (int ah = 0; ah < cardsWithTag.Length; ah++)
                                {
                                    try
                                    {
                                        Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                        if (cardsWithTag[ah].transform.parent.parent.parent.name == "Munsee" && cardsWithTag[ah].transform.parent.tag == "Wishlist")
                                        {
                                            Debug.Log("Team does not have enough cards, starting animation for Munsee");
                                            StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                            return;
                                        }
                                    }
                                    catch (NullReferenceException ex)
                                    {
                                        Debug.Log("Card came back with null parents");
                                        cardsWithTag[ah].gameObject.SetActive(false);
                                    }

                                }
                                return;
                            }
                        }
                        //


                        break;
                    }
                    else if (tag == tags[z])
                    {
                        Debug.LogError("None of specified card left");
                        if (z + 1 == tags.Length)
                        {
                            return;
                        }
                        GameObject[] cardsWithTag = GameObject.FindGameObjectsWithTag(tag);

                        for (int ah = 0; ah < cardsWithTag.Length; ah++)
                        {
                            try
                            {
                                Debug.Log("ah: " + ah + " " + cardsWithTag[ah].transform.parent.parent.parent.name + " " + cardsWithTag[ah].transform.parent.tag);
                                if (cardsWithTag[ah].transform.parent.parent.parent.name != null && cardsWithTag[ah].transform.parent.parent.parent.name == "Munsee" && ((((cardsWithTag[ah].transform.parent.tag == "Wishlist" || leftClicked) && isParentWishlist == 1)) || (isParentInventory == 1 && cardsWithTag[ah].transform.parent.tag == "Inventory")))
                                {
                                    Debug.Log("Team does not have enough cards, starting animation for Munsee");
                                    StartCoroutine(redCardAnimation(cardsWithTag[ah]));
                                    return;
                                }

                            }
                            catch (NullReferenceException ex)
                            {
                                Debug.Log("Card came back with null parents");
                                cardsWithTag[ah].gameObject.SetActive(false);
                            }

                            if (ah == cardsWithTag.Length - 1)
                            {
                                Debug.Log("Not correct team");
                                return;
                            }



                        }
                    }
                    else
                    {
                        Debug.Log("Not correct iteration?");
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
                Debug.Log("Card Instantiated at: " + instantiatedCard[j].transform.position);

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
                numberOfAcceptedTeams = 0;
            }

        }
    }

    [PunRPC]
    void cardSwitchTeams(PhotonMessageInfo info) // TODO
    {
        // This part does not have two while loops because it only needs to add the cards it is receiving since it has already 

        //Explanation: For the team who set up the trade, receive their items by going through all the items in receivingcardparent[team] that are ACTIVE and gets the tag of them. Then find the team by going up by parents.
        // The cardAmountObjects is all the Amount objects with the tag + amount, or all the teams' amount game object of the card. Then a for loop is used to get the Dutch team's amount game object where it then sets the text of the object.
        // Then adds that value to the respective team's amount of FindsGameObjectsWithTag of that tag + amount.

        // CHILD INDEX: If a fox is 5th in the hierarchy for the amounts, the childindex is 5 

        Debug.Log("cardSwitchTeams was called on THIS device");
        
        theSender = info.Sender.ToString();
        Debug.Log(theSender + " | " + PhotonNetwork.LocalPlayer.ToString());
        if (theSender == PhotonNetwork.LocalPlayer.ToString())
        {
            int childIndex = 0;
            Debug.Log("how many times did this run");
            // Team who's turn it is recieves their items
            if (DutchAccepted && turn == 1)
            {
                // Dutch inventory + Trade Receiving Cards
                int c = 0;

                while (tradeReceivingCardsParent[0].transform.childCount != c && c < 500)
                {
                    Debug.Log("Is the " + c + "iteration in " + tradeReceivingCardsParent[0] + " active: " + tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.active);
                    //Debug.Log("activeSelf: Is the " + c + "iteration in " + tradeReceivingCardsParent[0] + " active: " + tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.activeSelf);
                    if (tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log(tradeReceivingCardsParent[0].transform.GetChild(c));
                        string cardTag = tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.tag;
                        Debug.Log("cardTag: " + cardTag + "; looking for an amount of this");
                        
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects[d] + " + cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                            {
                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                DutchAmounts[childIndex]++;
                                //DutchAmounts[childIndex + 13]--;
                                //Debug.Log("New Dutch Amounts Wishlist: " + DutchAmounts[childIndex + 13]);
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex].ToString() + "x";

                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                            {
                                Debug.Log("Changing team Dutch. Wishlist (should be of dutch) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex + 13].ToString() + "/" + DutchAmountsStarting[childIndex + 13].ToString() + "x";

                            }
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
                    if (tradeReceivingCardsParent[1].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log(tradeReceivingCardsParent[1].transform.GetChild(c));
                        
                        string cardTag = tradeReceivingCardsParent[1].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                            {
                                Debug.Log(cardAmountObjects[d]);

                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                PhilipsesAmounts[childIndex]++;
                                //PhilipsesAmounts[childIndex + 13]--;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex].ToString() + "x";

                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                            {
                                Debug.Log("Changing team Philipses. Wishlist (should be of philipses) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex + 13].ToString() + "/" + PhilipsesAmountsStarting[childIndex + 13].ToString() + "x";

                            }
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
                    if (tradeReceivingCardsParent[2].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log(tradeReceivingCardsParent[2].transform.GetChild(c));
                        
                        string cardTag = tradeReceivingCardsParent[2].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                            {

                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                SixNationsAmounts[childIndex]++;
                                //SixNationsAmounts[childIndex + 13]--;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex].ToString() + "x";


                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                            {
                                Debug.Log("Changing team Six Nations. Wishlist (should be of Six Nations) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex + 13].ToString() + "/" + SixNationsAmountsStarting[childIndex + 13].ToString() + "x";

                            }
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
                    if (tradeReceivingCardsParent[3].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log(tradeReceivingCardsParent[3].transform.GetChild(c));
                        
                        string cardTag = tradeReceivingCardsParent[3].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                            {

                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                MunseeAmounts[childIndex]++;
                                //MunseeAmounts[childIndex + 13]--;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex].ToString() + "x";


                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                            {
                                Debug.Log("Changing team Munsee. Wishlist (should be of munsee) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex + 13].ToString() + "/" + MunseeAmountsStarting[childIndex + 13].ToString() + "x";

                            }
                        }
                    }
                    c++;
                }
                MunseeAccepted = false;
                MunseeTrading = false;
            }








            //Explanation: For the team accepting the trade, receive their items by going through all the items in receivingcardparent[team] / givingcardparent[team] that are ACTIVE and gets the tag of them. Then find the team by going up by parents.
            // The cardAmountObjects is all the Amount objects with the tag + amount, or all the teams' amount game object of the card. Then a for loop is used to get the Dutch team's amount game object where it then sets the text of the object. Then adds that value to the respective team's amount of FindsGameObjectsWithTag of that tag + amount.

            if (DutchAccepted)
            {
                // Dutch inventory + Trade Receiving Cards - Trade Giving Cards
                int c = 0;

                while (tradeReceivingCardsParent[0].transform.childCount != c && c < 5000)
                {
                    Debug.Log("Is the " + c + "iteration in " + tradeReceivingCardsParent[0] + " active: " + tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.active);
                    Debug.Log("activeSelf: Is the " + c + "iteration in " + tradeReceivingCardsParent[0] + " active: " + tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.activeSelf);
                    if (tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log("Card is: " + tradeReceivingCardsParent[0].transform.GetChild(c) + " at index " + c);
                        string cardTag = tradeReceivingCardsParent[0].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                            {

                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                DutchAmounts[childIndex]++;
                                DutchAmounts[childIndex + 13]--;
                                
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex].ToString() + "x";


                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                            {
                                Debug.Log("Changing team Dutch. Wishlist (should be of dutch) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex + 13].ToString() + "/" + DutchAmountsStarting[childIndex + 13].ToString() + "x";

                            }
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[0].transform.childCount != c && tradeGivingCardsParent[0].transform.childCount > c && c < 5000)
                {
                    if (tradeGivingCardsParent[0].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log(c);
                        Debug.Log("Card is: " + tradeGivingCardsParent[0].transform.GetChild(c) + " at index " + c);
                        string cardTag = tradeGivingCardsParent[0].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Dutch")
                            {
                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                DutchAmounts[childIndex]--;
                                DutchAmounts[childIndex+13]++;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = DutchAmounts[childIndex].ToString() + "x";

                                if (DutchAmountsGameObjects[childIndex + 13] != null)
                                {
                                    DutchAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = DutchAmounts[childIndex + 13].ToString() + "/" + DutchAmountsStarting[childIndex + 13].ToString() + "x";

                                    if (DutchAmounts[childIndex + 13] < 0)
                                    {
                                        DutchAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[childIndex + 13]).ToString();
                                    }
                                }
                            }
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

                while (tradeReceivingCardsParent[1].transform.childCount != c && c < 5000)
                {
                    if (tradeReceivingCardsParent[1].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log("Card is: " + tradeReceivingCardsParent[1].transform.GetChild(c) + " at index " + c);

                        string cardTag = tradeReceivingCardsParent[1].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                            {
                                Debug.Log(cardAmountObjects[d]);
                                Debug.Log("iteration: " + d);


                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                PhilipsesAmounts[childIndex]++;
                                PhilipsesAmounts[childIndex + 13]--;
                                Debug.Log("Their wishlisted item is going down as they are the ones accepting the trade: " + PhilipsesAmounts[childIndex + 13]);
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex].ToString() + "x";

                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                            {
                                Debug.Log("Changing team Philipses. Wishlist (should be of philipses) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex + 13].ToString() + "/" + PhilipsesAmountsStarting[childIndex + 13].ToString() + "x";

                            }
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[1].transform.childCount != c && tradeGivingCardsParent[1].transform.childCount > c && c < 5000)
                {
                    if (tradeGivingCardsParent[1].transform.GetChild(c).gameObject.active == true)
                    {

                        Debug.Log(c);
                        Debug.Log("Card is: " + tradeGivingCardsParent[1].transform.GetChild(c) + " at index " + c);
                        string cardTag = tradeGivingCardsParent[1].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Philipses")
                            {
                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                PhilipsesAmounts[childIndex]--;
                                PhilipsesAmounts[childIndex + 13]++;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = PhilipsesAmounts[childIndex].ToString() + "x";

                                if (PhilipsesAmountsGameObjects[childIndex + 13] != null)
                                {
                                    PhilipsesAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = PhilipsesAmounts[childIndex + 13].ToString() + "/" + PhilipsesAmountsStarting[childIndex + 13].ToString() + "x";

                                    if (PhilipsesAmounts[childIndex + 13] < 0)
                                    {
                                        PhilipsesAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[childIndex + 13]).ToString();
                                    }
                                }

                            }
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

                while (tradeReceivingCardsParent[2].transform.childCount != c && c < 5000)
                {
                    if (tradeReceivingCardsParent[2].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log("Card is: " + tradeReceivingCardsParent[2].transform.GetChild(c) + " at index " + c);
                        string cardTag = tradeReceivingCardsParent[2].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                            {

                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                SixNationsAmounts[childIndex]++;
                                SixNationsAmounts[childIndex + 13]--;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex].ToString() + "x";

                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                            {
                                Debug.Log("Changing team Six Nations. Wishlist (should be of Six Nations) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex + 13].ToString() +"/" + SixNationsAmountsStarting[childIndex + 13].ToString() + "x";

                            }
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[2].transform.childCount != c && tradeGivingCardsParent[2].transform.childCount > c && c < 5000)
                {
                    if (tradeGivingCardsParent[2].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log(c);
                        Debug.Log("Card is: " + tradeGivingCardsParent[2].transform.GetChild(c) + " at index " + c);
                        string cardTag = tradeGivingCardsParent[2].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Six Nations")
                            {
                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                SixNationsAmounts[childIndex]--;
                                SixNationsAmounts[childIndex + 13]++;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = SixNationsAmounts[childIndex].ToString() + "x";

                                if (SixNationsAmountsGameObjects[childIndex + 13] != null)
                                {
                                    SixNationsAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = SixNationsAmounts[childIndex + 13].ToString() + "/" + SixNationsAmountsStarting[childIndex + 13].ToString() + "x";

                                    if (SixNationsAmounts[childIndex + 13] < 0)
                                    {
                                        SixNationsAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[childIndex + 13]).ToString();
                                    }
                                }
                            }
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

                while (tradeReceivingCardsParent[3].transform.childCount != c && c < 5000)
                {
                    if (tradeReceivingCardsParent[3].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log("Card is: " + tradeReceivingCardsParent[3].transform.GetChild(c) + " at index " + c);
                        string cardTag = tradeReceivingCardsParent[3].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        GameObject[] cardAmountObjects2 = GameObject.FindGameObjectsWithTag(cardTag + " Amount Wishlist");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                            {
                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                MunseeAmounts[childIndex]++;
                                MunseeAmounts[childIndex + 13]--;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex].ToString() + "x";

                            }
                        }
                        for (int d = 0; d < cardAmountObjects2.Length; d++)
                        {
                            Debug.Log("d = " + d + "; cardAmountObjects2[d].team = " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                            if (cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                            {
                                Debug.Log("Changing team Munsee. Wishlist (should be of munsee) is of team: " + cardAmountObjects2[d].gameObject.transform.parent.transform.parent.transform.parent.name);
                                cardAmountObjects2[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex + 13].ToString() + "/" + MunseeAmountsStarting[childIndex + 13].ToString() + "x";

                            }
                        }
                    }
                    c++;
                }
                c = 0;
                while (tradeGivingCardsParent[3].transform.childCount != c && tradeGivingCardsParent[3].transform.childCount > c && c < 5000)
                {
                    if (tradeGivingCardsParent[3].transform.GetChild(c).gameObject.active == true)
                    {
                        Debug.Log("Card is: " + tradeGivingCardsParent[3].transform.GetChild(c) + " at index " + c);
                        string cardTag = tradeGivingCardsParent[3].transform.GetChild(c).gameObject.tag;
                        GameObject[] cardAmountObjects = GameObject.FindGameObjectsWithTag(cardTag + "Amount");
                        for (int d = 0; d < cardAmountObjects.Length; d++)
                        {
                            if (cardAmountObjects[d].gameObject.transform.parent.transform.parent.transform.parent.name == "Munsee")
                            {
                                Debug.Log(cardAmountObjects[d]);
                                childIndex = cardAmountObjects[d].transform.GetSiblingIndex();
                                MunseeAmounts[childIndex]--;
                                MunseeAmounts[childIndex + 13]++;
                                cardAmountObjects[d].gameObject.GetComponent<Text>().text = MunseeAmounts[childIndex].ToString() + "x";

                                if (MunseeAmountsGameObjects[childIndex + 13] != null)
                                {
                                    MunseeAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = MunseeAmounts[childIndex + 13].ToString() + "/" + MunseeAmountsStarting[childIndex + 13].ToString() + "x";

                                    if (MunseeAmounts[childIndex + 13] < 0)
                                    {
                                        MunseeAmountsGameObjects[childIndex + 13].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[childIndex + 13]).ToString();
                                    }
                                }
                            }
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
            for (int i = 13; i < 18; i++)
            {
                if (DutchAmounts[i] > 0)
                {
                    break;
                }
                else
                {
                    if(i == 17)
                    {
                        this.GetComponent<PhotonView>().RPC("moveToCalculationScene", RpcTarget.All);
                        doNotDoAnything = true;
                    }
                }
            }
            Debug.Log("did i even run vro");
            if (doNotDoAnything == false)
            {


                for (int i = 13; i < 19; i++)
                {
                    Debug.Log(i + " | Amount:" + PhilipsesAmounts[i]);
                    if (PhilipsesAmounts[i] > 0)
                    {
                        Debug.Log("Broke at: " + i + " | Amount:" + PhilipsesAmounts[i] + " | turn: " + turn);
                        break;
                    }
                    else
                    {
                        if (i == 18)
                        {
                            this.GetComponent<PhotonView>().RPC("moveToCalculationScene", RpcTarget.All);
                            doNotDoAnything = true;
                        }
                    }
                }
                if (doNotDoAnything == false)
                {


                    for (int i = 19; i < SixNationsAmounts.Length; i++)
                    {
                        if (SixNationsAmounts[i] > 0)
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
                    if (doNotDoAnything == false)
                    {


                        for (int i = 19; i < MunseeAmounts.Length; i++)
                        {
                            if (MunseeAmounts[i] > 0)
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
                    }
                }
            }
            if(doNotDoAnything == false) // If we shouldn't be ending the game, keep moving turns
            {

                Debug.Log("time to move turns");

                if(dutchMovedTurns == false && PhotonNetwork.LocalPlayer.ToString() == Dutch || philipsesMovedTurns == false && PhotonNetwork.LocalPlayer.ToString() == Philipses || sixNationsMovedTurns == false && PhotonNetwork.LocalPlayer.ToString() == SixNations || munseeMovedTurns == false && PhotonNetwork.LocalPlayer.ToString() == Munsee)
                {
                    Debug.Log("Moving turns on: " + PhotonNetwork.LocalPlayer.ToString());
                    this.GetComponent<PhotonView>().RPC("MoveTurns", RpcTarget.All);
                }
                else
                {
                    Debug.Log(dutchMovedTurns + " " + philipsesMovedTurns + " " + sixNationsMovedTurns + " " + munseeMovedTurns + " " + PhotonNetwork.LocalPlayer.ToString());
                }
                
            }
            
        }
        


    }


    [PunRPC]
    void moveToCalculationScene(PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(3);
            SceneManager.LoadScene(3);
        }
        Debug.Log("Moved Scenes");
        if (SceneManager.GetActiveScene().name == "Final_Wampum_Value")
        {
            Debug.Log("Inside if");
            if (alreadyRanCalculation == false)
            {
                for (int wa = 0; wa < 13; wa++)
                {

                    if (wa < 5 && DutchAmounts[wa] <= DutchAmountsStarting[wa + 13])
                    {
                        Debug.Log("Dutch: " + tags[wa] + " adding " + DutchAmounts[wa] * PointMultiplier[wa] + " for a total of " + DutchPoints);
                        DutchPoints += DutchAmounts[wa] * PointMultiplier[wa];

                    }
                    else if (wa < 5)
                    {
                        Debug.Log("Dutch: " + tags[wa] + "Max adding " + DutchAmounts[wa] * PointMultiplier[wa] + " for a total of " + DutchPoints);
                        DutchPoints += DutchAmountsStarting[wa + 13] * PointMultiplier[wa];
                    }

                    if (wa <= 5 && PhilipsesAmounts[wa] <= PhilipsesAmountsStarting[wa + 13])
                    {
                        Debug.Log("Philipses: " + tags[wa] + " adding " + PhilipsesAmounts[wa] * PointMultiplier[wa] + " for a total of " + PhilipsesPoints);
                        PhilipsesPoints += PhilipsesAmounts[wa] * PointMultiplier[wa];
                    }
                    else if (wa < 5)
                    {
                        PhilipsesPoints += PhilipsesAmountsStarting[wa + 13] * PointMultiplier[wa];
                        Debug.Log("Philipses: " + tags[wa] + "Max adding " + PhilipsesAmounts[wa] * PointMultiplier[wa] + " for a total of " + PhilipsesPoints);
                    }

                    if (wa > 5 && SixNationsAmounts[wa] <= SixNationsAmountsStarting[wa + 13])
                    {
                        Debug.Log("DutSixNationsch: " + tags[wa] + " adding " + SixNationsAmounts[wa] * PointMultiplier[wa] + " for a total of " + SixNationsPoints);
                        SixNationsPoints += SixNationsAmounts[wa] * PointMultiplier[wa];
                    }
                    else if (wa > 5)
                    {
                        SixNationsPoints += SixNationsAmountsStarting[wa + 13] * PointMultiplier[wa];
                        Debug.Log("SixNations: " + tags[wa] + "Max adding " + SixNationsAmounts[wa] * PointMultiplier[wa] + " for a total of " + SixNationsPoints);
                    }

                    if (wa > 5 && MunseeAmounts[wa] <= MunseeAmountsStarting[wa + 13])
                    {
                        Debug.Log("Munsee: " + tags[wa] + " adding " + MunseeAmounts[wa] * PointMultiplier[wa] + " for a total of " + MunseePoints);
                        MunseePoints += MunseeAmounts[wa] * PointMultiplier[wa];
                    }
                    else if (wa > 5)
                    {
                        MunseePoints += MunseeAmountsStarting[wa + 13] * PointMultiplier[wa];
                        Debug.Log("Munsee: " + tags[wa] + "Max adding " + MunseeAmounts[wa] * PointMultiplier[wa] + " for a total of " + MunseePoints);

                        
                    }
                    
                }
                alreadyRanCalculation = true;




            }
            else
            {
                Debug.Log("Ran twice womp womp");
            }
            GameObject.FindGameObjectWithTag("DutchWampumText").gameObject.GetComponent<Text>().text = "Points:<br>" + DutchPoints.ToString();
            GameObject.FindGameObjectWithTag("PhilipsesWampumText").gameObject.GetComponent<Text>().text = "Points:<br>" + PhilipsesPoints.ToString();
            GameObject.FindGameObjectWithTag("SixNationsWampumText").gameObject.GetComponent<Text>().text = "Points:<br>" + SixNationsPoints.ToString();
            GameObject.FindGameObjectWithTag("MunseeWampumText").gameObject.GetComponent<Text>().text = "Points:<br>" + MunseePoints.ToString();
        }
    }

    [PunRPC]
    void moveToEndScene(PhotonMessageInfo info)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(4);
            SceneManager.LoadScene(4);
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

        if (info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString() || TurnTimerRanOut) // Bring back / revert theSender == info.Sender.ToString() &&  if neccessary
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
                                    DutchAmounts[ad+13] -= 1;
                                    DutchAmountsGameObjects[ad].GetComponent<Text>().text = DutchAmounts[ad].ToString() + "x";
                                    if (DutchAmountsGameObjects[ad + 13] != null)
                                    {
                                        DutchAmountsGameObjects[ad + 13].GetComponent<Text>().text = DutchAmounts[ad + 13].ToString() + "/" + DutchAmountsStarting[ad + 13].ToString() + "x";

                                        if (DutchAmounts[ad + 13] < 0)
                                        {
                                            DutchAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[ad + 13]).ToString();
                                        }
                                    }
                                }
                                else if (turn == 2 && tradeGivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Philipses")
                                {
                                    PhilipsesAmounts[ad] += 1;
                                    PhilipsesAmounts[ad+13] -= 1;
                                    PhilipsesAmountsGameObjects[ad].GetComponent<Text>().text = PhilipsesAmounts[ad].ToString() + "x";
                                    if (PhilipsesAmountsGameObjects[ad + 13] != null)
                                    {
                                        PhilipsesAmountsGameObjects[ad + 13].GetComponent<Text>().text = PhilipsesAmounts[ad + 13].ToString() + "/" + PhilipsesAmountsStarting[ad + 13].ToString() + "x";

                                        if (PhilipsesAmounts[ad + 13] < 0)
                                        {
                                            PhilipsesAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[ad + 13]).ToString();
                                        }
                                    }

                                }
                                else if (turn == 3 && tradeGivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Six Nations")
                                {
                                    SixNationsAmounts[ad] += 1;
                                    SixNationsAmounts[ad+13] -= 1;
                                    SixNationsAmountsGameObjects[ad].GetComponent<Text>().text = SixNationsAmounts[ad].ToString() + "x";
                                    if (SixNationsAmountsGameObjects[ad + 13] != null)
                                    {
                                        SixNationsAmountsGameObjects[ad + 13].GetComponent<Text>().text = SixNationsAmounts[ad + 13].ToString() + "/" + SixNationsAmountsStarting[ad + 13].ToString() + "x";

                                        if (SixNationsAmounts[ad + 13] < 0)
                                        {
                                            SixNationsAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[ad + 13]).ToString();
                                        }
                                    }
                                }
                                else if (turn == 4 && tradeGivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Munsee")
                                {
                                    MunseeAmounts[ad] += 1;
                                    MunseeAmounts[ad + 13] -= 1;
                                    MunseeAmountsGameObjects[ad].GetComponent<Text>().text = MunseeAmounts[ad].ToString() + "x";
                                    if (MunseeAmountsGameObjects[ad + 13] != null)
                                    {
                                        MunseeAmountsGameObjects[ad + 13].GetComponent<Text>().text = MunseeAmounts[ad + 13].ToString() + "/" + MunseeAmountsStarting[ad + 13].ToString() + "x";

                                        if (MunseeAmounts[ad + 13] < 0)
                                        {
                                            MunseeAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[ad + 13]).ToString();
                                        }
                                    }
                                }

                            }
                        }
                        if (tradeGivingCardsParent[ae].transform.GetChild(b).gameObject.GetComponent<PhotonView>().IsMine)
                        {
                            Debug.Log("Destroyed");
                            PhotonNetwork.Destroy(tradeGivingCardsParent[ae].transform.GetChild(b).gameObject);
                        }
                        else
                        {
                            Debug.Log("Find player which this object is his");
                        }
                        if(tradeGivingCardsParent[ae].transform.GetChild(b).gameObject != null){
                            tradeGivingCardsParent[ae].transform.GetChild(b).gameObject.SetActive(false);
                        }
                        



                        
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
                                        if(DutchAmountsGameObjects[ad + 13] != null)
                                        {
                                            DutchAmountsGameObjects[ad + 13].GetComponent<Text>().text = DutchAmounts[ad + 13].ToString() + "/" + DutchAmountsStarting[ad + 13].ToString() + "x";

                                            if (DutchAmounts[ad + 13] < 0)
                                            {
                                                DutchAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[ad + 13]).ToString();
                                            }
                                        }
                                        
                                    }
                                    else if ((turn == 2 && tradeReceivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Philipses"))
                                    {
                                        PhilipsesAmounts[ad + 13] += 1;
                                        if (PhilipsesAmountsGameObjects[ad + 13] != null)
                                        {
                                            PhilipsesAmountsGameObjects[ad + 13].GetComponent<Text>().text = PhilipsesAmounts[ad + 13].ToString() + "/" + PhilipsesAmountsStarting[ad + 13].ToString() + "x";

                                            if (PhilipsesAmounts[ad + 13] < 0)
                                            {
                                                PhilipsesAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[ad + 13]).ToString();
                                            }
                                        }
                                    }
                                    else if (turn == 3 && tradeReceivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Six Nations")
                                    {
                                        SixNationsAmounts[ad + 13] += 1;
                                        if (SixNationsAmountsGameObjects[ad + 13] != null)
                                        {
                                            SixNationsAmountsGameObjects[ad + 13].GetComponent<Text>().text = SixNationsAmounts[ad + 13].ToString() + "/" + SixNationsAmountsStarting[ad + 13].ToString() + "x";

                                            if (SixNationsAmounts[ad + 13] < 0)
                                            {
                                                SixNationsAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[ad + 13]).ToString();
                                            }
                                        }
                                    }
                                    else if (turn == 4 && tradeReceivingCardsParent[ae].gameObject.transform.parent.parent.tag == "Munsee")
                                    {
                                        MunseeAmounts[ad + 13] += 1;
                                        if (MunseeAmountsGameObjects[ad + 13] != null)
                                        {
                                            MunseeAmountsGameObjects[ad + 13].GetComponent<Text>().text = MunseeAmounts[ad + 13].ToString() + "/" + MunseeAmountsStarting[ad + 13].ToString() + "x";

                                            if (MunseeAmounts[ad + 13] < 0)
                                            {
                                                MunseeAmountsGameObjects[ad + 13].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[ad + 13]).ToString();
                                            }
                                        }
                                    }
                                }
                                Debug.Log(tags[ad] + " " + tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.tag + " ae: " + ae.ToString());
                                Debug.Log(tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.tag == tags[ad]);
                            }
                        }

                        if (tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.GetComponent<PhotonView>().IsMine)
                        {
                            Debug.Log("Destroyed");
                            PhotonNetwork.Destroy(tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject);
                        }
                        else
                        {
                            Debug.Log("Find player which this object is his");
                        }
                        if (tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject != null)
                        {
                            tradeReceivingCardsParent[ae].transform.GetChild(b).gameObject.SetActive(false);
                        }
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
            DutchTrading = false;
            PhilipsesTrading = false;
            SixNationsTrading = false;
            MunseeTrading = false;
            DutchAccepted = false;
            PhilipsesAccepted = false;
            SixNationsAccepted = false;
            MunseeAccepted = false;
            numberOfAcceptedTeams = 0;
            clearTradeButton = false;

            System.Array.Copy(DutchAmounts, DutchAmountsSubtractedDuringTrade, DutchAmounts.Length);
            System.Array.Copy(PhilipsesAmounts, PhilipsesAmountsSubtractedDuringTrade, PhilipsesAmounts.Length);
            System.Array.Copy(SixNationsAmounts, SixNationsAmountsSubtractedDuringTrade, SixNationsAmounts.Length);
            System.Array.Copy(MunseeAmounts, MunseeAmountsSubtractedDuringTrade, MunseeAmounts.Length);

            for(int zb = 0; zb < 13; zb++)
            {
                if (DutchAmounts[zb + 13] < 0 && DutchAmountsGameObjects[zb+13] != null)
                {
                    DutchAmountsGameObjects[zb + 13].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[zb + 13]).ToString();
                }
                if (PhilipsesAmounts[zb + 13] < 0 && PhilipsesAmountsGameObjects[zb + 13] != null)
                {
                    PhilipsesAmountsGameObjects[zb + 13].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[zb + 13]).ToString();
                }
                if (SixNationsAmounts[zb + 13] < 0 && SixNationsAmountsGameObjects[zb + 13] != null)
                {
                    SixNationsAmountsGameObjects[zb + 13].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[zb + 13]).ToString();
                }
                if (MunseeAmounts[zb + 13] < 0 && MunseeAmountsGameObjects[zb + 13] != null)
                {
                    MunseeAmountsGameObjects[zb + 13].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[zb + 13]).ToString();
                }
            }
            
            /*            DutchAmountsSubtractedDuringTrade = DutchAmounts;
                        PhilipsesAmountsSubtractedDuringTrade = PhilipsesAmounts;
                        SixNationsAmountsSubtractedDuringTrade = SixNationsAmounts;
                        MunseeAmountsSubtractedDuringTrade = MunseeAmounts;*/

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

            InventoryCardsInTrade = 0;
            WishlistCardsInTrade = 0;

            for (int ak = 0; ak < tags.Length; ak++)
            {
                GameObject[] extraCards = GameObject.FindGameObjectsWithTag(tags[ak]);
                for (int am = 0; am < extraCards.Length; am++)
                    if (extraCards[am].gameObject.transform.parent == null && extraCards[am].GetComponent<PhotonView>().IsMine)
                    {
                        PhotonNetwork.Destroy(extraCards[am].gameObject);
                    }
            }
        }
        

    }

    [PunRPC]
    void addWampumValues(PhotonMessageInfo info)
    {

        Debug.Log("We got here, RPC: " + trader);
        /*if (trader == Dutch)
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
        }*/
    }




    GameObject[] countdownTimers;
    public void StartCountDown()
    {
        
        if(countDownFinished == false)
        {
            countdownTimers = GameObject.FindGameObjectsWithTag("CountdownTimer");
            if(totalTurnNumber < 5)
            {
                time = turnTimeLengthFirstIteration;
            }
            else
            {
                time = turnTimeLength;

            }
            if (PhotonNetwork.LocalPlayer.ToString() == "#02 ''")
            {
                Debug.Log("Start Count Down");
                
                StartCoroutine("LoseTime");

                for (int aj = 0; aj < countdownTimers.Length; aj++)
                {
                    countdownTimers[aj].GetComponent<Text>().text = "Next Turn In: " + time + "s";
                }
            }
            countDownFinished = true;
        }
        
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            this.GetComponent<PhotonView>().RPC("ReduceTime", RpcTarget.All);
        }
    }

    [PunRPC]
    private void ReduceTime()
    {
        
        time--;
        //Debug.Log("Reduce Time: " + time);
        for (int al = 0; al < countdownTimers.Length; al++)
        {
            countdownTimers[al].GetComponent<Text>().text = "Next Turn In: " + time + "s";
        }
        if (time <= 0)
        {
            StopCoroutine("LoseTime");
            for(int al = 0; al < countdownTimers.Length; al++)
            {
                countdownTimers[al].GetComponent<Text>().text = "Moving turns...";
            }
            if (PhotonNetwork.LocalPlayer.ToString() == "#02 ''")
            {
                TimesUp();
            }
            
        }
    }

    public void TimesUp()
    {
        
        Debug.Log("Move turns");
        
        this.GetComponent<PhotonView>().RPC("TurnTimerRPC", RpcTarget.All);
        this.GetComponent<PhotonView>().RPC("clearAllTrades", RpcTarget.All);
        this.GetComponent<PhotonView>().RPC("MoveTurns", RpcTarget.All);
    }


    [PunRPC]
    void TurnTimerRPC(PhotonMessageInfo info)
    {
        clearTradeButton = true;
        TurnTimerRanOut = true;
    }











    IEnumerator waitForASecondThenResetMoveTurns()
    {

        yield return new WaitForSeconds(2);
        dutchMovedTurns = false;
        philipsesMovedTurns = false;
        sixNationsMovedTurns = false;
        munseeMovedTurns = false;

    }

    [PunRPC]
    void MoveTurns(PhotonMessageInfo info)
    {
        Debug.Log("Moving turns");
        // Can't check wishlist because you only lose wishlisted cards
        for (int za = 0; za <= 12; za++)
        {
            if (allAmountsSummed[za] != (DutchAmounts[za] + PhilipsesAmounts[za] + SixNationsAmounts[za] + MunseeAmounts[za]))
            {
                Debug.Log("Something is wrong with the values");
                Debug.Log("Total: " + allAmountsSummed[za] + "; Amounts: " + DutchAmounts[za] + " " + PhilipsesAmounts[za] + " " + SixNationsAmounts[za] + " " + MunseeAmounts[za]);
            }
        }

        
        System.Array.Copy(DutchAmounts, DutchAmountsSubtractedDuringTrade, DutchAmounts.Length);
        System.Array.Copy(PhilipsesAmounts, PhilipsesAmountsSubtractedDuringTrade, PhilipsesAmounts.Length);
        System.Array.Copy(SixNationsAmounts, SixNationsAmountsSubtractedDuringTrade, SixNationsAmounts.Length);
        System.Array.Copy(MunseeAmounts, MunseeAmountsSubtractedDuringTrade, MunseeAmounts.Length);


        DeactivateTeamFlags();
        Debug.Log("This was sent by: " + info.Sender.ToString() + "; This is running on " + PhotonNetwork.LocalPlayer.ToString() + "; theSender: " + theSender);

        DutchObject.SetActive(false);
        DutchObject.SetActive(true);
        MunseeObject.SetActive(false);
        MunseeObject.SetActive(true);

        InventoryCardsInTrade = 0;
        WishlistCardsInTrade = 0;

        if (info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString() || TurnTimerRanOut) // theSender == info.Sender.ToString() && 
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
            TurnTimerRanOut = false;
        //}
            Debug.Log("Turn: " + turn + "Total turn number: " + totalTurnNumber);

            Debug.Log(DutchAmounts.Length);
            

            for (int k = 0; k < SeasonalTimers.Length; k++)
            {
                SeasonalTimers[k].GetComponent<Text>().text = "Year: " + (totalTurnNumber + 1600).ToString() + " \nTurn: " + teamNames[(turn - 1)].ToString();
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

        for (int ag = 0; ag < 26; ag++)
        {


            /*if (DutchAmounts[ag] <= 0)
            {
                DutchAmounts[ag] = 0;
            }
            if (PhilipsesAmounts[ag] <= 0)
            {
                PhilipsesAmounts[ag] = 0;
            }
            if (SixNationsAmounts[ag] <= 0)
            {
                SixNationsAmounts[ag] = 0;
            }
            if (MunseeAmounts[ag] <= 0)
            {
                MunseeAmounts[ag] = 0;
            }*/

            if (DutchAmountsGameObjects[ag] != null && ag < 13)
            {
                Debug.Log(DutchAmountsGameObjects[ag]);
                Debug.Log(DutchAmountsGameObjects[ag].GetComponent<Text>().text);
                DutchAmountsGameObjects[ag].GetComponent<Text>().text = DutchAmounts[ag].ToString() + "x";
                Debug.Log(DutchAmountsGameObjects[ag].GetComponent<Text>().text);
            }
            else if (PhilipsesAmountsGameObjects[ag] != null && ag < 13)
            {
                PhilipsesAmountsGameObjects[ag].GetComponent<Text>().text = PhilipsesAmounts[ag].ToString() + "x";
                Debug.Log("sub: " + ag);
            }
            
            else if(SixNationsAmountsGameObjects[ag] != null && ag < 13)
            {
                Debug.Log(SixNationsAmountsGameObjects[ag].GetComponent<Text>().text);
                SixNationsAmountsGameObjects[ag].GetComponent<Text>().text = SixNationsAmounts[ag].ToString() + "x";
                Debug.Log(SixNationsAmountsGameObjects[ag].GetComponent<Text>().text);
            }
            else if(MunseeAmountsGameObjects[ag] != null && ag < 13)
            {
                MunseeAmountsGameObjects[ag].GetComponent<Text>().text = MunseeAmounts[ag].ToString() + "x";
            }


            else if (DutchAmountsGameObjects[ag] != null && ag >= 13)
            {
                Debug.Log(DutchAmountsGameObjects[ag]);
                Debug.Log(DutchAmountsGameObjects[ag].GetComponent<Text>().text);
                DutchAmountsGameObjects[ag].GetComponent<Text>().text = DutchAmounts[ag].ToString() + "/" + DutchAmountsStarting[ag].ToString() + "x";
                if (DutchAmounts[ag] < 0)
                {
                    DutchAmountsGameObjects[ag].GetComponent<Text>().text = "+" + Mathf.Abs(DutchAmounts[ag]).ToString();
                }

                Debug.Log(DutchAmountsGameObjects[ag].GetComponent<Text>().text);
            }
            else if (PhilipsesAmountsGameObjects[ag] != null && ag >= 13)
            {
                PhilipsesAmountsGameObjects[ag].GetComponent<Text>().text = PhilipsesAmounts[ag].ToString() + "/" + PhilipsesAmountsStarting[ag].ToString() + "x";
                if (PhilipsesAmounts[ag] < 0)
                {
                    PhilipsesAmountsGameObjects[ag].GetComponent<Text>().text = "+" + Mathf.Abs(PhilipsesAmounts[ag]).ToString();
                }

                Debug.Log("sub: " + ag);
            }

            else if (SixNationsAmountsGameObjects[ag] != null && ag >= 13)
            {
                Debug.Log(SixNationsAmountsGameObjects[ag].GetComponent<Text>().text);
                SixNationsAmountsGameObjects[ag].GetComponent<Text>().text = SixNationsAmounts[ag].ToString() + "/" + SixNationsAmountsStarting[ag].ToString() + "x";
                if (SixNationsAmounts[ag] < 0)
                {
                    SixNationsAmountsGameObjects[ag].GetComponent<Text>().text = "+" + Mathf.Abs(SixNationsAmounts[ag]).ToString();
                }
                Debug.Log(SixNationsAmountsGameObjects[ag].GetComponent<Text>().text);
            }
            else if (MunseeAmountsGameObjects[ag] != null && ag >= 13)
            {
                MunseeAmountsGameObjects[ag].GetComponent<Text>().text = MunseeAmounts[ag].ToString() + "/" + MunseeAmountsStarting[ag].ToString() + "x";
                if (MunseeAmounts[ag] < 0)
                {
                    MunseeAmountsGameObjects[ag].GetComponent<Text>().text = "+" + Mathf.Abs(MunseeAmounts[ag]).ToString();
                }
            }

            Debug.Log("ag: " + ag);
        }
        countDownFinished = false;
        StopCoroutine("LoseTime");
        StartCountDown();

        for (int ak = 0; ak < tags.Length; ak++)
        {
            GameObject[] extraCards = GameObject.FindGameObjectsWithTag(tags[ak]);
            for (int am = 0; am < extraCards.Length; am++)
                if (extraCards[am].gameObject.transform.parent == null && extraCards[am].GetComponent<PhotonView>().IsMine)
                {
                    PhotonNetwork.Destroy(extraCards[am].gameObject);                               
                }
        }
        StartCoroutine(waitForASecondThenResetMoveTurns());


        
        
        /*PhilipsesTradingButtonOnClick philipsesTradingButtonOnClick = new PhilipsesTradingButtonOnClick();
        philipsesTradingButtonOnClick.GetComponent<PhotonView>().RPC("greyOutButtons", RpcTarget.All);*/

    }
    
        




}