using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Junjun
{
    [Serializable,CreateAssetMenu(fileName = "CSVImporter", menuName = "CSV/CSVImporter")]
    public class CSVLoader :ScriptableObject
    {

        public TextAsset cSV;
    }

    //public class PostProcessingTest : AssetPostprocessor
    //{
    //    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    //    {
    //        foreach (string str in importedAssets)
    //        {
    //            //�@IndexOf�̈�����"/(�ǂݍ��܂������t�@�C����)"�Ƃ���B
    //            if (str.IndexOf("/CSVEnemy.csv") != -1)
    //            {
    //                //�@�G�f�B�^���œǂݍ��ނȂ�Resource.Load�ł͂Ȃ���������g�����Ƃ��ł���B
    //                TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
    //                //�@������ScriptableObject�t�@�C����ǂݍ��ށB�Ȃ��ꍇ�͐V���ɍ��B
    //                string assetfile = str.Replace(".csv", ".asset");
    //                //�@��"MonsterDataBase"��ScriptableObject�̃N���X���ɍ��킹�ĕύX����B
    //                EnemyStatusData enemyStatusData = AssetDatabase.LoadAssetAtPath<EnemyStatusData>(assetfile);
    //                if (enemyStatusData == null)
    //                {
    //                    enemyStatusData = new EnemyStatusData();
    //                    AssetDatabase.CreateAsset(enemyStatusData, assetfile);
    //                }
    //                //�@��"MonsterData"��ScriptableObject�ɓ����f�[�^�̃N���X���ɍ��킹�ĕύX�B
    //                //�@��"datas"��ScriptableObject���ۗL����z�񖼂ɍ��킹��B
    //                enemyStatusData.enemyDatas = CSVSerializer.Deserialize<EnemyData>(textasset.text);
    //                EditorUtility.SetDirty(enemyStatusData);
    //                AssetDatabase.SaveAssets();
    //            }
    //        }
    //    }
    //}
}
