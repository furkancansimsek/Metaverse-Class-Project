using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BNG {

    /// <summary>
    /// A simple laser pointer that draws a line with a dot at the end
    /// </summary>
    public class LaserPointer : MonoBehaviour {

        public float MaxRange = 25f;
        public LayerMask ValidLayers;
        public Transform LaserEnd;

        public bool Active = true;

        LineRenderer line;

        PhotonView pv;

        // Start is called before the first frame update
        void Start() {
            line = GetComponent<LineRenderer>();
            pv = gameObject.GetPhotonView();
        }

        void LateUpdate() {
            pv.RPC("RPC_Lazer",RpcTarget.AllBuffered);
        }

        [PunRPC]
        void RPC_Lazer(){
            if(Active) {

                line.enabled = true;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, MaxRange, ValidLayers, QueryTriggerInteraction.Ignore)) {
                    line.useWorldSpace = true;
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, hit.point);

                    // Add dot at line's end
                    LaserEnd.gameObject.SetActive(true);
                    LaserEnd.position = hit.point;
                    LaserEnd.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                }
                else {
                    line.useWorldSpace = false;
                    line.SetPosition(0, transform.localPosition);
                    line.SetPosition(1, new Vector3(0, 0, MaxRange));
                    LaserEnd.gameObject.SetActive(false);
                }
            }
            else {
                LaserEnd.gameObject.SetActive(false);

                if (line) {
                    line.enabled = false;
                }
            }
        }
    }
}
