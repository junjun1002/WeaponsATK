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
    //            //　IndexOfの引数は"/(読み込ませたいファイル名)"とする。
    //            if (str.IndexOf("/CSVEnemy.csv") != -1)
    //            {
    //                //　エディタ内で読み込むならResource.Loadではなくこちらを使うこともできる。
    //                TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
    //                //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
    //                string assetfile = str.Replace(".csv", ".asset");
    //                //　※"MonsterDataBase"はScriptableObjectのクラス名に合わせて変更する。
    //                EnemyStatusData enemyStatusData = AssetDatabase.LoadAssetAtPath<EnemyStatusData>(assetfile);
    //                if (enemyStatusData == null)
    //                {
    //                    enemyStatusData = new EnemyStatusData();
    //                    AssetDatabase.CreateAsset(enemyStatusData, assetfile);
    //                }
    //                //　※"MonsterData"はScriptableObjectに入れるデータのクラス名に合わせて変更。
    //                //　※"datas"もScriptableObjectが保有する配列名に合わせる。
    //                enemyStatusData.enemyDatas = CSVSerializer.Deserialize<EnemyData>(textasset.text);
    //                EditorUtility.SetDirty(enemyStatusData);
    //                AssetDatabase.SaveAssets();
    //            }
    //        }
    //    }
    //}
}
