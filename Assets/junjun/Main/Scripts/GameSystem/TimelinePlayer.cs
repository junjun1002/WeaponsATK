using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timelineを管理するクラス
/// </summary>
public class TimelinePlayer : SingletonMonoBehavior<TimelinePlayer>
{
    /// <summary>
    /// 再生する
    /// </summary>
    /// <param name="playableDirector"></param>
    public void PlayTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Play();
    }

    /// <summary>
    /// 一時停止する
    /// </summary>
    /// <param name="playableDirector"></param>
    public void PauseTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Pause();
    }

    /// <summary>
    /// 一時停止を再開する
    /// </summary>
    /// <param name="playableDirector"></param>
    public void ResumeTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Resume();
    }

    /// <summary>
    /// 停止する
    /// </summary>
    /// <param name="playableDirector"></param>
    public void StopTimeline(PlayableDirector playableDirector)
    {
        playableDirector.Stop();
    }

}