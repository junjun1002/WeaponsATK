using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// json�Ńf�[�^���Z�[�u�ƃ��[�h���s���N���X
/// </summary>
public class SaveAndLoad : SingletonMonoBehavior<SaveAndLoad>
{
    [SerializeField] Text m_bestTime;

    [Serializable]
    public class TimeData
    {
        public string bestTime;
        public int bestMinute = 4;
        public float bestSecond = 59;
    }

    TimeData timeData = new TimeData();

    private void Start()
    {
        LoadTimeData();
    }

    /// <summary>
    /// �x�X�g�^�C�����X�V���ꂽ�Ƃ��ɃZ�[�u����
    /// </summary>
    /// <param name="minite"></param>
    /// <param name="second"></param>
    public void SaveTimeData(int minite, float second)
    {
        if ((timeData.bestMinute > minite) || (timeData.bestMinute == minite && timeData.bestSecond > second))
        {
            Debug.Log("�x�X�g�X�R�A�X�V");
            timeData.bestTime = minite.ToString() + ":" + second.ToString("f1");
            timeData.bestMinute = minite;
            timeData.bestSecond = second;
        }
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(timeData);
        writer = new StreamWriter(Application.persistentDataPath + ".json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
    
    /// <summary>
    /// �x�X�g�^�C�������[�h
    /// </summary>
    public void LoadTimeData()
    {
        string datastr = "";
        StreamReader reader;

        reader = new StreamReader(Application.persistentDataPath + ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        timeData = JsonUtility.FromJson<TimeData>(datastr); // ���[�h�����f�[�^�ŏ㏑��    
        m_bestTime.text = timeData.bestTime;
    }
}
