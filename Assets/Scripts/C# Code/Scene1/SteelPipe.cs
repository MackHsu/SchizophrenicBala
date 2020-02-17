using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelPipe : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject canvas = GameObject.Find("Canvas");
        HintManager.ShowTips(canvas, 4);
        HintManager.ShowTips(canvas, 3);
    }
}
