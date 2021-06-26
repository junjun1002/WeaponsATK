using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Junjun
{
    /// <summary>
    /// CSVを読み込んでScriptableObjectを生成するScriptableObject
    /// </summary>
    [Serializable,CreateAssetMenu(fileName = "EnemyCSVImporter", menuName = "CSV/EnemyCSVImporter")]
    public class EnemyCSVImporter :ScriptableObject
    {
        /// <summary>
        /// Inspecterから設定してもしなくてもよし
        /// CSVを読み込ませる側から設定されてない場合設定させるため
        /// </summary>
        public TextAsset cSV;
    }
}
