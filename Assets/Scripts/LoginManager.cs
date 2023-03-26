using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class LoginManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void ConnectAsTeacher() 
    {
        PhotonNetwork.NickName = "Ogretmen";
        PhotonNetwork.ConnectUsingSettings();
        ExitGames.Client.Photon.Hashtable avatarLink = new ExitGames.Client.Photon.Hashtable(){{"Avatar_Link","https://d1a370nemizbjq.cloudfront.net/2e47f2ab-87c3-42a1-ade3-0afc1da80a9b.glb"}};
        PhotonNetwork.LocalPlayer.SetCustomProperties(avatarLink);
    }
    public void ConnectAsStudent() 
    {
        PhotonNetwork.NickName = "Ogrenci";
        PhotonNetwork.ConnectUsingSettings();
        ExitGames.Client.Photon.Hashtable avatarLink = new ExitGames.Client.Photon.Hashtable(){{"Avatar_Link","https://d1a370nemizbjq.cloudfront.net/d771f62f-f556-4d1f-aa38-d646bab6e675.glb"}};
        PhotonNetwork.LocalPlayer.SetCustomProperties(avatarLink);
    }
    public override void OnConnected()
    {
        Debug.Log("Connected: Server");
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Connected: <color=aqua>"+ PhotonNetwork.NickName + "</color>");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Connected to lobby");
        PhotonNetwork.LoadLevel("Classroom");
    }
}