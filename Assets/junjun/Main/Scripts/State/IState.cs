using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Junjun
{
    public interface IState<T> where T : MonoBehaviour
    {
        void OnExcute(T owner);
    }
}

