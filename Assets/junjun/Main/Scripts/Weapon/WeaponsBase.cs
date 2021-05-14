using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  武器の基底クラス
/// </summary>
public abstract class WeaponsBase : MonoBehaviour
{
    /// <summary>武器の種類</summary>
    [SerializeField] public WeaponsType m_weaponsType;
    /// <summary>武器の攻撃力</summary>
    [SerializeField] public int m_power;

    public EnemyBase enemyBase;

    /// <summary>
    /// Enemyにダメージを与えたときの処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //左のコントローラーを0.5秒間振動させる
            StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.RTouch));
            enemyBase.m_hp -= m_power;
            enemyBase.KnockBack();

            UIManager.Instance.m_damageText.text = m_power.ToString();
            UIManager.Instance.PopUpText();
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

