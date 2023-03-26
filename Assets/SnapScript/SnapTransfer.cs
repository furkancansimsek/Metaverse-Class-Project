using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTransfer : MonoBehaviour
{
    public TakeSnap snaps;
    public void ScreenShot()
    {
        snaps.Targets();
    }
}
