using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class avatarSelectNetwork :  MonoBehaviour, IPunInstantiateMagicCallback
{
    public object[] instantiationData;
    PhotonView pv;
    private void Start() {
        pv = gameObject.GetPhotonView();
    }
    public  void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        ReadyPlayerMeLoadAvatar readyPlayerMeLoadAvatar = GetComponent<ReadyPlayerMeLoadAvatar>();
        instantiationData = info.photonView.InstantiationData;
        readyPlayerMeLoadAvatar.avatarUrl = (string)instantiationData[0];
        Debug.Log("Yeni avatar atarken gelen data <color=yellow>" + instantiationData[0] + "</color>");
    }
}