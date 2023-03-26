using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Eraser : MonoBehaviourPunCallbacks
{
    GameObject go,go2,lastChild2, lastChild;
    private PhotonView pv;

    private void Start()
    {
        pv = gameObject.GetComponent<PhotonView>();
    }
    public void DestroyMakerLine() {
        pv.RPC("RPC_DestroyMakerLine",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_DestroyMakerLine()
    {
        go = GameObject.Find("MarkerLineHolder");
        Destroy(go);
    }
    public void DeleteLastChildObject()
    {

        try
        {
            go = GameObject.Find("MarkerLineHolder");

            int lastChildIndex = go.transform.childCount - 1;
            if (lastChildIndex >= 0)
            {
                lastChild = go.transform.GetChild(lastChildIndex).gameObject;
                Destroy(lastChild);
                if (lastChildIndex == 0)
                {
                    Destroy(go);
                }
            }
        }
        catch (System.Exception)
        {

        }

    }


    public void DestroyMakerLine2()
    {
        go2 = GameObject.Find("MarkerLineHolder2");
        Destroy(go2);
    }


    public void DeleteLastChildObject2()
    {

        try
        {
            go2 = GameObject.Find("MarkerLineHolder2");

            int lastChildIndex = go2.transform.childCount - 1;
            if (lastChildIndex >= 0)
            {
                lastChild2 = go2.transform.GetChild(lastChildIndex).gameObject;
                Destroy(lastChild2);
                if (lastChildIndex == 0)
                {
                    Destroy(go2);
                }
            }
        }
        catch (System.Exception)
        {

        }

    }
}