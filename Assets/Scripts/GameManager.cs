using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
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
    public int[] DutchAmounts =      {0, 0, 0, 0, 0, 0, 12, 0, 0, 9, 5, 20, 3, 12, 4, 4, 5, 10, 0, 0, 0, 0, 0, 0, 0, 0};


    public GameObject[] SixNationsAmountsGameObjects = { };
    public GameObject[] MunseeAmountsGameObjects = { };
    public GameObject[] PhilipsesAmountsGameObjects = { };
    public GameObject[] DutchAmountsGameObjects = { };
    public GameObject[] Prefabs;
    public string[] tags = { };


    public string[] teamNames = { "Dutch", "Philipses", "Six Nations", "Munsee" }; // FOR ENEMY TEAM BUTTONS
    public string[] cameras = { "Dutch", "Philipses", "Six Nations", "Munsee"}; // FOR CAMERA 


    public GameObject[] tradeGivingCardsParent = { }; // All giving card parents
    public GameObject[] tradeReceivingCardsParent = { }; // All receiving card parents


    // TURN SYSTEM VARIABLES
    public int turn = 1;
    public int totalTurnNumber = 1;
    public float TurnTimer = 90.0f;
    
    public GameObject[] SeasonalTimers = { };
    
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
        if (DutchJoined && !AlreadyLoaded) // && SixNationsJoined && MunseeJoined && PhilipsesJoined
        {
            Debug.Log("Teans joined, loading main screen");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
                SceneManager.LoadScene(1);
            }
            this.GetComponent<PhotonView>().RPC("mainSceneCameraRPC", RpcTarget.All);
            this.GetComponent<PhotonView>().RPC("mainSceneSetInventoryAmountsRPC", RpcTarget.All);

            AlreadyLoaded = true;
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






  

    [PunRPC]
    void mainSceneCameraRPC()
    {
        //Debug.LogError(SceneManager.GetActiveScene().name);
        DutchCardsCanvasObject = GameObject.FindGameObjectWithTag("Dutch Card Canvas");
        PhilipsesCardsCanvasObject = GameObject.FindGameObjectWithTag("Philipses Card Canvas");
        SixNationsCardsCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Card Canvas");
        MunseeCardsCanvasObject = GameObject.FindGameObjectWithTag("Munsee Card Canvas");

        if (PhotonNetwork.LocalPlayer.ToString() == Dutch && AlreadyLoaded == false)
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
        if (PhotonNetwork.LocalPlayer.ToString() == SixNations && AlreadyLoaded == false)
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
        if (PhotonNetwork.LocalPlayer.ToString() == Munsee && AlreadyLoaded == false)
        {
            MunseeCameraPrefab = MunseeCameraGameObject.GetPhotonView();
            MunseeCamera = PhotonView.Instantiate(MunseeCameraPrefab);
            MunseeTextCanvasObject = GameObject.FindGameObjectWithTag("Munsee Text Canvas");
            MunseeBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Munsee Background Canvas");
            MunseeCamera.transform.parent = MunseeTextCanvasObject.transform.parent;
            MunseeTextCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
            MunseeBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
            MunseeCardsCanvasObject.GetComponent<Canvas>().worldCamera = MunseeCamera.gameObject.GetComponent<Camera>();
        }
        if (PhotonNetwork.LocalPlayer.ToString() == Philipses && AlreadyLoaded == false)
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


        // If there is somehow than one of specified camera
        GameObject[] DutchCamerasCheckArray = GameObject.FindGameObjectsWithTag("DWIC Camera");
        for(int a = 0; a < DutchCamerasCheckArray.Length; a++)
        {
            if (DutchTextCanvasObject.GetComponent<Canvas>().worldCamera != DutchCamerasCheckArray[a].GetComponent<Camera>() || DutchBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != DutchCamerasCheckArray[a].GetComponent<Camera>() || DutchCardsCanvasObject.GetComponent<Canvas>().worldCamera != DutchCamerasCheckArray[a].GetComponent<Camera>())
            {
                DutchCamerasCheckArray[a].SetActive(false);
            }
        }
        GameObject[] SixNationsCamerasCheckArray = GameObject.FindGameObjectsWithTag("Six Nations Camera");
        for (int a = 0; a < SixNationsCamerasCheckArray.Length; a++)
        {
            if (SixNationsTextCanvasObject.GetComponent<Canvas>().worldCamera != SixNationsCamerasCheckArray[a].GetComponent<Camera>() || DutchBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != SixNationsCamerasCheckArray[a].GetComponent<Camera>() || DutchCardsCanvasObject.GetComponent<Canvas>().worldCamera != SixNationsCamerasCheckArray[a].GetComponent<Camera>())
            {
                SixNationsCamerasCheckArray[a].SetActive(false);
            }
        }
        GameObject[] MunseeCamerasCheckArray = GameObject.FindGameObjectsWithTag("Munsee Camera");
        for (int a = 0; a < MunseeCamerasCheckArray.Length; a++)
        {
            if (MunseeTextCanvasObject.GetComponent<Canvas>().worldCamera != MunseeCamerasCheckArray[a].GetComponent<Camera>() || MunseeBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != MunseeCamerasCheckArray[a].GetComponent<Camera>() || MunseeCamerasCheckArray[a].GetComponent<Canvas>().worldCamera != MunseeCamerasCheckArray[a].GetComponent<Camera>())
            {
                MunseeCamerasCheckArray[a].SetActive(false);
            }
        }
        GameObject[] PhilipsesCamerasCheckArray = GameObject.FindGameObjectsWithTag("Philipse Camera");
        for (int a = 0; a < PhilipsesCamerasCheckArray.Length; a++)
        {
            if (PhilipsesTextCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>() || PhilipsesBackgroundCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>() || PhilipsesCardsCanvasObject.GetComponent<Canvas>().worldCamera != PhilipsesCamerasCheckArray[a].GetComponent<Camera>())
            {
                PhilipsesCamerasCheckArray[a].SetActive(false);
            }
        }


    }


    [PunRPC]
    void mainSceneSetInventoryAmountsRPC() // TO DO
    {
        GameObject[] AmountsGameObjectsWithTag = { };
        if (!AlreadyLoaded)
        {
            for (int i = 0; i < DutchAmounts.Length; i++)
            {
                if (i < 13)
                {
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
/*


        if (PhotonNetwork.LocalPlayer.ToString() == Dutch && AlreadyLoaded == false)
        {
            for (int i = 0; i < DutchAmounts.Length; i++)
            {
                if (i < 13)
                {
                    Debug.Log(i);

                    DutchAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i] + "Amount");
                    DutchAmountsGameObjects[i].GetComponent<Text>().text = DutchAmounts[i].ToString() + "x";
                }
                // Part of Wishlist
                else
                {
                    if(DutchAmounts[i] == 0)
                    {
                        DutchAmountsGameObjects[i] = null;
                    }
                    else
                    {
                        DutchAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i - 13] + " Amount Wishlist");
                        DutchAmountsGameObjects[i].GetComponent<Text>().text = DutchAmounts[i].ToString() + "x";
                    }
                }
                
                
            }
        }
        if (PhotonNetwork.LocalPlayer.ToString() == SixNations && AlreadyLoaded == false)
        {
            for (int i = 0; i < SixNationsAmounts.Length; i++)
            {
                if (i < 13)
                {
                    Debug.Log(i);
                    SixNationsAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i] + "Amount");
                    SixNationsAmountsGameObjects[i].GetComponent<Text>().text = SixNationsAmounts[i].ToString() + "x";
                }
                // Part of Wishlist
                else
                {
                    if (SixNationsAmounts[i] == 0)
                    {
                        SixNationsAmountsGameObjects[i] = null;
                    }
                    else
                    {
                        SixNationsAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i - 13] + " Amount Wishlist");
                        SixNationsAmountsGameObjects[i].GetComponent<Text>().text = SixNationsAmounts[i].ToString() + "x";
                    }
                }


            }
        }
        if (PhotonNetwork.LocalPlayer.ToString() == Munsee && AlreadyLoaded == false)
        {
            for (int i = 0; i < MunseeAmounts.Length; i++)
            {
                if (i < 13)
                {
                    Debug.Log(i);
                    MunseeAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i] + "Amount");
                    MunseeAmountsGameObjects[i].GetComponent<Text>().text = MunseeAmounts[i].ToString() + "x";
                }
                // Part of Wishlist
                else
                {
                    if (MunseeAmounts[i] == 0)
                    {
                        MunseeAmountsGameObjects[i] = null;
                    }
                    else
                    {
                        MunseeAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i - 13] + " Amount Wishlist");
                        MunseeAmountsGameObjects[i].GetComponent<Text>().text = MunseeAmounts[i].ToString() + "x";
                    }
                }


            }
        }
        if (PhotonNetwork.LocalPlayer.ToString() == Philipses && AlreadyLoaded == false)
        {
            for (int i = 0; i < PhilipsesAmounts.Length; i++)
            {
                if (i < 13)
                {
                    Debug.Log(i);
                    PhilipsesAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i] + "Amount");
                    PhilipsesAmountsGameObjects[i].GetComponent<Text>().text = PhilipsesAmounts[i].ToString() + "x";
                }
                // Part of Wishlist
                else
                {
                    if(PhilipsesAmounts[i] == 0)
                    {
                        PhilipsesAmountsGameObjects[i] = null;
                    }
                    else
                    {
                        PhilipsesAmountsGameObjects[i] = GameObject.FindGameObjectWithTag(tags[i - 13] + " Amount Wishlist");
                        PhilipsesAmountsGameObjects[i].GetComponent<Text>().text = PhilipsesAmounts[i].ToString() + "x";
                    }
                }
                
                
            }
        }*/

        tradeGivingCardsParent = GameObject.FindGameObjectsWithTag("Giving");
        tradeReceivingCardsParent = GameObject.FindGameObjectsWithTag("Receiving");
        SeasonalTimers = GameObject.FindGameObjectsWithTag("Seasonal Timer");
        for (int k = 0; k < SeasonalTimers.Length; k++)
        {
            SeasonalTimers[k].GetComponent<Text>().text = "Year: " + (totalTurnNumber + 1600).ToString() + " | Turn: " + teamNames[turn - 1].ToString();
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
            if (playerString == Dutch && DutchTrading == true)
            {
                if (SixNationsTrading == true) // If six nations is the enemy team
                {
                    for (int a = 0; a < SixNationsTradeButton.Length; a++)
                    {
                        enemyTeamButtonPos[a] = SixNationsTradeButton[a].transform.position;
                        if (SixNationsTradeButton[a].transform.parent.parent.name == "Six Nations") // If the screen is Six Nations
                        {
                            addToReceiving[a] = true;
                            enemyTeamButtonPos[a] = DutchTradeButton[a].transform.position;
                        }
                    }

                }

                else if (MunseeTrading == true)
                {
                    for (int a = 0; a < MunseeTradeButton.Length; a++)
                    {
                        enemyTeamButtonPos[a] = MunseeTradeButton[a].transform.position;
                        if (MunseeTradeButton[a].transform.parent.parent.name == "Munsee") // If the screen is Munsee
                        {
                            addToReceiving[a] = true;
                            enemyTeamButtonPos[a] = DutchTradeButton[a].transform.position;

                        }
                    }
                }

                else if (PhilipsesTrading == true)
                {
                    for (int a = 0; a < PhilipsesTradeButton.Length; a++)
                    {
                        enemyTeamButtonPos[a] = PhilipsesTradeButton[a].transform.position;
                        if (PhilipsesTradeButton[a].transform.parent.parent.name == "Philipses") // If the screen is Philipses
                        {
                            addToReceiving[a] = true;
                            enemyTeamButtonPos[a] = DutchTradeButton[a].transform.position;

                        }
                    }
                }

                else
                {
                    Debug.LogError("No Enemy Team Selected");
                    return;
                }
            }


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
            for (int z = 0; z < tags.Length; z++)
            {
                Debug.Log(tag + " " + tags[z]);
                if (tag == tags[z] && ((DutchAmounts[z] > 0 && isParentInventory == 1) || isParentWishlist == 1)) // If the card amounts are greater than zero or is in their wishlist
                {

                    if (isParentInventory == 1)
                    {
                        for (int b = 0; b < enemyTeamButtonPos.Length; b++)
                        {
                            if (addToReceiving[b] == true) // Pretend that it is a isParentWishlist side, yet counts the number of InventoryCardsInTrade
                            {
                                instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), enemyTeamButtonPos[b] + new Vector3((float)3 + (float)2.2 + (float)0.3 * InventoryCardsInTrade, (float)0.2, 0), Quaternion.identity);
                            }
                            else
                            {
                                instantiatedCard[b] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), enemyTeamButtonPos[b] + new Vector3(((float)2.1 + ((float)0.3 * InventoryCardsInTrade * isParentInventory)) + (isParentWishlist * ((float)2.2 + (float)0.3 * WishlistCardsInTrade)), (float)0.2, 0), Quaternion.identity);
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
                                    instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), enemyTeamButtonPos[j] + new Vector3((float)2.1 + ((float)0.3 * WishlistCardsInTrade), (float)0.2, 0), Quaternion.identity);
                                }
                                else
                                {

                                    instantiatedCard[j] = PhotonNetwork.Instantiate(Prefabs[z].ToString().Remove(Prefabs[z].ToString().Length - 25), enemyTeamButtonPos[j] + new Vector3(((float)3 + ((float)0.3 * InventoryCardsInTrade * isParentInventory)) + (isParentWishlist * ((float)2.2 + (float)0.3 * WishlistCardsInTrade)), (float)0.2, 0), Quaternion.identity);
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
                        Debug.Log(CardsCanvasObjects[j]);
                        Debug.Log(j);
                        instantiatedCard[j].transform.SetParent(CardsCanvasObjects[j].transform.GetChild(0)); // SETS IT TO GIVING CARD
                    }
                    else
                    {
                        Debug.Log(CardsCanvasObjects[j]);
                        instantiatedCard[j].transform.SetParent(CardsCanvasObjects[j].transform.GetChild(1)); // SETS IT TO RECEIVING CARD
                    }

                }
                else
                {
                    Debug.Log(InventoryCardsInTrade);
                    if (addToReceiving[j] == true)
                    {
                        Debug.Log(CardsCanvasObjects[j]);
                        instantiatedCard[j].transform.SetParent(CardsCanvasObjects[j].transform.GetChild(1)); // SETS IT TO RECEIVING CARD
                    }
                    else
                    {
                        Debug.Log(CardsCanvasObjects[j]);
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

                PhilipsesAccepted = false;
                PhilipsesTrading = false;
            }
            else if (SixNationsAccepted && turn == 3)
            {
                // Six Nations inventory + Trade Receiving Cards

                SixNationsAccepted = false;
                SixNationsTrading = false;
            }
            else if (MunseeAccepted && turn == 4)
            {
                // Munsee inventory + Trade Receiving Cards

                MunseeAccepted = false;
                MunseeTrading = false;
            }

            // Team who accepted the trade recieves their items

            if (DutchAccepted)
            {
                // Dutch inventory + Trade Receiving Cards - Trade Giving Cards
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
                SixNationsAccepted = false;
                SixNationsTrading = false;
            }
            else if (MunseeAccepted)
            {
                // Munsee inventory + Trade Receiving Cards - Trade Giving Cards
                MunseeAccepted = false;
                MunseeTrading = false;
            }

            this.GetComponent<PhotonView>().RPC("clearAllTrades", RpcTarget.All);
            this.GetComponent<PhotonView>().RPC("MoveTurns", RpcTarget.All);
        }
        


    }

    [PunRPC]
    void clearAllTrades(PhotonMessageInfo info)
    {
        Debug.Log(info.Sender.ToString());
        if(theSender == info.Sender.ToString() && info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString())
        {
            // Note; int b is utilized in case of crashing aswell. It will not run over 1000 iterations
            for (int i = 0; i < tradeGivingCardsParent.Length; i++) // For every Trade giving card object, destroy all their child objects
            {
                
                int b = 0;
                while (tradeGivingCardsParent[i].transform.childCount != b && b < 1000)
                {
                    tradeGivingCardsParent[i].transform.GetChild(b).gameObject.SetActive(false);
                    //Debug.Log(tradeGivingCardsParent[i].transform.GetChild(b).gameObject);
                    b++;
                }



                
                b = 0;
                while (tradeReceivingCardsParent[i].transform.childCount != b && b < 1000)
                {
                    tradeReceivingCardsParent[i].transform.GetChild(b).gameObject.SetActive(false);
                    //Debug.Log(tradeReceivingCardsParent[i].transform.GetChild(b).gameObject);
                    b++;
                }

                /*
                 * 
                 * int j = 0;
                 * while(tradeGivingCardsParent[i].transform.childCount > 0 && j < 150)
                {
                    PhotonView.Destroy(tradeGivingCardsParent[i].transform.GetChild(0).gameObject);
                    j++;
                    Debug.Log(tradeGivingCardsParent[i].transform.childCount);
                    Debug.Log(tradeGivingCardsParent[i].transform.GetChild(0).gameObject);
                }
                j = 0;
                while (tradeReceivingCardsParent[i].transform.childCount > 0 && j < 150)
                {
                    PhotonView.Destroy(tradeReceivingCardsParent[i].transform.GetChild(0).gameObject);
                    j++;
                }*/
            }
        }
        

    }

    [PunRPC]
    void MoveTurns(PhotonMessageInfo info)
    {
        Debug.Log(info.Sender.ToString());
        if (theSender == info.Sender.ToString() && info.Sender.ToString() == PhotonNetwork.LocalPlayer.ToString())
        {
            
            totalTurnNumber++;
            Debug.Log("Called");
            if (turn == 4)
            {
                turn = 1;
                Debug.Log("This ran bruh");
            }
            else
            {
                turn++;
                Debug.Log("turn++");
            }
            Debug.Log(turn);
            Debug.Log("Total turn number: " + totalTurnNumber);
            for (int k = 0; k < SeasonalTimers.Length; k++)
            {
                SeasonalTimers[k].GetComponent<Text>().text = "Year: " + (totalTurnNumber + 1600).ToString() + " | Turn: " + teamNames[(turn - 1)].ToString();
            }

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

        /*PhilipsesTradingButtonOnClick philipsesTradingButtonOnClick = new PhilipsesTradingButtonOnClick();
        philipsesTradingButtonOnClick.GetComponent<PhotonView>().RPC("greyOutButtons", RpcTarget.All);*/

    }
        
    

}
