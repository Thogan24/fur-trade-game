using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Reconnect : MonoBehaviourPunCallbacks, IConnectionCallbacks
{
    public LoadBalancingClient loadBalancingClient;
    public AppSettings appSettings;

    void Start()
    {
        loadBalancingClient = PhotonNetwork.NetworkingClient;
        appSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        loadBalancingClient.AddCallbackTarget(this);
    }
/*    public Reconnect(LoadBalancingClient loadBalancingClient, AppSettings appSettings)
    {
        this.loadBalancingClient = loadBalancingClient;
        this.appSettings = appSettings;
        this.loadBalancingClient.AddCallbackTarget(this);
    }*/

    void onDestroy()
    {
        
        if (this.loadBalancingClient == null)
            return;
        
        this.loadBalancingClient.RemoveCallbackTarget(this);
    }
/*    ~Reconnect()
    {
        this.loadBalancingClient.RemoveCallbackTarget(this);
    }*/

    void IConnectionCallbacks.OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Trying to reconnect, the cause is " + cause.ToString());
        if (this.CanRecoverFromDisconnect(cause))
        {
            Debug.Log("recovery in process");
            this.Recover();
        }
    }

    private bool CanRecoverFromDisconnect(DisconnectCause cause)
    {
        switch (cause)
        {
            // the list here may be non exhaustive and is subject to review
            case DisconnectCause.Exception:
            case DisconnectCause.ServerTimeout:
            case DisconnectCause.ClientTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByServerReasonUnknown:
                return true;
        }
        return false;
    }

    private void Recover()
    {
        if (!loadBalancingClient.ReconnectAndRejoin())
        {
            Debug.LogError("ReconnectAndRejoin failed, trying Reconnect");
            if (!loadBalancingClient.ReconnectToMaster())
            {
                Debug.LogError("Reconnect failed, trying ConnectUsingSettings");
                if (!loadBalancingClient.ConnectUsingSettings(appSettings))
                {
                    Debug.LogError("ConnectUsingSettings failed");
                }
            }
        }
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room!");
    }
    #region Unused Methods

    void IConnectionCallbacks.OnConnected()
    {
    }

    void IConnectionCallbacks.OnConnectedToMaster()
    {
    }

    void IConnectionCallbacks.OnRegionListReceived(RegionHandler regionHandler)
    {
    }

    void IConnectionCallbacks.OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }

    void IConnectionCallbacks.OnCustomAuthenticationFailed(string debugMessage)
    {
    }

    #endregion
}