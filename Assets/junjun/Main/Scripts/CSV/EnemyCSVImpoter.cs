using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Junjun
{
    /// <summary>
    /// CSV��ǂݍ����ScriptableObject�𐶐�����ScriptableObject
    /// </summary>
    [Serializable,CreateAssetMenu(fileName = "EnemyCSVImporter", menuName = "CSV/EnemyCSVImporter")]
    public class EnemyCSVImporter :ScriptableObject
    {
        /// <summary>
        /// Inspecter����ݒ肵�Ă����Ȃ��Ă��悵
        /// CSV��ǂݍ��܂��鑤����ݒ肳��ĂȂ��ꍇ�ݒ肳���邽��
        /// </summary>
        public TextAsset cSV;
    }
}
