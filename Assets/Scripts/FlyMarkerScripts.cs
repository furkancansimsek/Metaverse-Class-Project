using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BNG;
using TMPro;
using Photon.Pun;


public class FlyMarkerScripts : MonoBehaviour
{
    NetworkedGrabbable grab;

    LineRenderer currentLineRenderer;

    //Lines çizgilerin tutulduğu bir empty obje
    //brush linerender gidecek
    [SerializeField] GameObject lines,brush;
    GameObject brushInstance;
    [SerializeField] UnityEngine.UI.Slider brushStartWidth, brushEndWidth, brushRedColor, brushGreenColor, brushBlueColor, brushRedColorEnd, brushGreenColorEnd, brushBlueColorEnd;
    [SerializeField] TextMeshProUGUI brushStartWidthText, brushEndWidthText, brushRedColorText, brushGreenColorText, brushBlueColorText, brushRedColorTextEnd, brushGreenColorTextEnd, brushBlueColorTextEnd;
    [SerializeField] Image colorCreationImage, colorCreationImageEnd;
    [SerializeField] private PhotonView pv;

    Vector3 lastPos;

    //lineları nerede oluşturacağının konumunu giriyoruz
    [SerializeField] Transform pos;

    Color color1 = Color.black;
    Color color2 = Color.black;
    float startSize = 0.03f;
    float endSize = 0.03f;
    
    private void Start()
    {
        pv = gameObject.GetPhotonView();

        currentLineRenderer = GetComponent<LineRenderer>();

        grab = GetComponent<NetworkedGrabbable>();
        brushStartWidth.maxValue = 0.05f;
        brushStartWidth.minValue = 0.01f;
        brushEndWidth.maxValue = 0.05f;
        brushEndWidth.minValue = 0.01f;
        brushRedColor.maxValue = 255f;
        brushRedColor.wholeNumbers = true;
        brushGreenColor.maxValue = 255f;
        brushGreenColor.wholeNumbers = true;
        brushBlueColor.maxValue = 255f;
        brushBlueColor.wholeNumbers = true;
         brushRedColorEnd.maxValue = 255f;
        brushRedColorEnd.wholeNumbers = true;
        brushGreenColorEnd.maxValue = 255f;
        brushGreenColorEnd.wholeNumbers = true;
        brushBlueColorEnd.maxValue = 255f;
        brushBlueColorEnd.wholeNumbers = true;
    }
    void Update()
    {
        //RPC_Draw();
        if ( grab.BeingHeld && InputBridge.Instance.AButtonDown )
         {
            pv.RPC("CreateBrush",RpcTarget.AllBuffered);
         }

        if ( grab.BeingHeld && InputBridge.Instance.AButton)
         {
             Vector3 posUptade = pos.position;
             if (posUptade != lastPos)
             {
                 pv.RPC("AddAPoint",RpcTarget.AllBuffered, posUptade);
                 lastPos = posUptade;
             }
         }
        else
         {
             currentLineRenderer = null;
         }
        pv.RPC("RPC_UseNewColor", RpcTarget.AllBuffered);
    }


    void RPC_Draw()
    {
         if ( grab.BeingHeld && InputBridge.Instance.RightTriggerDown )
         {
            pv.RPC("CreateBrush",RpcTarget.AllBuffered);
         }

         if ( grab.BeingHeld && InputBridge.Instance.RightTrigger>=0.1)
         {

             Vector3 posUptade = pos.position;
             if (posUptade != lastPos)
             {
                 pv.RPC("AddAPoint",RpcTarget.AllBuffered, posUptade);
                 lastPos = posUptade;
             }
         }
         else
         {
             currentLineRenderer = null;
         }


        // //Masaüstü kodları.

        // if (grab.BeingHeld && Input.GetKeyDown(KeyCode.Z))
        // {
        //     pv.RPC("CreateBrush",RpcTarget.AllBuffered);
        //     CreateBrush();
        // }

        // if (grab.BeingHeld && Input.GetKey(KeyCode.Z))
        // {
        //     Vector3 posUptade = pos.position;
        //     if (posUptade != lastPos)
        //     {
        //         pv.RPC("AddAPoint",RpcTarget.AllBuffered, posUptade);
        //         lastPos = posUptade;
        //     }
        // }
        // else
        // {
        //     currentLineRenderer = null;
        // }
    }

