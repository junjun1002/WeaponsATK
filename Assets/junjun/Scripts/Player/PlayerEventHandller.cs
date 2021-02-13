using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandller : EventReceiver<PlayerEventHandller>
{
    /// <summary>
    /// 敵に攻撃したとき
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("何か当たってるよ！collisionだよ！");
        if (other.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
        {
            Debug.Log("貴様敵やないかい？");
            eventCollision.CollisionEvent(m_eventSystemInGameScene);
        }
    }

    /// <summary>
    /// 敵に攻撃されたとき
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spider")
        {
            m_eventSystemInGameScene.ExecuteGameOver();
            Debug.Log("GameOverだよん(笑)");
        }
    }
}
