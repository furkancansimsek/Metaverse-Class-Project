using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SkyTriggerSC : MonoBehaviourPunCallbacks
{
    public Material[] skyboxes;
    [SerializeField]
    private PhotonView pv;
    void Start() {
        pv = gameObject.GetPhotonView();
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.gameObject.tag)
            {
                case "sky1":
                other.gameObject.transform.position = new Vector3(-4.56650591f,3.15399981f,10.5419893f);
                other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                pv.RPC("Sky1",RpcTarget.AllBuffered);
                break;

                case "sky2":
                other.gameObject.transform.position = new Vector3(-3.53800464f,3.25400019f,9.60243988f);
                other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                pv.RPC("Sky2",RpcTarget.AllBuffered);
                break;

                case "sky3":
                other.gameObject.transform.position = new Vector3(-2.58320856f,3.15399981f,8.48690987f);
                other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                pv.RPC("Sky3",RpcTarget.AllBuffered);
                break;
        }
    }

    [PunRPC]
    void Sky3(){
        RenderSettings.skybox = skyboxes[2];
    }
    [PunRPC]
    void Sky2(){
        RenderSettings.skybox = skyboxes[1];
    }
    [PunRPC]
    void Sky1(){
        RenderSettings.skybox = skyboxes[0];
    }
}
