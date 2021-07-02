using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Junjun
{
    /// <summary>
    /// �{�^�����CSV����WeaponData��ScriptableObject�𐶐�������
    /// Editor�g��
    /// </summary>
    [CustomEditor(typeof(WeaponCSVImporter))]
    public class WeaponCSVImportEditor : Editor, ICSVImport<WeaponCSVImporter>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = target as WeaponCSVImporter;
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

        public void CSVLoad(WeaponCSVImporter cSVLoader)
        {
            Debug.Log("CSV�t�@�C�����Z�b�g���܂��B");

            //�@�e�L�X�g�t�@�C���̓ǂݍ��݂��s���Ă����N���X
            TextAsset textasset = new TextAsset();
            //�@��قǗp�ӂ���csv�t�@�C����ǂݍ��܂���B
            //�@�t�@�C���́uResources�v�t�H���_�����A�����ɓ���Ă������ƁB�܂�"CSVTestData"�̕����̓t�@�C�����ɍ��킹�ĕύX����B
            textasset = Resources.Load("CSVWeapon", typeof(TextAsset)) as TextAsset;

            cSVLoader.cSV = textasset;
        }

        public void SetCsvDataToScriptableObject(WeaponCSVImporter csvLoader)
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
                var weaponData = CreateInstance<WeaponData>();

                // �s����ID�Ƃ��ăt�@�C�����쐬
                string fileName = weaponData.weaponName = parseByComma[column] + "WeaponData" + ".asset";
                string path = "Assets/junjun/Main/ScriptableObject/Weapon/" + fileName;


                // ���O
                weaponData.weaponName = parseByComma[column];

                // �Œ�U����
                column += 1;
                weaponData.minAtk = int.Parse(parseByComma[column]);

                // �ō��U����
                column += 1;
                weaponData.maxAtk = int.Parse(parseByComma[column]);

                // �C���X�^���X���������̂��A�Z�b�g�Ƃ��ĕۑ�
                var asset = (EnemyData)AssetDatabase.LoadAssetAtPath(path, typeof(EnemyData));
                if (asset == null)
                {
                    // �w��̃p�X�Ƀt�@�C�������݂��Ȃ��ꍇ�͐V�K�쐬
                    AssetDatabase.CreateAsset(weaponData, path);
                }
                else
                {
                    // �w��̃p�X�Ɋ��ɓ����̃t�@�C�������݂���ꍇ�͍X�V
                    EditorUtility.CopySerialized(weaponData, asset);
                    AssetDatabase.SaveAssets();
                }
                AssetDatabase.Refresh();
            }
            Debug.Log(" ����f�[�^�̍쐬���������܂����B");
        }
    }
}
