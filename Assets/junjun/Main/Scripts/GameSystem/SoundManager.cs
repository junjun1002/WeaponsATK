using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音の管理をするクラス
/// （今後改良しまくりたいクラス）
/// </summary>
public class SoundManager : SingletonMonoBehavior<SoundManager>
{
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// SEを鳴らす関数（今のとここれで鳴らすものがない）
    /// </summary>
    /// <param name="SE"></param>
    //public void  PlayTigerSE(AudioClip SE)
    //{

    //}
}
