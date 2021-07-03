using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestToggle : MonoBehaviour
{
    public GameObject DebugWindow;

    /// <summary>
    /// デバッグウインドウの表示切り替え
    /// </summary>
    /// <param name="val"></param>
    public void OnValueChanged(bool val)
    {
        DebugWindow.SetActive(val);
    }
}
