using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PositionReset : MonoBehaviourPunCallbacks
{
    Vector3 start;
    Vector3 rota;
    Rigidbody rb;
    public PhotonView pv;
    void Start()
    {
        start = transform.position;
        rota = transform.eulerAngles;
        rb = gameObject.GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="ground")
        {
            pv.RPC("RPC_Reset",RpcTarget.AllBuffered);
        }   
    }

    [PunRPC]
    public void RPC_Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = start;
        transform.eulerAngles = rota;
    }
}
