using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestToggle : MonoBehaviour
{
    public GameObject DebugWindow;

    /// <summary>
    /// �f�o�b�O�E�C���h�E�̕\���؂�ւ�
    /// </summary>
    /// <param name="val"></param>
    public void OnValueChanged(bool val)
    {
        DebugWindow.SetActive(val);
    }
}
