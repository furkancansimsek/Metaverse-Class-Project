using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    GameObject sitUI;
    [SerializeField] GameObject player;

    private void Start() {
        sitUI = transform.GetChild(0).gameObject;
    }

    public void ChairHover(){
        sitUI.SetActive(true);
        gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.cyan;
    }

    public void ChairHoverOut(){
        sitUI.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.white;;
    }

    public void SitDown(){
        Teleport();
    }

    private void Teleport(){
        player.transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        for (int i = 0; i < player.transform.childCount; i++)
        {
            player.transform.GetChild(i).transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        }
    }
}
