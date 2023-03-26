using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{
    [SerializeField] bool mirrorMode;
    void Update()
    {
        if(!mirrorMode){
            transform.LookAt(2 * transform.position - Camera.main.transform.position);
        }else{
            transform.LookAt(Camera.main.transform.position);
        }
    }
}
