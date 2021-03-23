using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponsBase : MonoBehaviour
{
    [SerializeField] public WeaponsType m_weaponsType;
    [SerializeField] public int m_power;
    public EnemyBase enemyBase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tiger")
        {
            //左のコントローラーを0.5秒間振動させる
            StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.RTouch));
            enemyBase.m_hp -= m_power;
        }
    }

    /// <summary>
    /// Oculus Quest(やQuest2)のコントローラーを振動させる
    /// </summary>
    public static IEnumerator Vibrate(float duration = 0.1f, float frequency = 2.0f, float amplitude = 2.0f, OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        //コントローラーを振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //指定された時間待つ
        yield return new WaitForSeconds(duration);

        //コントローラーの振動を止める
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}

