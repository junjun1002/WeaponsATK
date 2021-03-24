using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音の管理をするクラス
/// （今後改良しまくりたいクラス）
/// </summary>
public class SoundManager : SingletonMonoBehavior<SoundManager>
{
    // TigerのSE
    [SerializeField] AudioSource m_tigerSE;

    // Tiger用のSE
    public AudioClip m_runSE;

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }

    public void  PlayTigerSE(AudioClip SE)
    {
        m_tigerSE.PlayOneShot(SE);
    }
}
