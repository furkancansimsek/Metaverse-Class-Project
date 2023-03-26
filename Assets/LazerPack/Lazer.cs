using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Photon.Pun;

public class Lazer : MonoBehaviour
{
    [SerializeField] GameObject lazer;
    Grabbable lazerGrabbable;
    PhotonView pv;
    [SerializeField] LaserPointer laserPointer;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        lazerGrabbable = GetComponent<NetworkedGrabbable>();
    }
    void Update()
    {
       
        if (lazerGrabbable.BeingHeld && InputBridge.Instance.RightTrigger == 1)
        {
            pv.RPC("RPC_ActivateLazer",RpcTarget.AllBuffered);
        }
        else
        {
            
        }
    }

    [PunRPC]
    public void RPC_ActivateLazer()
    {
        laserPointer.Active = true;
    }

    [PunRPC]
    public void RPC_DeActivateLazer()
    {
        laserPointer.Active = false;
    }
}
