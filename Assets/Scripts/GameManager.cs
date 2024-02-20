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

    public GameObject DutchTradeButton;
    public GameObject SixNationsTradeButton;
    public GameObject MunseeTradeButton;
    public GameObject PhilipsesTradeButton;

    public GameObject gameManager; // This Object



    // TRADING VARIABLES

    public int InventoryCardsInTrade = 0;
    public int WishlistCardsInTrade = 0;
    public Vector3 enemyTeamButtonPos;

    public GameObject BeaverCard;
    public GameObject DuffelsCard;
    public GameObject DeerSkinCard;



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


    public string[] teamNames = { "Dutch", "Six Nations", "Munsee", "Philipses" }; // FOR ENEMY TEAM BUTTONS
    public string[] cameras = { "Dutch", "Philipses", "Six Nations", "Munsee"}; // FOR CAMERA 
    
    // TURN SYSTEM VARIABLES
    public int turn = 1;
    public int totalTurnNumber = 0;
    
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




    // MAIN SCENE






  

    [PunRPC]
    void mainSceneCameraRPC()
    {
        //Debug.LogError(SceneManager.GetActiveScene().name);
        if (PhotonNetwork.LocalPlayer.ToString() == Dutch && AlreadyLoaded == false)
        {
            DutchCameraPrefab = DutchCameraGameObject.GetPhotonView();
            DutchCamera = PhotonView.Instantiate(DutchCameraPrefab);
            DutchTextCanvasObject = GameObject.FindGameObjectWithTag("Dutch Text Canvas");
            DutchBackgroundCanvasObject = GameObject.FindGameObjectWithTag("Dutch Background Canvas");
            DutchCardsCanvasObject = GameObject.FindGameObjectWithTag("Dutch Card Canvas");
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
            SixNationsCardsCanvasObject = GameObject.FindGameObjectWithTag("Six Nations Card Canvas");
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
            MunseeCardsCanvasObject = GameObject.FindGameObjectWithTag("Munsee Card Canvas");
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
            PhilipsesCardsCanvasObject = GameObject.FindGameObjectWithTag("Philipses Card Canvas");
            PhilipsesTextCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
            PhilipsesBackgroundCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
            PhilipsesCardsCanvasObject.GetComponent<Canvas>().worldCamera = PhilipsesCamera.gameObject.GetComponent<Camera>();
        }
        DutchTradeButton = GameObject.FindGameObjectWithTag("Dutch Trading");
        SixNationsTradeButton = GameObject.FindGameObjectWithTag("Six Nations Trading");
        MunseeTradeButton = GameObject.FindGameObjectWithTag("Munsee Trading");
        PhilipsesTradeButton = GameObject.FindGameObjectWithTag("Philipses Trading");
        
        
    }


    /*
                DeerskinsAmountDutch 
                BearAmountDutch
                FisherAmountDutch
                FoxAmountDutch
                SchepelsAmountDutch
                DuffelsAmountDutch
                LinenAmountDutch
                StockingsAmountDutch
                StroudsAmountDutch
                AxesAmountDutch
                BeadsAmountDutch
                ScissorsAmountDutch
     */

    [PunRPC]
    void mainSceneSetInventoryAmountsRPC() // TO DO
    {
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
            

        }
        if (PhotonNetwork.LocalPlayer.ToString() == Munsee && AlreadyLoaded == false)
        {
            
        }
        if (PhotonNetwork.LocalPlayer.ToString() == Philipses && AlreadyLoaded == false)
        {
            
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

    
    [PunRPC]
    void addCardToTrade(string tag, string parentTag)
    {
        if (PhotonNetwork.LocalPlayer.ToString() == Dutch && DutchTrading == true)
        {
            if (SixNationsTrading == true)
            {
                enemyTeamButtonPos = SixNationsTradeButton.transform.position;
                Debug.LogError(enemyTeamButtonPos);
            }
            else if (MunseeTrading == true)
            {
                enemyTeamButtonPos = MunseeTradeButton.transform.position;
                Debug.LogError(enemyTeamButtonPos);
            }
            else if (PhilipsesTrading == true)
            {
                enemyTeamButtonPos = PhilipsesTradeButton.transform.position;
                Debug.LogError(enemyTeamButtonPos);
            }
            else
            {
                Debug.LogError("No Enemy Team Selected");
                return;
            }
        }

        Vector3 pos = enemyTeamButtonPos;
        Debug.LogError("Adding card...");
        GameObject instantiatedCard = null;
        int isParentWishlist = 0;
        int isParentInventory = 1;


        if (parentTag == "Wishlist")
        {
            isParentWishlist = 1;
            isParentInventory = 0;
        }


        Debug.Log(isParentInventory + " " + isParentWishlist);
        for (int i = 0; i < tags.Length; i++)
        {
            Debug.Log(tag + " " + tags[i]);
            if (tag == tags[i] && ((DutchAmounts[i] > 0 && isParentInventory == 1) || isParentWishlist == 1))
            {
                
                if (isParentInventory == 1)
                {
                    instantiatedCard = PhotonNetwork.Instantiate(Prefabs[i].ToString().Remove(Prefabs[i].ToString().Length - 25), pos + new Vector3((2 + ((float)0.3 * InventoryCardsInTrade * isParentInventory)) + (isParentWishlist * (3 + (float)0.3 * WishlistCardsInTrade)), (float)0.2, 0), Quaternion.identity);
                    Debug.Log(DutchAmounts[i]);
                    DutchAmounts[i]--;
                    DutchAmountsGameObjects[i].GetComponent<Text>().text = DutchAmounts[i].ToString() + "x";
                }
                else
                {
                    Debug.Log(DutchAmounts[i + 13]);
                    if (DutchAmountsGameObjects[i+13] != null && DutchAmounts[i+13] > 0)
                    {
                        instantiatedCard = PhotonNetwork.Instantiate(Prefabs[i].ToString().Remove(Prefabs[i].ToString().Length - 25), pos + new Vector3((2 + ((float)0.3 * InventoryCardsInTrade * isParentInventory)) + (isParentWishlist * (3 + (float)0.3 * WishlistCardsInTrade)), (float)0.2, 0), Quaternion.identity);
                        DutchAmounts[i + 13]--;
                        DutchAmountsGameObjects[i+13].GetComponent<Text>().text = DutchAmounts[i+13].ToString() + "x";

                        break;
                    }
                    else
                    {
                        Debug.LogError("None of specified card left");
                        return;
                    }
                }
                
                break;
            }
            else
            {
                Debug.LogError("None of specified card left");
                if (i+1 == tags.Length)
                {
                    return;
                }
            }
        }

        instantiatedCard.GetComponent<Button>().enabled = false;
        
        instantiatedCard.transform.position = new Vector3(instantiatedCard.transform.position.x, instantiatedCard.transform.position.y, 10);
        Debug.LogError(instantiatedCard.transform.position);

        if (parentTag == "Wishlist")
        {
            WishlistCardsInTrade++;
            Debug.Log(WishlistCardsInTrade);
            instantiatedCard.transform.SetParent(DutchCardsCanvasObject.transform.GetChild(1));
        }
        else
        {
            InventoryCardsInTrade++;
            Debug.Log(InventoryCardsInTrade);
            instantiatedCard.transform.SetParent(DutchCardsCanvasObject.transform.GetChild(0));
        }

        DutchAccepted = false;
        PhilipsesAccepted = false;
        MunseeAccepted = false;
        SixNationsAccepted = false;

    }

    [PunRPC]
    void cardSwitchTeams() // TODO
    {
        // Team who's turn it is recieves their items
        if (DutchAccepted && turn == 1)
        {
            // Dutch inventory + Trade Receiving Cards - Trade Giving Cards - TODO
            DutchAccepted = false;
            DutchTrading = false;
        }
        else if (PhilipsesAccepted && turn == 2)
        {
            // Philipses inventory + Trade Receiving Cards - Trade Giving Cards
            PhilipsesAccepted = false;
            PhilipsesTrading = false;
        }
        else if (SixNationsAccepted && turn == 3)
        {
            // Six Nations inventory + Trade Receiving Cards - Trade Giving Cards
            SixNationsAccepted = false;
            SixNationsTrading = false;
        }
        else if (MunseeAccepted && turn == 4)
        {
            // Munsee inventory + Trade Receiving Cards - Trade Giving Cards
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

        this.GetComponent<PhotonView>().RPC("clearAllTradesAndMoveTurns", RpcTarget.All);


    }

    [PunRPC]
    void clearAllTradesAndMoveTurns()
    {
        // CLEAR ALL TRADES -- TODO


        if (turn == 4)
        {
            turn = 1;
        }
        else
        {
            turn++;
        }
    }

}
