using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class ClockScript : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject panel, tablet, pen, laser;

    [SerializeField]
    private PhotonView pv;
    private bool isOpenTab = false;
    private bool isOpenPan = false;
    private bool isCreatedPen = false;
    private bool isCreatedLaser = false;
    private void Start() 
    {
        pv = gameObject.GetPhotonView();  
    }

    public void OpenTablet()
    {
        pv.RPC("RPC_OpenTablet",RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void RPC_OpenTablet()
    {
        if (!isOpenTab)
        {
            tablet.transform.DOScaleY(0.005f, 1f);
            tablet.transform.DOScaleX(0.005f, 1f);
            isOpenTab = !isOpenTab;
        }
        else
        {
            tablet.transform.DOScaleY(0f, 1f);
            tablet.transform.DOScaleX(0f, 1f);
            isOpenTab = !isOpenTab;
        }
    }
    public void OpenClockPanel()
    {
        if (!isOpenPan)
        {
            panel.transform.DOScaleY(1f, 1f);
            panel.transform.DOScaleX(1f, 1f);
            isOpenPan = !isOpenPan;
        }
        else
        {
            panel.transform.DOScaleY(0f, 1f);
            panel.transform.DOScaleX(0f, 1f);
            isOpenPan = !isOpenPan;
        }
    }

    public void CreatePen()
    {
        if (!isCreatedPen)
        {
            pen.transform.localPosition = new Vector3(-0.25f, 0.03f, 0);
            pen.transform.localRotation = new Quaternion(-90, 90, 0, 0);
            pen.transform.DOScaleY(1f, 1f);
            pen.transform.DOScaleX(1f, 1f);
            isCreatedPen = !isCreatedPen;
        }
        else
        {
            pen.transform.DOScaleY(0f, 1f);
            pen.transform.DOScaleX(0f, 1f);
            isCreatedPen = !isCreatedPen;
        }
    }
    public void CreateLaser()
    {
        if (!isCreatedLaser)
        {
            laser.transform.DOScaleY(1f, 1f);
            laser.transform.DOScaleX(1f, 1f);
            isCreatedLaser = !isCreatedLaser;
        }
        else
        {
            laser.transform.DOScaleY(0f, 1f);
            laser.transform.DOScaleX(0f, 1f);
            isCreatedLaser = !isCreatedLaser;
        }
    }
}
