using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class EnemyController : StateControllerBase
{
    BoolReactiveProperty isDied = new BoolReactiveProperty(false);

    private void Start()
    {
        DoDie();
    }

    async UniTask DoDie()
    {
        await isDied;

        gameObject.SetActive(false);
        Debug.Log("Die");
    }

    public void Kill()
    {
        isDied.Value = true;
    }
}
