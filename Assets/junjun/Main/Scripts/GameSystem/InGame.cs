using UnityEngine;
using UnityEngine.UI;

namespace Junjun
{
    public class InGame : IState<GameManager>
    {
        public void OnExecute(GameManager owner)
        {

            if (owner.m_minute < owner.m_stopTimer)
            {
                owner.m_seconds += Time.unscaledDeltaTime;
                if (owner.m_seconds >= 60f)
                {
                    owner.m_minute++;
                    owner.m_seconds = owner.m_seconds - 60;
                }
                //　値が変わった時だけテキストUIを更新
                if (owner.m_seconds != owner.m_oldSeconds)
                {
                    owner.m_timerText.text = owner.m_minute.ToString() + ":" + owner.m_seconds.ToString("f1");
                }
                owner.m_oldSeconds = owner.m_seconds;
            }
        }
    }
}