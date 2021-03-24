using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// jsonでデータをセーブとロードを行うクラス
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
    /// ベストタイムが更新されたときにセーブする
    /// </summary>
    /// <param name="minite"></param>
    /// <param name="second"></param>
    public void SaveTimeData(int minite, float second)
    {
        if ((timeData.bestMinute > minite) || (timeData.bestMinute == minite && timeData.bestSecond > second))
        {
            Debug.Log("ベストスコア更新");
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
    /// ベストタイムをロード
    /// </summary>
    public void LoadTimeData()
    {
        string datastr = "";
        StreamReader reader;

        reader = new StreamReader(Application.persistentDataPath + ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        timeData = JsonUtility.FromJson<TimeData>(datastr); // ロードしたデータで上書き    
        m_bestTime.text = timeData.bestTime;
    }
}
