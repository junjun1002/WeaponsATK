using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandller : EventReceiver<PlayerEventHandller>
{
    public Tiger tiger;
    /// <summary>
    /// 敵に攻撃したとき
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Tiger")
        {
            if (tiger.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
            {
                Debug.Log("貴様敵やないかい？");
                eventCollision.CollisionEvent(m_eventSystemInGameScene);
            }
        }
    }
}
