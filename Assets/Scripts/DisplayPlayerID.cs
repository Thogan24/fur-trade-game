using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DisplayPlayerID : MonoBehaviour
{
    public PhotonView playerPrefab;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PhotonNetwork.AuthValues.UserId;
    }
}
