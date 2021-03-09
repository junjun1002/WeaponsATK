using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
public abstract class WeaponsBase : MonoBehaviour
{
    [SerializeField] public WeaponsType m_weaponsType;

    Rigidbody m_rb;
    private OVRGrabbable ovrGrabbalbe;
    OVRInput.Controller controller;

    void Start()
    {
        ovrGrabbalbe = GetComponent<OVRGrabbable>();
        m_rb = GetComponent<Rigidbody>();
    }
}

/// <summary>
/// 武器の種類
/// 後々杖を追加したい（素手で魔法出そうかな）
/// </summary>
public enum WeaponsType
{
    None, Sword, Gun
}