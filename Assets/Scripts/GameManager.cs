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
    public int BeaverAmountSixNations = 12;
    public int DeerskinsAmountSixNations = 5;
    public int BearAmountSixNations = 6;
    public int FisherAmountSixNations = 4;
    public int FoxAmountSixNations = 3;
    public int SchepelsAmountSixNations = 0;
    public int DuffelsAmountSixNations = 0;
    public int LinenAmountSixNations = 0;
    public int StockingsAmountSixNations = 0;
    public int StroudsAmountSixNations = 0;
    public int AxesAmountSixNations = 0;
    public int BeadsAmountSixNations = 0;
    public int ScissorsAmountSixNations = 0;

    public int BeaverAmountMunsee;
    public int DeerskinsAmountMunsee;
    public int BearAmountMunsee;
    public int FisherAmountMunsee;
    public int FoxAmountMunsee;
    public int SchepelsAmountMunsee;
    public int DuffelsAmountMunsee;
    public int LinenAmountMunsee = 0;
    public int StockingsAmountMunsee;
    public int StroudsAmountMunsee;
    public int AxesAmountMunsee;
    public int BeadsAmountMunsee;
    public int ScissorsAmountMunsee;

    public int BeaverAmountPhilipses;
    public int DeerskinsAmountPhilipses;
    public int BearAmountPhilipses;
    public int FisherAmountPhilipses;
    public int FoxAmountPhilipses;
    public int SchepelsAmountPhilipses;
    public int DuffelsAmountPhilipses;
    public int LinenAmountPhilipses = 8;
    public int StockingsAmountPhilipses;
    public int StroudsAmountPhilipses;
    public int AxesAmountPhilipses;
    public int BeadsAmountPhilipses;
    public int ScissorsAmountPhilipses;

    public int BeaverAmountDutch = 0;
    public int DeerskinsAmountDutch = 0;
    public int BearAmountDutch = 0;
    public int FisherAmountDutch = 0;
    public int FoxAmountDutch = 0;
    public int SchepelsAmountDutch = 0;
    public int DuffelsAmountDutch = 12;
    public int LinenAmountDutch = 0;
    public int StockingsAmountDutch = 0;
    public int StroudsAmountDutch = 9;
    public int AxesAmountDutch = 5;
    public int BeadsAmountDutch = 20;
    public int ScissorsAmountDutch = 3;

    public GameObject SixNationsBeaverAmountObject;
    public GameObject SixNationsDeerskinsAmountObject;
    public GameObject SixNationsBearAmountObject;
    public GameObject SixNationsFisherAmountObject;
    public GameObject SixNationsFoxAmountObject;
    public GameObject SixNationsSchepelsAmountObject;
    public GameObject SixNationsDuffelsAmountObject;
    public GameObject SixNationsLinenAmountObject;
    public GameObject SixNationsStockingsAmountObject;
    public GameObject SixNationsStroudsAmountObject;
    public GameObject SixNationsAxesAmountObject;
    public GameObject SixNationsBeadsAmountObject;
    public GameObject SixNationsScissorsAmountObject;

    public GameObject MunseeBeaverAmountObject;
    public GameObject MunseeDeerskinsAmountObject;
    public GameObject MunseeBearAmountObject;
    public GameObject MunseeFisherAmountObject;
    public GameObject MunseeFoxAmountObject;
    public GameObject MunseeSchepelsAmountObject;
    public GameObject MunseeDuffelsAmountObject;
    public GameObject MunseeLinenAmountObject;
    public GameObject MunseeStockingsAmountObject;
    public GameObject MunseeStroudsAmountObject;
    public GameObject MunseeAxesAmountObject;
    public GameObject MunseeBeadsAmountObject;
    public GameObject MunseeScissorsAmountObject;

    public GameObject PhilipsesBeaverAmountObject;
    public GameObject PhilipsesDeerskinsAmountObject;
    public GameObject PhilipsesBearAmountObject;
    public GameObject PhilipsesFisherAmountObject;
    public GameObject PhilipsesFoxAmountObject;
    public GameObject PhilipsesSchepelsAmountObject;
    public GameObject PhilipsesDuffelsAmountObject;
    public GameObject PhilipsesLinenAmountObject;
    public GameObject PhilipsesStockingsAmountObject;
    public GameObject PhilipsesStroudsAmountObject;
    public GameObject PhilipsesAxesAmountObject;
    public GameObject PhilipsesBeadsAmountObject;
    public GameObject PhilipsesScissorsAmountObject;

    public GameObject DutchBeaverAmountObject;
    public GameObject DutchDeerskinsAmountObject;
    public GameObject DutchBearAmountObject;
    public GameObject DutchFisherAmountObject;
    public GameObject DutchFoxAmountObject;
    public GameObject DutchSchepelsAmountObject;
    public GameObject DutchDuffelsAmountObject;
    public GameObject DutchLinenAmountObject;
    public GameObject DutchStockingsAmountObject;
    public GameObject DutchStroudsAmountObject;
    public GameObject DutchAxesAmountObject;
    public GameObject DutchBeadsAmountObject;
    public GameObject DutchScissorsAmountObject;




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
        gameManager = this.gameObject;
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
            this.GetComponent<PhotonView>().RPC("mainSceneInventorySetupRPC", RpcTarget.All);

            AlreadyLoaded = true;
        }

        //Debug.LogError(PhotonNetwork.PlayerList.Length + " | " + DutchTrading + " - " + gameManager.GetPhotonView().ViewID); 


        /*
                if (DutchTrading)
                {
                    DutchTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
                if (SixNationsTrading)
                {
                    SixNationsTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
                if (MunseeTrading)
                {
                    MunseeTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
                if (PhilipsesTrading)
                {
                    PhilipsesTradeButton.GetComponent<Image>().color = Color.HSVToRGB(0f, 0f, 1f);
                }
        */







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

    /*
     * STARTING INVENTORIES
    
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
    void mainSceneInventorySetupRPC()
    {
        if (PhotonNetwork.LocalPlayer.ToString() == Dutch && AlreadyLoaded == false)
        {
            DutchBeaverAmountObject = GameObject.FindGameObjectWithTag("BeaverAmount");
            DutchBeaverAmountObject.GetComponent<Text>().text = BeaverAmountDutch.ToString() + "x";

            /*DutchDeerskinsAmountObject.GetComponent<Text>().text = DeerskinsAmountDutch.ToString();
            DutchBearAmountObject.GetComponent<Text>().text = BearAmountDutch.ToString();
            DutchFisherAmountObject.GetComponent<Text>().text = FisherAmountDutch.ToString();
            DutchFoxAmountObject.GetComponent<Text>().text = FoxAmountDutch.ToString();
            DutchSchepelsAmountObject.GetComponent<Text>().text = SchepelsAmountDutch.ToString();
            DutchDuffelsAmountObject.GetComponent<Text>().text = DuffelsAmountDutch.ToString();
            DutchLinenAmountObject.GetComponent<Text>().text = LinenAmountDutch.ToString();
            DutchStockingsAmountObject.GetComponent<Text>().text = StockingsAmountDutch.ToString();
            DutchStroudsAmountObject.GetComponent<Text>().text = StroudsAmountDutch.ToString();
            DutchAxesAmountObject.GetComponent<Text>().text = AxesAmountDutch.ToString();
            DutchBeadsAmountObject.GetComponent<Text>().text = BeadsAmountDutch.ToString();
            DutchScissorsAmountObject.GetComponent<Text>().text = ScissorsAmountDutch.ToString();*/
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

    
    //NEED TO MAKE AN RPC
    public void addCardToTrade(string tag, string parentTag)
    {
        
        if (PhotonNetwork.LocalPlayer.ToString() == Dutch && DutchTrading == true)
        {
            if (SixNationsTrading == true)
            {
                enemyTeamButtonPos = SixNationsTradeButton.transform.position;
                Debug.LogError(enemyTeamButtonPos);
            }
            if (MunseeTrading == true)
            {
                enemyTeamButtonPos = MunseeTradeButton.transform.position;
                Debug.LogError(enemyTeamButtonPos);
            }
            if (PhilipsesTrading == true)
            {
                enemyTeamButtonPos = PhilipsesTradeButton.transform.position;
                Debug.LogError(enemyTeamButtonPos);
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
        if (tag == "Beaver" && ((BeaverAmountDutch > 0 && isParentInventory == 1) || isParentWishlist == 1))
        {
            Debug.LogError("Beaver Card Added");
            instantiatedCard = PhotonNetwork.Instantiate("BeaverCard", pos + new Vector3((2 + ((float) 0.3 * InventoryCardsInTrade * isParentInventory)) + (isParentWishlist * (3 + (float) 0.3 * WishlistCardsInTrade)), (float) 0.2, 0), Quaternion.identity);
            BeaverAmountDutch--;


        }
        else if (tag == "Duffels" && ((DuffelsAmountDutch > 0 && isParentInventory == 1) || isParentWishlist == 1))
        {
            Debug.LogError("Duffels Card Added");
            instantiatedCard = PhotonNetwork.Instantiate("DuffelsCard", pos + new Vector3((2 + ((float)0.3 * InventoryCardsInTrade * isParentInventory)) + (isParentWishlist * (3 + (float)0.3 * WishlistCardsInTrade)), (float)0.2, 0), Quaternion.identity);
            DuffelsAmountDutch--;
        }
        else if (tag == "DeerSkin" && ((DeerskinsAmountDutch > 0 && isParentInventory == 1) || isParentWishlist == 1))
        {
            Debug.LogError("Deer Skin Card Added");
            instantiatedCard = PhotonNetwork.Instantiate("DeerSkinCard", pos + new Vector3((2 + ((float)0.3 * InventoryCardsInTrade * isParentInventory)) + (isParentWishlist * (3 + (float)0.3 * WishlistCardsInTrade)), (float)0.2, 0), Quaternion.identity);
            DeerskinsAmountDutch--;
        }
        else
        {
            Debug.LogError("None of specified card left");
            return;
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
    void cardSwitchTeams()
    {
        // Team who's turn it is recieves their items
        if (DutchAccepted && turn == 1)
        {
            // Dutch inventory + Trade Receiving Cards - Trade Giving Cards
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
        // Clear all trades

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
