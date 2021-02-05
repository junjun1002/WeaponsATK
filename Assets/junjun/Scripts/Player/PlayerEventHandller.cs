using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandller : EventReceiver<PlayerEventHandller>
{
    /// <summary>
    /// 武器とか
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("何か当たってるよ！collisionだよ！");
        if (collision.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
        {
            Debug.Log("貴様敵やないかい？");
            eventCollision.CollisionEvent(m_eventSystemInGameScene);
        }
    }
}
