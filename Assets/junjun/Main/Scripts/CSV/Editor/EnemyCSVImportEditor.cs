using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Junjun
{
    /// <summary>
    /// ボタン一つでCSVからEnemyDataのScriptableObjectを生成させる
    /// Editor拡張
    /// </summary>
    [CustomEditor(typeof(EnemyCSVImporter))]
    public class EnemyCSVImpotEditor : Editor, ICSVImport<EnemyCSVImporter>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = target as EnemyCSVImporter;
            if (GUILayout.Button("Apply"))
            {
                // CSVファイルがアタッチされていないならResourcesファイルからセットする
                if (t.cSV == null)
                {
                    CSVLoad(t);
                }
                SetCsvDataToScriptableObject(t);
            }
        }

        public void CSVLoad(EnemyCSVImporter cSVLoader)
        {
            Debug.Log("CSVファイルをセットします。");

            //　テキストファイルの読み込みを行ってくれるクラス
            TextAsset textasset = new TextAsset();
            //　先ほど用意したcsvファイルを読み込ませる。
            //　ファイルは「Resources」フォルダを作り、そこに入れておくこと。また"CSVTestData"の部分はファイル名に合わせて変更する。
            textasset = Resources.Load("CSVEnemy", typeof(TextAsset)) as TextAsset;

            cSVLoader.cSV = textasset;
        }

        public void SetCsvDataToScriptableObject(EnemyCSVImporter csvLoader)
        {
            // ボタンを押されたらパース実行
            if (csvLoader.cSV == null)
            {
                Debug.LogWarning(csvLoader.name + " : 読み込むCSVファイルがセットされていません。");
                return;
            }

            // csvファイルをstring形式に変換
            string csvText = csvLoader.cSV.text;

            // 改行ごとにパース
            string[] afterParse = csvText.Split('\n');

            // ヘッダー行を除いてインポート
            for (int i = 1; i < afterParse.Length; i++)
            {
                string[] parseByComma = afterParse[i].Split(',');

                int column = 0;

                // 先頭の列が空であればその行は読み込まない
                if (parseByComma[column] == "")
                {
                    continue;
                }

                // EnemyDataのインスタンスをメモリ上に作成
                var enemyData = CreateInstance<EnemyData>();

                // 行数をIDとしてファイルを作成
                string fileName = enemyData.name = parseByComma[column] + "EnemyData" + ".asset";
                string path = "Assets/junjun/Main/ScriptableObject/Enemy/" + fileName;


                // 名前
                enemyData.name = parseByComma[column];

                // 最大HP
                column += 1;
                enemyData.hp = int.Parse(parseByComma[column]);

                // 攻撃力
                column += 1;
                enemyData.power = int.Parse(parseByComma[column]);

                // インスタンス化したものをアセットとして保存
                var asset = (EnemyData)AssetDatabase.LoadAssetAtPath(path, typeof(EnemyData));
                if (asset == null)
                {
                    // 指定のパスにファイルが存在しない場合は新規作成
                    AssetDatabase.CreateAsset(enemyData, path);
                }
                else
                {
                    // 指定のパスに既に同名のファイルが存在する場合は更新
                    EditorUtility.CopySerialized(enemyData, asset);
                    AssetDatabase.SaveAssets();
                }
                AssetDatabase.Refresh();
            }
            Debug.Log("敵データの作成が完了しました。");
        }
    }

}
