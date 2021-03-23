using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timelineを管理するクラス
/// </summary>
public class TimelinePlayer : SingletonMonoBehavior<TimelinePlayer>
{
    //public PlayableDirector playableDirector;

    void Start()
    {
        ////同じゲームオブジェクトにあるPlayableDirectorを取得する
        //playableDirector = GetComponent<PlayableDirector>();
    }

    //再生する
    public void PlayTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Play();
    }

    //一時停止する
    public void PauseTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Pause();
    }

    //一時停止を再開する
    public void ResumeTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Resume();
    }

    //停止する
    public void StopTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Stop();
    }

}