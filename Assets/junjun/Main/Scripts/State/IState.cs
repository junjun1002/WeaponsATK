using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public interface IState<T> where T : MonoBehaviour
    {
        void OnExcute();
    }
}

