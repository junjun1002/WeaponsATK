using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 音の管理をするクラス
/// （今後改良しまくりたいクラス）
/// </summary>
public class SoundManager : SingletonMonoBehavior<SoundManager>
{
    [SerializeField] AudioSource m_audio;
    [SerializeField] AudioMixerGroup m_bgm;

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// ゾーン状態時のAudioMixer
    /// </summary>
    public void ZoneTime()
    {
        m_audio.outputAudioMixerGroup = m_bgm;
    }

    public void ResetAudioMixer()
    {
        m_audio.outputAudioMixerGroup = null;
    }
}
