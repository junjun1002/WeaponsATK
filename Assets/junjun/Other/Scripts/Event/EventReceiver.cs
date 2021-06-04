//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// ゲーム内のイベント購読クラス
///// </summary>
///// <typeparam name="T"></typeparam>
//public abstract class EventReceiver<T> : MonoBehaviour where T : MonoBehaviour
//{
//    protected EventSystemInGameScene m_eventSystemInGameScene;

//    private void Awake()
//    {
//        m_eventSystemInGameScene = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventSystemInGameScene>();
//    }

//    //// イベント登録
//    //protected abstract void OnEnable();
//    //// イベント解除
//    //protected abstract void OnDisable();
//}
