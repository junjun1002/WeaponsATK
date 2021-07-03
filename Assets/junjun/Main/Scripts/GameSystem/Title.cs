using UnityEngine.UI;

namespace Junjun
{
    public class Title : IState<GameManager>
    {
        public void OnExecute(GameManager owner)
        {
            SaveAndLoad.Instance.LoadTimeData(owner.m_bestTimeText);
        }
    }
}