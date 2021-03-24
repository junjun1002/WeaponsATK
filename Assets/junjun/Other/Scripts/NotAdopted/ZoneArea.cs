using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneArea : MonoBehaviour
{
    /// <summary>
    /// スロウタイムになる時 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Spider")
        {
            Debug.Log("時よおそくなれ");
            TimeState.Instance.SlowTime();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Spider")
        {
            Debug.Log("時よ戻れ");
            TimeState.Instance.RestoredTime();
        }
    }
}
