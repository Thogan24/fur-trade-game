using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text;

    public  Player Player { get; private set; }

    public void SetPlayerInfo(Player player1)
    {
        Player = player1;
        text.text = player1.NickName;
    }

}