    [PunRPC]
    void CreateBrush()
    {
        //Photon Deneme
        brushInstance = PhotonNetwork.Instantiate("brush", pos.transform.position, pos.transform.rotation, 0);
        // brushInstance = Instantiate(brush, lines.transform);
        brushInstance.transform.parent = lines.transform;
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.startColor = color1;
        currentLineRenderer.endColor = color2;
        currentLineRenderer.startWidth = startSize;
        currentLineRenderer.endWidth = endSize;
        //Markerdaki oluşturma noktasının konumununu tutar
        Vector3 posUptade = pos.position;
        currentLineRenderer.SetPosition(0, posUptade);
        currentLineRenderer.SetPosition(1, posUptade);
    }

    //Linerendera yeni point noktaları eklemek için count sayısını arttırıyoruz 
    [PunRPC]
    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positonIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positonIndex, pointPos);
    }

    public void DeleteLine()
    {
        pv.RPC("RPC_DeleteLine", RpcTarget.AllBuffered);
    }

    //Tüm lineları silmek için kullanılan fonksiyondur
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
    //En son oluşturulan Çizgiyi silen fonksiyondur

    public void DeleteLastChild()
    {
        pv.RPC("RPC_DeleteLastChild", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPC_DeleteLastChild()
    {
        if (lines.transform.childCount != 0)
        {
            PhotonNetwork.Destroy(lines.transform.GetChild(0).gameObject);
            // Destroy(lines.transform.GetChild(0).gameObject);
        }
    }

    //Static renklerin ayarlandığı kısım
    public void ChangeColor(int colorId)
    {
        pv.RPC("RPC_ChangeColor", RpcTarget.AllBuffered,colorId);
    }

    [PunRPC]
    public void RPC_ChangeColor(int colorId)
    {
        switch (colorId)
        {
            case 0:
                color1 = Color.red;
                color2 = Color.red;
                brushRedColor.value = 255f;
                brushGreenColor.value = 0;
                brushBlueColor.value = 0;
                brushRedColorEnd.value = 255f;
                brushGreenColorEnd.value = 0;
                brushBlueColorEnd.value = 0;
                break;
            case 1:
                color1 = Color.blue;
                color2 = Color.blue;
                brushRedColor.value = 0;
                brushGreenColor.value = 0;
                brushBlueColor.value = 255f;
                brushRedColorEnd.value = 0;
                brushGreenColorEnd.value = 0;
                brushBlueColorEnd.value = 255f;
                break;
            case 2:
                color1 = Color.white;
                color2 = Color.white;
                brushRedColor.value = 255f;
                brushGreenColor.value = 255f;
                brushBlueColor.value = 255f;
                brushRedColorEnd.value = 255f;
                brushGreenColorEnd.value = 255f;
                brushBlueColorEnd.value = 255f;
                break;
            case 3:
                color1 = Color.black;
                color2 = Color.black;
                brushRedColor.value = 0;
                brushGreenColor.value = 0;
                brushBlueColor.value = 0;
                brushRedColorEnd.value = 0;
                brushGreenColorEnd.value = 0;
                brushBlueColorEnd.value = 0;
                break;
            case 4:
                color1 = Color.green;
                color2 = Color.green;
                brushRedColor.value = 0;
                brushGreenColor.value = 0;
                brushBlueColor.value = 255f;
                brushRedColorEnd.value = 0;
                brushGreenColorEnd.value = 0;
                brushBlueColorEnd.value = 255f;
                break;
            case 5:
                color1 = Color.yellow;
                color2 = Color.red;
                brushRedColor.value = 255f;
                brushGreenColor.value = 255f;
                brushBlueColor.value = 0;
                brushRedColorEnd.value = 255f;
                brushGreenColorEnd.value = 255f;
                brushBlueColorEnd.value = 0;
                break;
            default:
                color1 = Color.red;
                color2 = Color.red;
                brushRedColor.value = 255f;
                brushGreenColor.value = 0;
                brushBlueColor.value = 0;
                brushRedColorEnd.value = 255f;
                brushGreenColorEnd.value = 0;
                brushBlueColorEnd.value = 0;
                break;
        }
        currentLineRenderer.startColor = color1;
        currentLineRenderer.endColor = color2;
    }

    public void ChangeBrushSize(int brushId)
    {
        pv.RPC("RPC_ChangeBrushSize", RpcTarget.AllBuffered,brushId);
    }

    [PunRPC]
    //Static fırça boyutlarının ayarlandığı yer
    public void RPC_ChangeBrushSize(int brushId)
    {
        switch (brushId)
        {
            case 0:
                startSize = 0.04f;
                endSize = 0.04f;
                break;
            case 1:
                startSize = 0.03f;
                endSize = 0.03f;
                break;
            case 2:
                startSize = 0.022f;
                endSize = 0.022f;
                break;
            case 3:
                startSize = 0.015f;
                endSize = 0.015f;
                break;
            case 4:
                startSize = 0.007f;
                endSize = 0.007f;
                break;
            default:
                startSize = 0.007f;
                endSize = 0.007f;
                break;
        }
    }

    // Sliderların change özelliğine id verilerek güncelleme yapıyor

    public void SliderUpdate(int sliderId)
    {
        pv.RPC("RPC_SliderUpdate", RpcTarget.AllBuffered,sliderId);
    }

    [PunRPC]
    public void RPC_SliderUpdate(int sliderId)
    {
        switch (sliderId)
        {
            case 0:
                brushStartWidthText.text = Convert.ToString(brushStartWidth.value);
                startSize = brushStartWidth.value;
                endSize = brushEndWidth.value;
                break;
            case 1:
                brushEndWidthText.text = Convert.ToString(brushEndWidth.value);
                startSize = brushStartWidth.value;
                endSize = brushEndWidth.value;
                break;
            case 2:
                brushRedColorText.text = Convert.ToString(brushRedColor.value);
                color1 = new Color32(Convert.ToByte(brushRedColor.value), Convert.ToByte(brushGreenColor.value), Convert.ToByte(brushBlueColor.value), 255);
                break;
            case 3:
                brushGreenColorText.text = Convert.ToString(brushGreenColor.value);
                color1 = new Color32(Convert.ToByte(brushRedColor.value), Convert.ToByte(brushGreenColor.value), Convert.ToByte(brushBlueColor.value), 255);
                break;
            case 4:
                brushBlueColorText.text = Convert.ToString(brushBlueColor.value);
                color1 = new Color32(Convert.ToByte(brushRedColor.value), Convert.ToByte(brushGreenColor.value), Convert.ToByte(brushBlueColor.value), 255);
                break;
            case 5:
                brushRedColorTextEnd.text = Convert.ToString(brushRedColorEnd.value);
                color2 = new Color32(Convert.ToByte(brushRedColorEnd.value), Convert.ToByte(brushGreenColorEnd.value), Convert.ToByte(brushBlueColorEnd.value), 255);
                break;
            case 6:
                brushGreenColorTextEnd.text = Convert.ToString(brushGreenColorEnd.value);
                color2 = new Color32(Convert.ToByte(brushRedColorEnd.value), Convert.ToByte(brushGreenColorEnd.value), Convert.ToByte(brushBlueColorEnd.value), 255);
                break;
            case 7:
                brushBlueColorTextEnd.text = Convert.ToString(brushBlueColorEnd.value);
                color2 = new Color32(Convert.ToByte(brushRedColorEnd.value), Convert.ToByte(brushGreenColorEnd.value), Convert.ToByte(brushBlueColorEnd.value), 255);
                break;
        }
    }

    [PunRPC]
    private void RPC_UseNewColor()
    {
        colorCreationImage.color = color1;
        colorCreationImageEnd.color = color2;
    }
}