using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// CSVを読み込んでScriptableObjectを作る時は必ずこいつを継承する
    /// 中身は継承先で定義
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICSVImport<T> where T : ScriptableObject
    { 
        /// <summary>
        /// CSVが設定されてないときにセットさせる関数
        /// </summary>
        /// <param name="owner"></param>
        void CSVLoad(T owner);

        /// <summary>
        ///  CSVデータをScriptableObjectをに流しこむ関数
        /// </summary>
        /// <param name="owner"></param>
        void SetCsvDataToScriptableObject(T owner);
    }
}
