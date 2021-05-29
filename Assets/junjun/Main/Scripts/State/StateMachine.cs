using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class StateMachine<T> where T : MonoBehaviour
    {
        /// <summary>ステートの種類によってオーナーを設定する</summary>
        T owner;
        /// <summary>現在のステート</summary>
        public IState<T> currentState;

        /// <summary>
        /// オーナーと初期ステートを設定するコンストラクター
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="initState"></param>
        public StateMachine(T owner, IState<T> initState)
        {
            this.owner = owner;
            currentState = initState;
        }

        /// <summary>
        /// 次のステートに移行させる関数
        /// </summary>
        /// <param name="nextState"></param>
        public void ChageMachine(IState<T> nextState)
        {
            currentState = nextState;
        }
    }
}
