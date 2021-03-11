//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Test_Player : MonoBehaviour
//{
//    Tiger tiger;

//    private void Start()
//    {
//        tiger = GetComponent<Tiger>();
//    }
//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "Tiger")
//        {
//            tiger.m_atkPoint = Random.Range(5f, 8f);
//            UIManager.Instance.DecreasesHP(tiger.m_atkPoint);
//        }
//    }
//}
