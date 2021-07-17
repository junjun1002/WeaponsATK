using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public static class ControllerVibration
    {
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

    /// <summary>
    ///  武器の基底クラス
    /// </summary>
    public abstract class WeaponsBase : EventReceiver<WeaponsBase>
    {
        /// <summary>WeaponのScriptableObject</summary>
        [SerializeField] WeaponData weaponData;

        /// <summary>武器の最低攻撃力</summary>
        int m_minAtk;
        /// <summary>武器の最高攻撃力</summary>
        int m_maxAtk;
        /// <summary>武器の攻撃力</summary>
        [SerializeField, HideInInspector] public int m_power;

        void Start()
        {
            Init();
        }

        /// <summary>
        /// Enemyにダメージを与えたときの処理
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
            {
                m_power = Random.Range(m_minAtk, m_maxAtk);
                other.gameObject.GetComponent<EnemyBase>().m_currentHp -= m_power;
            }
        }

        void Init()
        {
            m_maxAtk = weaponData.maxAtk;
            m_minAtk = weaponData.minAtk;
        }
    }
}

