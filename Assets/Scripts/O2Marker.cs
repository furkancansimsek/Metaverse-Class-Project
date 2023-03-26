using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Photon.Pun;

public class O2Marker : MonoBehaviour
{
    public GameObject lines;
    NetworkedGrabbable grab;
    LineRenderer currentLineRenderer;
    GameObject brushInstance,lastChild2;
    Vector3 lastPos;
    PhotonView pv;

    public Transform pen;
    
    private void Start() {
        pv=gameObject.GetPhotonView();
    }
    private void Update()
    {
    #if UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pv.RPC("CreateBrush",RpcTarget.AllBuffered);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            Vector3 mousePos = pen.position;
            if (lastPos != mousePos)
            {
                pv.RPC("AddAPoint",RpcTarget.AllBuffered,mousePos);
                lastPos = mousePos;
            }
        }
  
    #else
        if (InputBridge.Instance.AButtonDown)
        {
            pv.RPC("CreateBrush",RpcTarget.AllBuffered);
        }
        else if (InputBridge.Instance.AButton)
        {
            Vector3 mousePos = pen.position;
            if (lastPos != mousePos)
            {
                pv.RPC("AddAPoint",RpcTarget.AllBuffered,mousePos);
                lastPos = mousePos;
            }
        }
  
    #endif
    }

    [PunRPC]
    void CreateBrush()
    {
        brushInstance = PhotonNetwork.Instantiate("brush",pen.position,Quaternion.identity);
        brushInstance.transform.SetParent(lines.transform);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector3 mousePos = pen.position;

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    [PunRPC]
    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    [PunRPC]
    void NullLine(){
        currentLineRenderer = null;
    }


    //Eklentiler


    //Silme Kodu
    public void DeleteLine()
    {
        pv.RPC("RPC_DeleteLine", RpcTarget.AllBuffered);
    }

    //Tüm linelarý silmek için kullanýlan fonksiyondur
    [PunRPC]
    public void RPC_DeleteLine()
    {
        if (lines.transform.childCount != 0)
        {
            for (int i = 0; i < lines.transform.childCount; i++)
            {
                PhotonNetwork.Destroy(lines.transform.GetChild(i).gameObject);
                // Destroy(lines.transform.GetChild(i).gameObject);
            }
        }
    }

    //Geri alma kodu
    public void DeleteLastChild()
    {
        pv.RPC("RPC_DeleteLastChild", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_DeleteLastChild()
    {
        int lastChildIndex = lines.transform.childCount - 1;
        if (lastChildIndex >= 0)
        {
                lastChild2 = lines.transform.GetChild(lastChildIndex).gameObject;
                PhotonNetwork.Destroy(lastChild2);
                if (lastChildIndex == 0)
                {
                    PhotonNetwork.Destroy(lines);
                }
            
            // Destroy(lines.transform.GetChild(0).gameObject);
        }
    }
}