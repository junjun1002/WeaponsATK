using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの再有効化を行うクラス（アクティブカメラが複数あると一つ以外正常に動かなくなるのを解消するため）
/// </summary>
public class CameraEnable : MonoBehaviour
{
    void Start()
    {
        GetComponent<Camera>().enabled = false;
        GetComponent<Camera>().enabled = true;
    }
}
