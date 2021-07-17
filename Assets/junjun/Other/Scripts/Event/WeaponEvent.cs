using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class WeaponEvent : MonoBehaviour, IEventCollision
    {
        //  武器の種類
        [SerializeField] public WeaponsType m_weaponsType;

        [SerializeField] WeaponsBase weapons;

        /// <summary>1フレーム前の位置</summary>
        Vector3 lastPos;
        /// <summary>Rayが当たったか</summary>
        bool isHit;

        RaycastHit hit;

        private void Update()
        {
            Debug.DrawRay(lastPos, transform.position - lastPos, Color.red, 5);

            lastPos = this.transform.position;
        }

        public void CollisionEvent(EventSystemInGameScene eventSystem)
        {
            //左のコントローラーを0.5秒間振動させる
            StartCoroutine(ControllerVibration.Vibrate(duration: 0.5f, controller: OVRInput.Controller.RTouch));

            isHit = Physics.Raycast(lastPos, transform.position - lastPos, out hit);
            /* 
               *敵に攻撃を当てた際に一フレーム前の位置と今のフレーム位置の
               *真ん中の位置からダメージテキストを出すことで当たった箇所からダメージテキストが
               *出ているようになる
             */
            if (isHit)
            {
                Debug.Log(hit.point);
                Debug.Log("hit");
                UIManager.Instance.m_damageText.rectTransform.position = hit.point;
                UIManager.Instance.m_damageText.text = weapons.m_power.ToString();
                UIManager.Instance.PopUpText();
            }
        }
    }

    /// <summary>
    /// 武器の種類
    /// </summary>
    public enum WeaponsType
    {
        Sword
    }
}