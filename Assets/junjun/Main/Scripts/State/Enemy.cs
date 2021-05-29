using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UniRx;

namespace Junjun
{
    public class Enemy : MonoBehaviour
    {
        StateMachine<Enemy> stateMachine;

        ReactiveProperty<Idle> idleReactiveProperty;

        public IState<Enemy> IdleState { get; set; } = new Idle();

        public IState<Enemy> AttackState { get; set; } = new Attack();

        private async void Start()
        {
            stateMachine = new StateMachine<Enemy>(this, IdleState);
            Debug.Log(stateMachine.currentState);
            stateMachine.currentState.OnExcute();
            await UniTask.Delay(500);
            stateMachine.ChageMachine(AttackState);
            Debug.Log(stateMachine.currentState);
            stateMachine.currentState.OnExcute();

            
        }
    }

    public class Idle : IState<Enemy>
    {

        public void OnExcute()
        {
            Debug.Log("Idle");
        }

      
    }

    public class Attack : IState<Enemy>
    {
        public void OnExcute()
        {
            Debug.Log("Attack");
        }
    }
}
