using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEnemyData<T> where T : MonoBehaviour
    {
        /// <summary>
        /// Enemy�̃f�[�^��ScriptableObject�ɓo�^����֐�
        /// </summary>
        /// <param name="owner"></param>
        void OnEntry(T owner);
    }
}
