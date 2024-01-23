using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class TeamSelectHandler : MonoBehaviourPunCallbacks
{

   /* [SerializeField] GameObject sixNationsButton;
    [SerializeField] GameObject sixNationsMenu;
    [SerializeField] Button sixNationsClose;

    [SerializeField] GameObject MunseeButton;
    [SerializeField] GameObject MunseeMenu;
    [SerializeField] Button MunseeClose;

    [SerializeField] GameObject DutchButton;
    [SerializeField] GameObject DutchMenu;
    [SerializeField] Button DutchClose;

    [SerializeField] GameObject PhilipsesButton;
    [SerializeField] GameObject PhilipsesMenu;
    [SerializeField] Button PhilipsesClose;*/
    public GameManager gameManager;

    public Text debug;


    public GameObject debugger;

    public PhotonView playerPrefab;
    public PhotonView sixNationsButtonInstantiated;
    public PhotonView dutchButtonInstantiated;

    public GameObject Dutch;
    public GameObject SixNations;
    public GameObject theCanvas;



    bool menuOpen = false;
    int userID = 1;


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        

        //sixNationsButton.SetActive(true);
        /*sixNationsMenu.SetActive(false);

        Button sixNationsButtonReal = sixNationsButton.GetComponent<Button>();
        sixNationsButtonReal.onClick.AddListener(sixNationsButtonClicked);
        sixNationsClose.onClick.AddListener(sixNationsClosedClicked);

        MunseeButton.SetActive(true);
        MunseeMenu.SetActive(false);

        Button MunseeButtonReal = MunseeButton.GetComponent<Button>();
        MunseeButtonReal.onClick.AddListener(MunseeButtonClicked);
        MunseeClose.onClick.AddListener(MunseeClosedClicked);

        DutchButton.SetActive(true);
        DutchMenu.SetActive(false);

        Button DutchButtonReal = DutchButton.GetComponent<Button>();
        DutchButtonReal.onClick.AddListener(DutchButtonClicked);
        DutchClose.onClick.AddListener(DutchClosedClicked);

        PhilipsesButton.SetActive(true);
        PhilipsesMenu.SetActive(false);

        Button PhilipsesButtonReal = PhilipsesButton.GetComponent<Button>();
        PhilipsesButtonReal.onClick.AddListener(PhilipsesButtonClicked);
        PhilipsesClose.onClick.AddListener(PhilipsesClosedClicked);*/
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        // User ID Assignment
        Debug.Log("Joined a room.");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.PublishUserId = true;
        AuthenticationValues authValues = new AuthenticationValues("0");
        PhotonNetwork.AuthValues.UserId = userID.ToString();
        userID += 1;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log(player);
        }

        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        // Instantiating buttons
        SixNations = PhotonNetwork.Instantiate(sixNationsButtonInstantiated.name, Vector3.zero, Quaternion.identity);
        Dutch = PhotonNetwork.Instantiate(dutchButtonInstantiated.name, Vector3.zero, Quaternion.identity);
        //GameObject DEBUG = PhotonNetwork.Instantiate(debugger.name, Vector3.zero, Quaternion.identity);

        Debug.LogError(PhotonNetwork.AuthValues.UserId);
        SixNations.transform.parent = theCanvas.transform;
        Dutch.transform.parent = theCanvas.transform;
        Debug.Log(SixNations.transform.position);
        SixNations.transform.position = new Vector3(400, 400, 0);
        Dutch.transform.position = new Vector3(900, 200, 0);
        Debug.Log(SixNations.transform);
        SixNations.SetActive(true);
        Dutch.SetActive(true);
        //DEBUG.SetActive(true);
        //DEBUG.transform.position = new Vector3(400, 200, 0);






    }


    /*public void sixNationsButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            sixNationsMenu.SetActive(true);
        }
    }

    public void sixNationsClosedClicked()
    {
         menuOpen = false;
         sixNationsMenu.SetActive(false);
        
    }

    public void MunseeButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            MunseeMenu.SetActive(true);
        }
    }

    public void MunseeClosedClicked()
    {
        menuOpen = false;
        MunseeMenu.SetActive(false);

    }

    public void DutchButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            DutchMenu.SetActive(true);
        }
    }

    public void DutchClosedClicked()
    {
        menuOpen = false;
        DutchMenu.SetActive(false);

    }

    public void PhilipsesButtonClicked()
    {
        if (!menuOpen)
        {
            menuOpen = true;
            PhilipsesMenu.SetActive(true);
        }
    }

    public void PhilipsesClosedClicked()
    {
        menuOpen = false;
        PhilipsesMenu.SetActive(false);

    }


    public void sixNationsTeamJoin()
    {
        Debug.Log(PhotonNetwork.AuthValues.UserId);
        debug.text = PhotonNetwork.AuthValues.UserId;
        sixNationsClosedClicked();
        gameManager.SixNations = PhotonNetwork.AuthValues.UserId;
        gameManager.SixNationsJoined = true;
        sixNationsButton.SetActive(false);
    }
    public void MunseeTeamJoin()
    {
        Debug.Log(PhotonNetwork.AuthValues.UserId);
        debug.text = PhotonNetwork.AuthValues.UserId;
        MunseeClosedClicked();
        gameManager.Munsee = PhotonNetwork.AuthValues.UserId;
        gameManager.MunseeJoined = true;
        MunseeButton.SetActive(false);
    }
    public void DutchTeamJoin()
    {
        Debug.Log(PhotonNetwork.AuthValues.UserId);
        debug.text = PhotonNetwork.AuthValues.UserId;
        DutchClosedClicked();
        gameManager.DutchWestIndiaCompany = PhotonNetwork.AuthValues.UserId;
        gameManager.DutchJoined = true;
        DutchButton.SetActive(false);
    }
    public void PhilipsesTeamJoin()
    {
        Debug.Log(PhotonNetwork.AuthValues.UserId);
        debug.text = PhotonNetwork.AuthValues.UserId;
        PhilipsesClosedClicked();
        gameManager.Philipses = PhotonNetwork.AuthValues.UserId;
        gameManager.PhilipsesJoined = true;
        PhilipsesButton.SetActive(false);
    }
*/

}
