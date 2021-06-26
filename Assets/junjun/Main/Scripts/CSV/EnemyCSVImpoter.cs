using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Junjun
{
    /// <summary>
    /// CSV‚ğ“Ç‚İ‚ñ‚ÅScriptableObject‚ğ¶¬‚·‚éScriptableObject
    /// </summary>
    [Serializable,CreateAssetMenu(fileName = "EnemyCSVImporter", menuName = "CSV/EnemyCSVImporter")]
    public class EnemyCSVImporter :ScriptableObject
    {
        /// <summary>
        /// Inspecter‚©‚çİ’è‚µ‚Ä‚à‚µ‚È‚­‚Ä‚à‚æ‚µ
        /// CSV‚ğ“Ç‚İ‚Ü‚¹‚é‘¤‚©‚çİ’è‚³‚ê‚Ä‚È‚¢ê‡İ’è‚³‚¹‚é‚½‚ß
        /// </summary>
        public TextAsset cSV;
    }
}
