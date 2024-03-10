using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using Photon.Realtime;

public class IroquoisInfoScript : MonoBehaviour
{

    public GameObject IroquoisInfo;
    void Start()
    {
        IroquoisInfo = this.gameObject;
    }

    public void IroquoisInfoClicked()
    {

    }

    [PunRPC]
    void WhenClicked(Vector3 transform, string userIDOfClicker) // 
    {


    }
}
