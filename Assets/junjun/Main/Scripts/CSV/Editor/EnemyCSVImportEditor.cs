using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Junjun
{
    /// <summary>
    /// �{�^�����CSV����EnemyData��ScriptableObject�𐶐�������
    /// Editor�g��
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
                // CSV�t�@�C�����A�^�b�`����Ă��Ȃ��Ȃ�Resources�t�@�C������Z�b�g����
                if (t.cSV == null)
                {
                    CSVLoad(t);
                }
                SetCsvDataToScriptableObject(t);
            }
        }

        public void CSVLoad(EnemyCSVImporter cSVLoader)
        {
            Debug.Log("CSV�t�@�C�����Z�b�g���܂��B");

            //�@�e�L�X�g�t�@�C���̓ǂݍ��݂��s���Ă����N���X
            TextAsset textasset = new TextAsset();
            //�@��قǗp�ӂ���csv�t�@�C����ǂݍ��܂���B
            //�@�t�@�C���́uResources�v�t�H���_�����A�����ɓ���Ă������ƁB�܂�"CSVTestData"�̕����̓t�@�C�����ɍ��킹�ĕύX����B
            textasset = Resources.Load("CSVEnemy", typeof(TextAsset)) as TextAsset;

            cSVLoader.cSV = textasset;
        }

        public void SetCsvDataToScriptableObject(EnemyCSVImporter csvLoader)
        {
            // �{�^���������ꂽ��p�[�X���s
            if (csvLoader.cSV == null)
            {
                Debug.LogWarning(csvLoader.name + " : �ǂݍ���CSV�t�@�C�����Z�b�g����Ă��܂���B");
                return;
            }

            // csv�t�@�C����string�`���ɕϊ�
            string csvText = csvLoader.cSV.text;

            // ���s���ƂɃp�[�X
            string[] afterParse = csvText.Split('\n');

            // �w�b�_�[�s�������ăC���|�[�g
            for (int i = 1; i < afterParse.Length; i++)
            {
                string[] parseByComma = afterParse[i].Split(',');

                int column = 0;

                // �擪�̗񂪋�ł���΂��̍s�͓ǂݍ��܂Ȃ�
                if (parseByComma[column] == "")
                {
                    continue;
                }

                // EnemyData�̃C���X�^���X����������ɍ쐬
                var enemyData = CreateInstance<EnemyData>();

                // �s����ID�Ƃ��ăt�@�C�����쐬
                string fileName = enemyData.name = parseByComma[column] + "EnemyData" + ".asset";
                string path = "Assets/junjun/Main/ScriptableObject/Enemy/" + fileName;


                // ���O
                enemyData.name = parseByComma[column];

                // �ő�HP
                column += 1;
                enemyData.hp = int.Parse(parseByComma[column]);

                // �U����
                column += 1;
                enemyData.power = int.Parse(parseByComma[column]);

                // �C���X�^���X���������̂��A�Z�b�g�Ƃ��ĕۑ�
                var asset = (EnemyData)AssetDatabase.LoadAssetAtPath(path, typeof(EnemyData));
                if (asset == null)
                {
                    // �w��̃p�X�Ƀt�@�C�������݂��Ȃ��ꍇ�͐V�K�쐬
                    AssetDatabase.CreateAsset(enemyData, path);
                }
                else
                {
                    // �w��̃p�X�Ɋ��ɓ����̃t�@�C�������݂���ꍇ�͍X�V
                    EditorUtility.CopySerialized(enemyData, asset);
                    AssetDatabase.SaveAssets();
                }
                AssetDatabase.Refresh();
            }
            Debug.Log("�G�f�[�^�̍쐬���������܂����B");
        }
    }

}
