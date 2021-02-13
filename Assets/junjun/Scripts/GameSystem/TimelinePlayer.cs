using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : SingletonMonoBehavior<TimelinePlayer>
{
    public PlayableDirector playableDirector;

    void Start()
    {
        //同じゲームオブジェクトにあるPlayableDirectorを取得する
        playableDirector = GetComponent<PlayableDirector>();
        PlayTimeline();
    }

    //再生する
    void PlayTimeline()
    {
        playableDirector.Play();
    }

    //一時停止する
    void PauseTimeline()
    {
        playableDirector.Pause();
    }

    //一時停止を再開する
    void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    //停止する
    void StopTimeline()
    {
        playableDirector.Stop();
    }

}