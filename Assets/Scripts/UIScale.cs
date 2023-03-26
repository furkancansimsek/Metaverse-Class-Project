using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class UIScale : MonoBehaviour
{
    [SerializeField] float multiplier;
    public void UIScaleIncrease(){
        transform.localScale *= multiplier;
    }

    public void UIScaleReduce(){
        transform.localScale /= multiplier;
    }
}
