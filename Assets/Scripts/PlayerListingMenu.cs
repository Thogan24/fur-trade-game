using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private PlayerListing playerListing;

    private List<PlayerListing> playerList = new List<PlayerListing>();
    public float changeInYFromListings = 57f;
    public int playerCount = 0;


    private RoomsCanvases roomsCanvases;
    private bool _ready = false;

    public GameObject HostText;
    public GameObject PlayerText;


    /*[SerializeField] private Text _readyUpText;
    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (state)
        {
            if (_ready)
            {
                _readyUpText.text = "R";
            }
            else
            {
                _readyUpText.text = "N";
            }
        }

    }*/


    
    public override void OnEnable()
    {
        Debug.Log("Enabled!");
        base.OnEnable();
        //SetReadyUp(false)
        GetCurrentRoomPlayers();

    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < playerList.Count; i++)
        {
            Destroy(playerList[i].gameObject);
        }

        playerList.Clear();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }
/*    public override void OnLeftRoom()
    {
        content.DestroyChildren();
    }*/
    private void GetCurrentRoomPlayers()
    {
        Debug.Log("Did");
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log("This many players (count me)");
            AddPlayerListing(playerInfo.Value, true);
        }

    }
    
    private void AddPlayerListing(Player player, bool notInRoomYet)
    {
        Debug.Log("Adding player listing");
        PlayerListing listing = Instantiate(playerListing, content); //new Vector3((float)727.2, 367 - PhotonNetwork.CountOfPlayers * 2, 0), Quaternion.identity
        Debug.Log(listing.transform.position.y);
        Debug.Log(Screen.height);
        if(notInRoomYet == true)
        {
            listing.transform.position = listing.transform.position + new Vector3(0.948288f + 1.525879e-05f, -2 - (38.6f * (playerCount)), 0);

        }
        else
        {
            listing.transform.position = listing.transform.position + new Vector3(0.948288f + 1.525879e-05f, -2 - ((0.08177905308f * Screen.height) * (playerCount)), 0);

        }
        Debug.Log(playerCount);
        Debug.Log(changeInYFromListings * (playerCount));
        Debug.Log(listing.transform.position.y);
        playerCount++;
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            playerList.Add(listing);
        }
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("PLAYER ENTERED!!!");

        //Don't do if rejoining
        if (SceneManager.GetActiveScene().name != "Main_Scene")
        {
            AddPlayerListing(newPlayer, false);
        }
        else
        {
            Debug.Log("Rejoinning player");
        }
            
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerCount--;
        int index = playerList.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(playerList[index].gameObject);
            playerList.RemoveAt(index);
        }

    }
    
    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient && playerCount == 4)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("TeamSelect");
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            TMPro.TextMeshProUGUI ThePlayerText = PlayerText.GetComponent<TMPro.TextMeshProUGUI>();
            StartCoroutine(FadeTextToZeroAlpha(1f, ThePlayerText));
        }
        else
        {
            Debug.Log("Else");
            TMPro.TextMeshProUGUI TheHostText = HostText.GetComponent<TMPro.TextMeshProUGUI>();
            StartCoroutine(FadeTextToZeroAlpha(1f, TheHostText));
        }
        
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TMPro.TextMeshProUGUI i)
    {
        Debug.Log("Setting alpha to 1");

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        yield return new WaitForSeconds(1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
