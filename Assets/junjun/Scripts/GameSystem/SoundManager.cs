using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehavior<SoundManager>
{
    // シーンのBGM
    [SerializeField] AudioSource m_bgm;
    // シーンのSE
    [SerializeField] AudioSource m_se;
    // TigerのSE
    [SerializeField] AudioSource m_tigerSE;

    // Tiger用のSE
    public AudioClip m_runSE;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void  PlayTigerSE(AudioClip SE)
    {
        m_tigerSE.PlayOneShot(SE);
    }
}
