using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中の衝突イベント
/// </summary>
public interface IEventCollision
{
    void CollisionEvent(EventSystemInGameScene eventSystem);
}
