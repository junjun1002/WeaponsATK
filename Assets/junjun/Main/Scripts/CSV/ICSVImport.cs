using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// CSV��ǂݍ����ScriptableObject����鎞�͕K���������p������
    /// ���g�͌p����Œ�`
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICSVImport<T> where T : ScriptableObject
    { 
        /// <summary>
        /// CSV���ݒ肳��ĂȂ��Ƃ��ɃZ�b�g������֐�
        /// </summary>
        /// <param name="owner"></param>
        void CSVLoad(T owner);

        /// <summary>
        ///  CSV�f�[�^��ScriptableObject���ɗ������ފ֐�
        /// </summary>
        /// <param name="owner"></param>
        void SetCsvDataToScriptableObject(T owner);
    }
}
